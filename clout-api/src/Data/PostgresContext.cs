using System.Reflection;
using AutoMapper;
using clout_api.Data.Base;
using clout_api.Data.Models;
using clout_api.Data.Models.Base;
using clout_api.Utilities;
using clout_api.Utilities.Base;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace clout_api.Data;

public partial class PostgresContext : DbContext
{
    private readonly IMapper? _mapper;
    private readonly HttpContextUtilitiesBase _httpContextUtilities;
    private ApplicationUser? _currentUser { get; set; } = null;

    public PostgresContext()
    {
    }

    public PostgresContext(DbContextOptions<PostgresContext> options, IMapper mapper, HttpContextUtilitiesBase httpContextUtilities)
        : base(options)
    {
        _mapper = mapper;
        _httpContextUtilities = httpContextUtilities;
    }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    public virtual DbSet<AuditEvent> AuditEvents { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserFriend> UserFriends { get; set; }

    public DbSet<FriendRequest> FriendRequests { get; set; }

    // This is where the magic of auditing happens
    //      1. First, save the changes to generate ids for the auditable records
    //      2. Find all the changed entities where the base is "AuditableBase"
    //      3. Loop through those changes, and call the "OnModelUpdate" method
    //      4. Call the base save changes method
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await SetUserInformation();

        var auditsSaved = 0;
        var entitiesSaved = await base.SaveChangesAsync(cancellationToken);
        var auditableChanges = ChangeTracker.Entries()
                                .Where(e =>
                                    e.Entity is AuditableBase &&
                                    e.State != EntityState.Deleted)
                                .ToList();

        foreach (var change in auditableChanges)
        {
            var auditableBase = change.Entity as AuditableBase;

            if (auditableBase is not null && _mapper is not null)
            {
                auditableBase.OnModelUpdate(this, _mapper);
            }
            else
            {
                Log.Logger.Error("Mapper is null in EF Context, auditing is disabled");
            }
        }

        if (auditableChanges.Count > 0)
        {
            auditsSaved = await base.SaveChangesAsync(cancellationToken);
        }

        return auditsSaved + entitiesSaved;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        if (environmentName is HostingEnvironment.DEVELOPMENT)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Eventually we'll convert all of our models over to use this (already have it mostly done)
        // This provides that automagic that allows us to build our audit tables
        // It also has the added benefit of keeping the database definition within the actual model
        // class (see: AuditEvent.cs and Event.cs)
        var modelTypes = Assembly.GetAssembly(typeof(ModelBase))!
                    .GetTypes()
                    .Where(
                        derived => derived.IsClass && !derived.IsAbstract && derived.IsSubclassOf(typeof(ModelBase)));

        foreach (Type type in modelTypes)
        {
            var derivedType = Activator.CreateInstance(type) as ModelBase;
            if (derivedType is not null)
            {
                derivedType.OnModelCreating(modelBuilder);
            }
        }
    }

    private async Task SetUserInformation()
    {
        if (_currentUser is null)
        {
            var username = _httpContextUtilities.GetUsernameFromHttpContext();
            _currentUser = await ApplicationUsers.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);

            if (_currentUser is null)
            {
                _currentUser = new ApplicationUser()
                {
                    Username = username,
                    CreatedBy = ApplicationUser.SystemUser.Id,
                    ExternalReference = null, //TODO Fix this once we have claims available,
                    UpdatedBy = ApplicationUser.SystemUser.Id,
                };

                ApplicationUsers.Add(_currentUser);
            }
        }

        foreach (var change in ChangeTracker.Entries().Where(e => e.Entity is ModelBase && e.State != EntityState.Deleted))
        {
            var changedEntity = change.Entity as ModelBase;

            if (changedEntity is null)
            {
                continue;
            }

            changedEntity.UpdatedBy = _currentUser.Id;
            if (change.State == EntityState.Added)
            {
                changedEntity.CreatedBy = _currentUser.Id;
            }
        }
    }
}

using clout_api.Data.Base;
using clout_api.Data.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace clout_api.Data.Models;

public class ApplicationUser : ModelBase
{
    public readonly static ApplicationUser SystemUser = new()
    {
        Username = "System",
        CreatedBy = 0,
        ExternalReference = null,
        Id = 1
    };

    public int Id { get; set; }
    public required string Username { get; set; }
    public string? ExternalReference { get; set; }

    protected override Action<ModelBuilder> OnBuildAction => (modelBuilder) =>
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("application_user");

            entity.Property(e => e.ExternalReference).HasColumnName("external_reference");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasData(SystemUser);

            BuildModelBaseEntity(entity);
        });
}

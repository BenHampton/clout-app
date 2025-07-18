// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data;
using clout_api.Data.Repository;
using clout_api.Data.Repository.Base;
using clout_api.Extensions.Mapper;
using clout_api.Services;
using clout_api.Services.Base;
using clout_api.Utilities;
using clout_api.Utilities.Base;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace clout_api.Extensions;

public static class ServiceRegistrationExtensions
{
    public static IServiceCollection AddDependencyExtensions(this IServiceCollection services, string dbConnectionString)
    {
        services.AddSerilog();

        //database connection
        services.AddDbContext<PostgresContext>(options =>
            options.UseNpgsql(dbConnectionString));
        services.AddSingleton<DapperContext>();

        //service
        services.AddScoped<UserServiceBase, UserService>();
        services.AddScoped<UserFriendServiceBase, UserFriendService>();
        services.AddScoped<FriendRequestServiceBase, FriendRequestService>();
        services.AddScoped<PostServiceBase, PostService>();
        services.AddScoped<CommentServiceBase, CommentService>();

        //mapper
        services.AddAutoMapper(typeof(AutoMapperProfileExtensions));

        //repository
        services.AddScoped<UserRepositoryBase, UserRepository>();
        services.AddScoped<UserFriendRepositoryBase, UserFriendRepository>();
        services.AddScoped<FriendRequestRepositoryBase, FriendRequestRepository>();
        services.AddScoped<PostRepositoryBase, PostRepository>();
        services.AddScoped<CommentRepositoryBase, CommentRepository>();

        // HttpContext
        services.AddHttpContextAccessor();

        // Utilities
        services.AddScoped<HttpContextUtilitiesBase, HttpContextUtilities>();
        //web services
        // When we have web service connectivity, we can uncomment this. Note, we'll also have to change the reference in ScheduleWsImporter
        // Since the generated code doesn't have an parent/base class, we can't just swap the implementation, and we'll actually have
        // To change the constructor argument
        // services.AddScoped(s => new DataInterfaceClient(DataInterfaceClient.EndpointConfiguration.DataInterfaceWsHttpBinding));
        // services.AddScoped<MockScheduling>();

        return services;
    }
}

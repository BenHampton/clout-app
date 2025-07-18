// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace clout_api.Data.Models;

public class User : ModelBase
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public ICollection<UserFriend> UserFriends { get; set; } = new List<UserFriend>();

    public ICollection<FriendRequest> FriendRequests { get; set; } = new List<FriendRequest>();

    protected override Action<ModelBuilder> OnBuildAction => (modelBuilder) =>
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(user => user.Id).HasName("user_pk");

            entity.ToTable("user");

            entity.Property(user => user.Id).HasColumnName("id");
            entity.Property(user => user.FirstName).HasColumnName("first_name");
            entity.Property(user => user.LastName).HasColumnName("last_name");
            // BuildModelBaseEntity(entity);
        });
}

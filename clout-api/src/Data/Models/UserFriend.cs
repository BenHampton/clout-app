// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace clout_api.Data.Models;

public class UserFriend : ModelBase
{
    public int Id { get; set; }

    public required int UserId { get; set; }

    public required int FriendId { get; set; }

    public User User { get; set; }

    public User Friend { get; set; }

  protected override Action<ModelBuilder> OnBuildAction => (modelBuilder) =>
    modelBuilder.Entity<UserFriend>(entity =>
    {
        entity.HasKey(userFriend => userFriend.Id).HasName("user_friend_pk");

        entity.ToTable("user_friend");

        entity.Property(userFriend => userFriend.Id).HasColumnName("id");
        entity.Property(userFriend => userFriend.UserId).HasColumnName("user_id");
        entity.Property(userFriend => userFriend.FriendId).HasColumnName("friend_id");

        entity.HasOne(user => user.User).WithMany(p => p.UserFriends)
            .HasForeignKey(uf => uf.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("user_friend_user_id_fk");
        // BuildModelBaseEntity(entity);
    });
}

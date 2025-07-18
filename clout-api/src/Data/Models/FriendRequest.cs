// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace clout_api.Data.Models;

public class FriendRequest : ModelBase
{
    public int Id { get; set; }

    public required int UserIdOne { get; set; }

    public required int UserIdTwo { get; set; }

    public required int Requestor { get; set; } // enum (UserIdOne, UserIdTwo)

    protected override Action<ModelBuilder> OnBuildAction => (modelBuilder) =>
    modelBuilder.Entity<FriendRequest>(entity =>
    {
        entity.HasKey(friendRequest => friendRequest.Id).HasName("user_request_pk");

        entity.ToTable("friend_request");

        entity.Property(friendRequest => friendRequest.UserIdOne).HasColumnName("user_id_one");
        entity.Property(friendRequest => friendRequest.UserIdTwo).HasColumnName("user_id_two");
        entity.Property(friendRequest => friendRequest.Requestor).HasColumnName("requestor");


        // BuildModelBaseEntity(entity);
    });
}

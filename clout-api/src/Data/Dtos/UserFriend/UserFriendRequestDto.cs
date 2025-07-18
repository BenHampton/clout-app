// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Models;

namespace clout_api.Data.Dtos.UserFriend;

public class UserFriendRequestDto
{
    public required int RelationshipTypeId { get; set; }

    public required int UserId { get; set; }

    public required int FriendId { get; set; }
}

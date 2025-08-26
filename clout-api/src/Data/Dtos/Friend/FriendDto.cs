// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Models;

namespace clout_api.Data.Dtos.Friend;

public class FriendDto
{
    public required int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public RelationshipType? RelationshipType { get; set; }

}

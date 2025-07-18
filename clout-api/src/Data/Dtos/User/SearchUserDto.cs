// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Dtos.UserFriend;
using clout_api.Data.Models;

namespace clout_api.Data.Dtos.User;

public class SearchUserDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public RelationshipType RelationshipType { get; set; }

    public bool IsFriend { get; set; }

    public bool IsRequested { get; set; }

    public bool IsPendingOutGoing { get; set; }

    public bool IsPendingIncoming { get; set; }
}

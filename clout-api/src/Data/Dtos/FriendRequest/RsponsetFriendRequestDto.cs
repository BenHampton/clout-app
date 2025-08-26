// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Models;

namespace clout_api.Data.Dtos.FriendRequest;

public class ResponseFriendRequestDto
{
    public required int UserIdOne { get; set; }

    public required int UserIdTwo { get; set; }

    public required int Requestor { get; set; } // enum (UserIdOne, UserIdTwo)

    public RelationshipType RelationshipType { get; set; }
}

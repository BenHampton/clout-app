// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace clout_api.Data.Dtos.FriendRequest;

public class FriendRequestDto
{
    public int Id { get; set; }

    public required int UserIdOne { get; set; }

    public required int UserIdTwo { get; set; }

    public required int Requestor { get; set; } // enum (UserIdOne, UserIdTwo)
}

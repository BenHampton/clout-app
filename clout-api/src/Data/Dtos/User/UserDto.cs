// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Dtos.Friend;
using clout_api.Data.Dtos.UserFriend;

namespace clout_api.Data.Dtos.User;

public class UserDto
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public List<MiniFriendDto> Friends { get; set; } = new List<MiniFriendDto>();
}

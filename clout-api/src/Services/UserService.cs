// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using clout_api.Data.Repository.Base;
using clout_api.Services.Base;

namespace clout_api.Services;

public class UserService : UserServiceBase
{
    public UserService(ILogger<UserService> logger,
        IMapper mapper,
        UserFriendServiceBase userFriendService,
        FriendRequestServiceBase friendRequestService,
        UserRepositoryBase userRepository)
        : base(logger,
            mapper,
            userFriendService,
            friendRequestService,
            userRepository)
    {
    }
}

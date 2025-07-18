// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using clout_api.Data.Repository.Base;
using clout_api.Services.Base;

namespace clout_api.Services;

public class FriendRequestService : FriendRequestServiceBase
{
    public FriendRequestService(ILogger<FriendRequestService> logger, IMapper mapper, UserFriendServiceBase _userFriendService, FriendRequestRepositoryBase friendRequestRepository)
        : base(logger, mapper, _userFriendService, friendRequestRepository)
    {
    }
}

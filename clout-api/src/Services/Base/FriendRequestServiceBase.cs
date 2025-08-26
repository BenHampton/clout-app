// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using clout_api.Data.Dtos.FriendRequest;
using clout_api.Data.Models;
using clout_api.Data.Repository.Base;

namespace clout_api.Services.Base;

public abstract class FriendRequestServiceBase
{
    private readonly ILogger<FriendRequestService> _logger;

    private readonly IMapper _mapper;

    private readonly UserFriendServiceBase _userFriendService;

    private readonly FriendRequestRepositoryBase _friendRequestRepository;

    [Obsolete("For testing only")]
    protected FriendRequestServiceBase()
    {}

    protected FriendRequestServiceBase(ILogger<FriendRequestService> logger, IMapper mapper,
        UserFriendServiceBase userFriendService, FriendRequestRepositoryBase friendRequestRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _userFriendService = userFriendService;
        _friendRequestRepository = friendRequestRepository;
    }

    public virtual async Task<FriendRequestDto?> FindByIdAsync(int id)
    {
        var friendRequest = await _friendRequestRepository.FindByIdAsync(id);
        return _mapper.Map<FriendRequestDto>(friendRequest);
    }

    public virtual async Task<List<FriendRequestDto>?> FindAllByIdsAndUserIdAsync(int userId)
    {
        var friendRequests = await _friendRequestRepository.FindAllForUserId(userId);
        return _mapper.Map<List<FriendRequestDto>>(friendRequests);
    }

    public virtual async Task<List<FriendRequestDto>?> FindAllOutgoingFriendRequestsByUserId(int userId)
    {
        var friendRequests = await _friendRequestRepository.FindAllOutgoingFriendRequestsByUserId(userId);
        return _mapper.Map<List<FriendRequestDto>>(friendRequests);
    }

    public virtual async Task<List<FriendRequestDto>?> FindAllIncomingFriendRequestsByUserId(int userId)
    {
        var friendRequests = await _friendRequestRepository.FindAllIncomingFriendRequestsByUserId(userId);
        return _mapper.Map<List<FriendRequestDto>>(friendRequests);
    }

    public virtual async Task<ResponseFriendRequestDto?> CreateAsync(RequestFriendRequestDto requestFriendRequestDto)
    {

        var userFromRequestPostDto = _mapper.Map<FriendRequest>(requestFriendRequestDto);

        if (userFromRequestPostDto.UserIdOne > userFromRequestPostDto.UserIdTwo)
        {
            var initUserIdOne = userFromRequestPostDto.UserIdOne;
            var initUserIdTwo = userFromRequestPostDto.UserIdTwo;
            userFromRequestPostDto.UserIdOne = initUserIdTwo;
            userFromRequestPostDto.UserIdTwo = initUserIdOne;
        }

        bool exists = _friendRequestRepository.Exists(userFromRequestPostDto);
        if (exists)
        {
            return null;
        }

        var friendRequest = await _friendRequestRepository.CreateAsync(userFromRequestPostDto);

        var responseFriendRequestDto = _mapper.Map<ResponseFriendRequestDto>(friendRequest);
        responseFriendRequestDto.RelationshipType = RelationshipType.PENDING_OUTGOING;

        return responseFriendRequestDto;
    }

    public virtual async Task<ResponseFriendRequestDto?> UpdateAsync(RequestFriendRequestDto requestFriendRequestDto, int statusId)
    {

        if ((int)RelationshipType.APPROVED_REQUEST == statusId)
        {
            UserFriend userFriendOne = _mapper.Map<UserFriend>(requestFriendRequestDto);
            UserFriend userFriendTwo = _mapper.Map<UserFriend>(requestFriendRequestDto);
            userFriendTwo.UserId = requestFriendRequestDto.UserIdTwo;
            userFriendTwo.FriendId = requestFriendRequestDto.UserIdOne;


            var savedUserFriendOne = await _userFriendService.CreateAsync(userFriendOne);
            var savedUserFriendTwo = await _userFriendService.CreateAsync(userFriendTwo);

            if (savedUserFriendOne != null && savedUserFriendTwo != null)
            {
                await DeleteAllByUserIdOneAndUserIdTwoAsync(requestFriendRequestDto.UserIdOne, requestFriendRequestDto.UserIdTwo);
                var responseFriendRequestDto = _mapper.Map<ResponseFriendRequestDto>(savedUserFriendOne);
                responseFriendRequestDto.RelationshipType = RelationshipType.FRIEND;
                return responseFriendRequestDto;
            }

            return null;
        }

        return null;
    }

    public virtual async Task<ResponseFriendRequestDto?> DeleteByIdAndFriendIdAsync(int id, int friendId)
    {
        int userIdOne;
        int userIdTwo;
        if (id > friendId)
        {
            userIdOne = friendId;
            userIdTwo = id;
        }
        else
        {
            userIdOne = id;
            userIdTwo = friendId;
        }

        var friendRequest = await _friendRequestRepository.DeleteAllByUserIdOneAndUserIdTwoAsync(userIdOne, userIdTwo);

        var responseFriendRequestDto = _mapper.Map<ResponseFriendRequestDto>(friendRequest);
        responseFriendRequestDto.RelationshipType = RelationshipType.REQUEST;

        return responseFriendRequestDto;
    }

    private async Task<FriendRequest?> DeleteAllByUserIdOneAndUserIdTwoAsync(int userIdOne, int userIdTwo)
    {
        return await _friendRequestRepository.DeleteAllByUserIdOneAndUserIdTwoAsync(userIdOne, userIdTwo);
    }
}

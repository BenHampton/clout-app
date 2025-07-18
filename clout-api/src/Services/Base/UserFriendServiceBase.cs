// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using clout_api.Data.Dtos.User;
using clout_api.Data.Dtos.UserFriend;
using clout_api.Data.Models;
using clout_api.Data.Repository;
using clout_api.Data.Repository.Base;

namespace clout_api.Services.Base;

public abstract class UserFriendServiceBase
{
    private readonly ILogger<UserFriendService> _logger;

    private readonly IMapper _mapper;

    private readonly UserFriendRepositoryBase _userFriendRepository;

    [Obsolete("For testing only")]
    protected UserFriendServiceBase()
    {}

    protected UserFriendServiceBase(ILogger<UserFriendService> logger, IMapper mapper, UserFriendRepositoryBase userFriendRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _userFriendRepository = userFriendRepository;
    }

    public virtual async Task<UserFriendDto?> FindByIdAsync(int id)
    {
        var userFriend = await _userFriendRepository.FindByIdAsync(id);
        return _mapper.Map<UserFriendDto>(userFriend);
    }

    public virtual async Task<List<UserFriendDto>?> FindAllByUserIdAsync(int userId)
    {
        var userFriends = await _userFriendRepository.FindAllByUserIdAsync(userId);
        return _mapper.Map<List<UserFriendDto>>(userFriends);
    }

    public virtual async Task<UserFriend> CreateAsyncFromDto(UserFriendRequestDto userFriendRequestDto)
    {

        //create entry for both userId === userId and both userId === friendId

        var userFromRequestPostDto = _mapper.Map<UserFriend>(userFriendRequestDto);


        var friendFromRequestPostDto = _mapper.Map<UserFriend>(userFriendRequestDto);
        friendFromRequestPostDto.UserId = userFriendRequestDto.FriendId;
        friendFromRequestPostDto.FriendId = userFriendRequestDto.UserId;

        await _userFriendRepository.CreateAsync(friendFromRequestPostDto);
        return await _userFriendRepository.CreateAsync(userFromRequestPostDto);
    }

    public virtual async Task<UserFriend> CreateAsync(UserFriend userFriend)
    {
        return await _userFriendRepository.CreateAsync(userFriend);
    }

    public virtual async Task<UserFriend?> UpdateAsync(UserFriendRequestDto userFriendRequestDto, int statusId)
    {
        //create entry for both userId === userId and both userId === friendId

        var userFromRequestPostDto = _mapper.Map<UserFriend>(userFriendRequestDto);
        // userFromRequestPostDto.RelationshipTypeId = (int) RelationshipType.REQUEST;

        var friendFromRequestPostDto = _mapper.Map<UserFriend>(userFriendRequestDto);
        friendFromRequestPostDto.UserId = userFriendRequestDto.FriendId;
        friendFromRequestPostDto.FriendId = userFriendRequestDto.UserId;


        await _userFriendRepository.CreateAsync(friendFromRequestPostDto);
        return await _userFriendRepository.CreateAsync(userFromRequestPostDto);
    }

}

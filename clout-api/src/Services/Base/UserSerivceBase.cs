// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using clout_api.Data.Dtos.Friend;
using clout_api.Data.Dtos.FriendRequest;
using clout_api.Data.Dtos.User;
using clout_api.Data.Dtos.UserFriend;
using clout_api.Data.Models;
using clout_api.Data.Repository.Base;

namespace clout_api.Services.Base;

public abstract class UserServiceBase
{
    private readonly ILogger<UserService> _logger;

    private readonly IMapper _mapper;

    private readonly UserFriendServiceBase _userFriendService;

    private readonly FriendRequestServiceBase _friendRequestService;

    private readonly UserRepositoryBase _userRepository;

    [Obsolete("For testing only")]
    protected UserServiceBase()
    {}

    protected UserServiceBase(ILogger<UserService> logger, IMapper mapper,
        UserFriendServiceBase userFriendService, FriendRequestServiceBase friendRequestService,
        UserRepositoryBase userRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _userFriendService = userFriendService;
        _friendRequestService = friendRequestService;
        _userRepository = userRepository;
    }

    public virtual async Task<UserDto?> FindByIdAsync(int id)
    {

        var user = await _userRepository.FindByIdAsync(id);

        var userDto = _mapper.Map<UserDto>(user);

        if (userDto.Friends.Count <= 0)
        {
            return userDto;
        }

        List<UserDto> friends = await FindFriends(userDto.Friends);

        foreach (MiniFriendDto miniFriendDto in userDto.Friends)
        {
            var friendUserDto = friends.FirstOrDefault(f => f.Id == miniFriendDto.Id);
            if (friendUserDto != null)
            {
                miniFriendDto.FirstName = friendUserDto.FirstName;
                miniFriendDto.LastName = friendUserDto.LastName;
            }
        }

        return userDto;
    }

    private async Task<List<UserDto>> FindFriends(List<MiniFriendDto> friends)
    {
        var friendIds = friends
            .Select(f => f.Id)
            .ToList();

        var friendUserDtos = await FindAllByIdsAsync(friendIds);

        if (friendUserDtos == null || friendUserDtos.Count == 0)
        {
            return [];
        }

        return friendUserDtos;
    }

    public virtual async Task<List<UserDto>?> FindAllByIdsAsync(List<int> ids)
    {

        var users = await FindAllUsersByIdsAsync(ids);

        return _mapper.Map<List<UserDto>>(users);
    }

    private async Task<List<User>?> FindAllUsersByIdsAsync(List<int> ids)
    {
        return await _userRepository.FindAllByIdsAsync(ids);
    }

    public virtual async Task<List<MiniFriendDto>?> FindAllFriendsByIds(List<int> ids)
    {

        var users = await FindAllUsersByIdsAsync(ids);

        return _mapper.Map<List<MiniFriendDto>>(users);
    }

    public virtual async Task<User> CreateAsync(RequestUserDto requestUserDto)
    {

        var userFromRequestPostDto = _mapper.Map<User>(requestUserDto);
        return await _userRepository.CreateAsync(userFromRequestPostDto);
    }

    public virtual async Task<List<User>> CreateAllAsync(List<RequestUserDto> requestUserDtos)
    {

        var userFromRequestPostDtos = _mapper.Map<List<User>>(requestUserDtos);
        return await _userRepository.CreateAllAsync(userFromRequestPostDtos);
    }

    //todo Blocked status.. might need to think more about this and where to save it?
    // in the friend_request or user_friend or maybe a new table? idk
    public virtual async Task<List<SearchUserDto>?> SearchByFirstOrLastNameAsync(string name, int currentUserId)
    {
        var user = await _userRepository.SearchByFirstOrLastNameAsync(name);

        user = user?
            .Where(u => u.Id != currentUserId)
            .ToList();

        if (user == null || user.Count == 0)
        {
            return new List<SearchUserDto>();
        }

        List<SearchUserDto> searchUserDtos = _mapper.Map<List<SearchUserDto>>(user);

        var userIds = user.Select(u => u.Id).ToList();

        var userFriendDtos = await _userFriendService.FindAllUserFriendDtosByUserIdAsync(currentUserId);
        if (userFriendDtos is { Count: > 0 })
        {
            foreach (UserFriendDto userFriendDto in userFriendDtos)
            {
                var searchUserDto = searchUserDtos.Find(userDto => userDto.Id == userFriendDto.FriendId);
                if (searchUserDto != null)
                {
                    searchUserDto.IsFriend = true;
                    searchUserDto.RelationshipType = RelationshipType.FRIEND;
                }
            }
        }

        var allFriendRequests = await _friendRequestService.FindAllByIdsAndUserIdAsync(currentUserId);
        if (allFriendRequests is { Count: > 0 })
        {
            foreach (FriendRequestDto friendRequest in allFriendRequests)
            {
                //outgoing friend requests
                var outgoingSearchUserDto = searchUserDtos.Find(userDto =>
                    (userDto.Id == friendRequest.UserIdOne || userDto.Id == friendRequest.UserIdTwo) &&
                    friendRequest.Requestor == currentUserId);

                if (outgoingSearchUserDto != null)
                {
                    outgoingSearchUserDto.IsPendingOutGoing = true;
                    outgoingSearchUserDto.RelationshipType = RelationshipType.PENDING_OUTGOING;
                }

                //incoming friend requests
                var incomingSearchUserDto = searchUserDtos.Find(userDto =>
                    (userDto.Id == friendRequest.UserIdOne || userDto.Id == friendRequest.UserIdTwo) &&
                    friendRequest.Requestor != currentUserId);

                if (incomingSearchUserDto != null)
                {
                    incomingSearchUserDto.IsPendingIncoming = true;
                    incomingSearchUserDto.RelationshipType = RelationshipType.PENDING_INCOMING;
                }
            }
        }

        return searchUserDtos;
    }

}

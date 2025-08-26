// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using clout_api.Data.Dtos.Friend;
using clout_api.Data.Dtos.UserFriend;
using clout_api.Data.Models;
using clout_api.Data.Repository.Base;

namespace clout_api.Services.Base;

public abstract class UserFriendServiceBase
{
    private readonly ILogger<UserFriendService> _logger;

    private readonly IMapper _mapper;

    private readonly UserRepositoryBase _userRepository;

    private readonly FriendRequestRepositoryBase _friendRequestRepository;

    private readonly UserFriendRepositoryBase _userFriendRepository;

    [Obsolete("For testing only")]
    protected UserFriendServiceBase()
    {}

    protected UserFriendServiceBase(ILogger<UserFriendService> logger, IMapper mapper,
        UserRepositoryBase userRepository, FriendRequestRepositoryBase friendRequestRepository,
        UserFriendRepositoryBase userFriendRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _userRepository = userRepository;
        _friendRequestRepository = friendRequestRepository;
        _userFriendRepository = userFriendRepository;
    }

    public virtual async Task<FriendDto?> FindAllByFriendIdAndUserIdAsync(int friendId, int userId)
    {
        //find single friend
        var user = await _userRepository.FindByIdAsync(friendId);

        if (user == null)
        {
            return null;
        }

        RelationshipType? relationshipType = null;
        var userFriends = await FindByUserIdAndFriendIdAsync(userId, friendId);

        if (userFriends != null)
        {
            relationshipType = RelationshipType.FRIEND;
        }


        if (relationshipType == null)
        {
            var outgoingFriendRequest = await _friendRequestRepository.FindOutgoingFriendRequestsByFriendIdAndUserId(friendId, userId);

            if (outgoingFriendRequest != null)
            {
                relationshipType = RelationshipType.PENDING_OUTGOING;
            }
            else
            {
                var incomingFiendRequest = await _friendRequestRepository.FindIncomingFriendRequestsByFriendIdAndUserId(friendId, userId);
                if (incomingFiendRequest != null)
                {
                    relationshipType = RelationshipType.PENDING_INCOMING;
                }
            }
        }

        var friendDto = _mapper.Map<FriendDto>(user);
        friendDto.RelationshipType = relationshipType;

        return friendDto;
    }

    public async Task<UserFriend?> FindByUserIdAndFriendIdAsync(int userId, int friendId)
    {
        return await _userFriendRepository.FindByUserIdAndFriendIdAsync(userId, friendId);
    }

    public virtual async Task<List<MiniFriendDto>?> FindAllByUserIdAsync(int userId)
    {
        var userFriends = await FindAllUserFriendByUserIdAsync(userId);

        if (userFriends == null)
        {
            return null;
        }

        var friendIds = userFriends.Select(uf => uf.FriendId).ToList();

        var users = await _userRepository.FindAllByIdsAsync(friendIds);

        var miniFriendDtos = _mapper.Map<List<MiniFriendDto>>(userFriends);

        if (users == null)
        {
            return miniFriendDtos;
        }

        foreach (var friendDto in miniFriendDtos)
        {
            var user = users.FirstOrDefault(u => u.Id == friendDto.Id);
            if (user != null)
            {
                friendDto.FirstName = user.FirstName;
                friendDto.LastName = user.LastName;
            }
        }

        return miniFriendDtos;
    }

    public virtual async Task<List<UserFriendDto>?> FindAllUserFriendDtosByUserIdAsync(int userId)
    {
        var userFriends = await FindAllUserFriendByUserIdAsync(userId);
        return _mapper.Map<List<UserFriendDto>>(userFriends);
    }

    private async Task<List<UserFriend>?> FindAllUserFriendByUserIdAsync(int userId)
    {
       return await _userFriendRepository.FindAllByUserIdAsync(userId);
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

    public virtual async Task<List<UserFriend>?> DeleteByUserIdAndFriendIdAsync(int userId, int friendId)
    {
        var userFriend = await FindByUserIdAndFriendIdAsync(userId, friendId);
        var friendUserFriend = await FindByUserIdAndFriendIdAsync(userId, friendId);

        if (userFriend == null || friendUserFriend == null)
        {
            return null;
        }
        var userFriends = new List<UserFriend> { userFriend, friendUserFriend };
        return await _userFriendRepository.DeleteByUserFriendsAsync(userFriends);
    }

}

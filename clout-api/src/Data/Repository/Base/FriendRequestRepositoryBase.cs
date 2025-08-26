// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Linq.Expressions;
using clout_api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace clout_api.Data.Repository.Base;

public abstract class FriendRequestRepositoryBase
{
    private readonly PostgresContext _postgresContext;

    [Obsolete("For testing purposes only")]
    protected FriendRequestRepositoryBase() {}

    protected FriendRequestRepositoryBase(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    public virtual async Task<FriendRequest?> FindByIdAsync(int id)
    {
        return await _postgresContext.FriendRequests.FirstOrDefaultAsync(user => user.Id == id);
    }


    public virtual async Task<List<FriendRequest>?> FindAllForUserId(int userId)
    {
        return await _postgresContext.FriendRequests
            .Where(fr => (fr.UserIdOne == userId && fr.Requestor == userId) ||
                         (fr.UserIdTwo == userId && fr.Requestor == userId) ||
                         (fr.UserIdOne == userId && fr.Requestor != userId) ||
                         (fr.UserIdTwo == userId && fr.Requestor != userId))
            .AsNoTracking()
            .ToListAsync();
        // might not need to query by Requestor
        // return await _postgresContext.FriendRequests
        //     .Where(fr =>
        //         ((ids.Contains(fr.UserIdOne) && fr.Requestor == userId) || (ids.Contains(fr.UserIdTwo) && fr.Requestor == userId)) ||
        //         ((ids.Contains(fr.UserIdOne) && fr.Requestor != userId) || (ids.Contains(fr.UserIdTwo) && fr.Requestor != userId)))
        //     .AsNoTracking()
        //     .ToListAsync();
    }

    public virtual async Task<FriendRequest?> FindOutgoingFriendRequestsByFriendIdAndUserId(int friendId, int userId)
    {
        return await _postgresContext.FriendRequests
            .FirstOrDefaultAsync(fr => (fr.UserIdOne == friendId || fr.UserIdTwo == friendId)
                                       && fr.Requestor == userId);
    }

    public virtual async Task<FriendRequest?> FindIncomingFriendRequestsByFriendIdAndUserId(int friendId, int userId)
    {
        return await _postgresContext.FriendRequests
            .FirstOrDefaultAsync(fr => (fr.UserIdOne == userId || fr.UserIdTwo == userId)
                                       && fr.Requestor == friendId);
    }

    //todo rename outgoing pending friend requests
    public virtual async Task<List<FriendRequest>?> FindAllOutgoingFriendRequestsByUserId(int userId)
    {
        return await _postgresContext.FriendRequests
            .Where(fr => (fr.UserIdOne == userId && fr.Requestor == userId) ||
                         (fr.UserIdTwo == userId && fr.Requestor == userId) )
            .AsNoTracking()
            .ToListAsync();
    }

    //todo rename incoming pending friend requests
    public virtual async Task<List<FriendRequest>?> FindAllIncomingFriendRequestsByUserId(int userId)
    {
        return await _postgresContext.FriendRequests
            .Where(fr => (fr.UserIdOne == userId && fr.Requestor != userId) ||
                         (fr.UserIdTwo == userId && fr.Requestor != userId))
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual bool Exists(FriendRequest friendRequest)
    {
        return _postgresContext.FriendRequests.Any(fr => fr.UserIdOne == friendRequest.UserIdOne &&
                                                                fr.UserIdTwo == friendRequest.UserIdTwo &&
                                                                fr.Requestor == friendRequest.Requestor);
    }

    public virtual async Task<FriendRequest> CreateAsync(FriendRequest friendRequest)
    {
        await _postgresContext.FriendRequests.AddAsync(friendRequest);
        await _postgresContext.SaveChangesAsync();
        return friendRequest;
    }

    public async Task<FriendRequest?> DeleteAllByUserIdOneAndUserIdTwoAsync(int userIdOne, int userIdTwo)
    {
        var friendRequests = await _postgresContext.FriendRequests
            .Where(friendRequest => (friendRequest.UserIdOne == userIdOne && friendRequest.UserIdTwo == userIdTwo) ||
                                    friendRequest.UserIdOne == userIdTwo && friendRequest.UserIdTwo == userIdOne)
            .FirstOrDefaultAsync();

        if (friendRequests == null)
        {
            return null;
        }

        _postgresContext.FriendRequests.Remove(friendRequests);
        await _postgresContext.SaveChangesAsync();

        return friendRequests;
    }
}

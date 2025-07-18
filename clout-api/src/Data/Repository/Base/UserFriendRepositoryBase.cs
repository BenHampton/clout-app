// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace clout_api.Data.Repository.Base;

public abstract class UserFriendRepositoryBase
{
    private readonly PostgresContext _postgresContext;

    [Obsolete("For testing purposes only")]
    protected UserFriendRepositoryBase() {}

    protected UserFriendRepositoryBase(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    public virtual async Task<UserFriend?> FindByIdAsync(int id)
    {
        return await _postgresContext.UserFriends.FirstOrDefaultAsync(user => user.Id == id);
    }

    public virtual async Task<List<UserFriend>?> FindAllByUserIdAsync(int userId)
    {
        return await _postgresContext.UserFriends
            .Where(uf => uf.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<UserFriend> CreateAsync(UserFriend userFriend)
    {
        await _postgresContext.UserFriends.AddAsync(userFriend);
        await _postgresContext.SaveChangesAsync();
        return userFriend;
    }
}

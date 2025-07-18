// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace clout_api.Data.Repository.Base;

public abstract class UserRepositoryBase
{
    private readonly PostgresContext _postgresContext;

    [Obsolete("For testing purposes only")]
    protected UserRepositoryBase() {}

    protected UserRepositoryBase(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    public virtual async Task<User?> FindByIdAsync(int id)
    {
        return await _postgresContext.Users
            .Include(user => user.UserFriends)
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public virtual async Task<List<User>?> FindAllByIdsAsync(List<int> ids)
    {
        return await _postgresContext.Users
            .Where(user => ids.Contains(user.Id))
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<User> CreateAsync(User user)
    {
        await _postgresContext.Users.AddAsync(user);
        await _postgresContext.SaveChangesAsync();
        return user;
    }

    public virtual async Task<List<User>> CreateAllAsync(List<User> users)
    {
        await _postgresContext.Users.AddRangeAsync(users);
        await _postgresContext.SaveChangesAsync();
        return users;
    }

    public virtual async Task<List<User>?> SearchByFirstOrLastNameAsync(string name)
    {
        return await _postgresContext.Users
            .Where(user => user.FirstName.ToLower().Contains(name.ToLower()) ||
                           user.LastName.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

}

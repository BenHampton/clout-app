// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Models;
using clout_api.Helpers;
using Microsoft.EntityFrameworkCore;

namespace clout_api.Data.Repository.Base;

public abstract class PostRepositoryBase
{

    private readonly PostgresContext _postgresContext;

    [Obsolete("For testing purposes only")]
    public PostRepositoryBase() {}

    public PostRepositoryBase(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    public async Task<List<Post>> FindAllAsync()
    {
        return await _postgresContext.Posts
            .Include(c => c.Comments)
            .ToListAsync();
    }

    public async Task<List<Post>> FindAllByQueryAsync(QueryObject query)
    {
        var posts = _postgresContext.Posts
            .Include(c => c.Comments)
            .AsQueryable();

        // if (!string.IsNullOrWhiteSpace(query.CompanyName))
        // {
        //     posts = posts.Where(s => s.CompanyName.ToLower().Contains(query.CompanyName.ToLower()));
        // }
        //
        // if (!string.IsNullOrWhiteSpace(query.Symbol))
        // {
        //     posts = posts.Where(s => s.Symbol.ToLower().Contains(query.Symbol.ToLower()));
        // }
        //
        // if (!string.IsNullOrWhiteSpace(query.SortBy))
        // {
        //     if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
        //     {
        //         posts = query.IsDescending ? posts.OrderByDescending(s => s.Symbol) :
        //             posts.OrderBy(s => s.Symbol);
        //     }
        // }

        var skipNumber = (query.PageNo - 1) * query.PageSize;

        return await posts.Skip(skipNumber).Take(query.PageSize).ToListAsync();
    }

    public async Task<Post?> FindByIdAsyncWithComments(int id)
    {
        return await _postgresContext.Posts
            .Include(c => c.Comments)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Post?> FindByIdAsync(int id)
    {
        return await _postgresContext.Posts
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Post> CreateAsync(Post post)
    {
        await _postgresContext.Posts.AddAsync(post);
        await _postgresContext.SaveChangesAsync();
        return post;
    }

    public async Task<Post?> UpdateAsync(Post post)
    {
        // var existingPost = await _postgresContext.Posts.FirstOrDefaultAsync(s => s.Id == id);
        //
        // if (existingPost == null)
        // {
        //     return null;
        // }
        //
        // //todo move to service
        // existingPost.Title = post.Title;
        // existingPost.Content = post.Content;
        // await _postgresContext.Posts.Update(post);
        await _postgresContext.SaveChangesAsync();

        return post;
    }

    public async Task<Post?> DeleteByIdAsync(int id)
    {
        var post = await _postgresContext.Posts.FirstOrDefaultAsync(s => s.Id == id);

        if (post == null)
        {
            return null;
        }

        _postgresContext.Posts.Remove(post);
        await _postgresContext.SaveChangesAsync();

        return post;
    }
}

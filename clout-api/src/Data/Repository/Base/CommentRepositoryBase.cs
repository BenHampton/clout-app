// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace clout_api.Data.Repository.Base;

public abstract class CommentRepositoryBase
{
    private readonly PostgresContext _postgresContext;

    [Obsolete("For testing purposes only")]
    public CommentRepositoryBase() {}

    public CommentRepositoryBase(PostgresContext postgresContext)
    {
        _postgresContext = postgresContext;
    }

    public async Task<List<Comment>> FindAllAsync()
    {
        return await _postgresContext.Comments.ToListAsync();
    }

    public async Task<Comment?> FindByIdAsync(int id)
    {
        return await _postgresContext.Comments.FindAsync(id);
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        await _postgresContext.Comments.AddAsync(comment);
        await _postgresContext.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateAsync(int id, Comment comment)
    {
        var existingComment = await _postgresContext.Comments.FindAsync(id);

        if (existingComment == null)
        {
            return null;
        }

        // existingComment.Title = comment.Title;
        existingComment.Content = comment.Content;
        existingComment.PostId = comment.PostId;

        await _postgresContext.SaveChangesAsync();

        return existingComment;
    }

    public async Task<Comment?> DeleteByIdAsync(int id)
    {
        var comment = await _postgresContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

        if (comment == null)
        {
            return null;
        }

        _postgresContext.Remove(comment);
        await _postgresContext.SaveChangesAsync();

        return comment;
    }
}

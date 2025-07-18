// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Dtos.Comment;
namespace clout_api.Data.Dtos.Post;

public class PostDto
{

    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public List<CommentDto> Comments { get; set; } = new List<CommentDto>();
}

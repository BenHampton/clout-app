// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using clout_api.Data.Dtos.Post;

namespace clout_api.Data.Dtos.Comment;

public class CommentDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = string.Empty;

    public int? PostId { get; set; }
}

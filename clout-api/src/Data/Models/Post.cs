// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace clout_api.Data.Models;

public class Post
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; }

    public string Title { get; set; }

    public List<Comment> Comments { get; set; } = new List<Comment>();
}

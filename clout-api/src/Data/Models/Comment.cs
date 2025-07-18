// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace clout_api.Data.Models;

public class Comment
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = string.Empty;

    //convention
    public int? PostId { get; set; }

    public Post? Post { get; set; }
}

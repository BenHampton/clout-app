// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace clout_api.Data.Dtos.Post;

public class RequestPostDto
{
    [Required]
    public string Title { get; set; }

    [Required]
    public string Content { get; set; }

    [Required]
    public int UserId { get; set; }
}

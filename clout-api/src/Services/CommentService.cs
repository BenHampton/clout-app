// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using clout_api.Data.Repository;
using clout_api.Data.Repository.Base;
using clout_api.Services.Base;

namespace clout_api.Services;

public class CommentService : CommentServiceBase
{
    public CommentService(ILogger<CommentService> logger, IMapper mapper, CommentRepositoryBase commentRepository) : base(logger, mapper, commentRepository)
    {
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using clout_api.Data.Repository;
using clout_api.Data.Repository.Base;
using clout_api.Services.Base;

namespace clout_api.Services;

public class PostService : PostServiceBase
{
    public PostService(ILogger<PostService> logger, IMapper mapper, PostRepositoryBase postRepository) : base(logger, mapper, postRepository)
    {
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using clout_api.Data.Dtos.Post;
using clout_api.Data.Models;
using clout_api.Data.Repository;
using clout_api.Data.Repository.Base;
using clout_api.Exceptions;
using clout_api.Helpers;

namespace clout_api.Services.Base;

public abstract class PostServiceBase
{
    private readonly ILogger<PostService> _logger;

    private readonly IMapper _mapper;

    private readonly PostRepositoryBase _postRepository;


    [Obsolete("For testing only")]
    public PostServiceBase() { }

    public PostServiceBase(ILogger<PostService> logger, IMapper mapper, PostRepositoryBase postRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public virtual async Task<List<PostDto>> FindAllAsync()
    {
        //todo deferred execution
        var posts = await _postRepository.FindAllAsync();

        //todo do i need ToList
        // var postDtos = posts.Select(post => post.ToDto()).ToList();

        return _mapper.Map<List<PostDto>>(posts);;
    }

    public virtual async Task<List<PostDto>> FindAllByQueryAsync(QueryObject query)
    {
        //todo deferred execution
        var posts = await _postRepository.FindAllByQueryAsync(query);

        // var postDtos = posts.Select(stock => post.ToDto()).ToList();

        return _mapper.Map<List<PostDto>>(posts);;
    }

    public virtual async Task<PostDto> FindByIdAsync(int Id)
    {
        //todo deferred execution
        var post = await _postRepository.FindByIdAsyncWithComments(Id);

        if (post == null)
        {
            throw new NotFoundException($"Post with id: {Id} not found");
        }

        return _mapper.Map<PostDto>(post);
    }

    public virtual async Task<Post> CreateAsync(RequestPostDto requestPostDto)
    {

        var postFromRequestPostDto = _mapper.Map<Post>(requestPostDto);
        return await _postRepository.CreateAsync(postFromRequestPostDto);
    }

    private async Task<Post> FindById(int Id)
    {
        //todo deferred execution
        var post = await _postRepository.FindByIdAsync(Id);

        if (post == null)
        {
            throw new NotFoundException($"Post with id: {Id} not found");
        }

        return post;
    }

    public virtual async Task<PostDto?> UpdateAsync(int id, RequestPostDto requestPostDto)
    {

        var existingPost = await FindById(id);

        existingPost.Title = requestPostDto.Title;
        existingPost.Content = requestPostDto.Content;

        var post = await _postRepository.UpdateAsync(existingPost);

        return post == null ? null : _mapper.Map<PostDto>(post);
    }

    public virtual async Task<PostDto?> DeleteByIdAsync(int id)
    {
        var post = await _postRepository.DeleteByIdAsync(id);

        return _mapper.Map<PostDto>(post);
    }
}

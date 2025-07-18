// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AutoMapper;
using clout_api.Data.Dtos.Comment;
using clout_api.Data.Models;
using clout_api.Data.Repository;
using clout_api.Data.Repository.Base;

namespace clout_api.Services.Base;

public abstract class CommentServiceBase
{
    private readonly ILogger<CommentService> _logger;

    private readonly IMapper _mapper;

    private readonly CommentRepositoryBase _commentRepository;

    [Obsolete("For testing only")]
    public CommentServiceBase() { }

    public CommentServiceBase(ILogger<CommentService> logger, IMapper mapper, CommentRepositoryBase commentRepository)
    {
        _logger = logger;
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    public virtual async Task<List<CommentDto>> FindAllAsync()
    {
        var comments = await _commentRepository.FindAllAsync();

        //Select() is like stream.map()
        // var commentDto = comments.Select(c => c.ToDto()).ToList();

        return _mapper.Map<List<CommentDto>>(comments);
    }

    public virtual async Task<CommentDto?> FindByIdAsync(int id)
    {
        var comment = await _commentRepository.FindByIdAsync(id);

        return comment == null ? null : _mapper.Map<CommentDto>(comment);
    }

    public virtual async Task<CommentDto> CreateAsync(RequestCommentDto requestCommentDto)
    {
        var commentFromRequestCommentDto = _mapper.Map<Comment>(requestCommentDto);

        var comment = await _commentRepository.CreateAsync(commentFromRequestCommentDto);

        return _mapper.Map<CommentDto>(comment);
    }

    public virtual async Task<CommentDto?> UpdateAsync(int id, RequestCommentDto requestCommentDto)
    {
        var commentFromRequestDto = _mapper.Map<Comment>(requestCommentDto);

        var comment = await _commentRepository.UpdateAsync(id, commentFromRequestDto);

        return _mapper.Map<CommentDto>(comment);
    }

    public virtual async Task<CommentDto?> DeleteByIdAsync(int id)
    {
        var comment = await _commentRepository.DeleteByIdAsync(id);

        return _mapper.Map<CommentDto>(comment);
    }
}

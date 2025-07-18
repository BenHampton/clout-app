// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Dtos.Comment;
using clout_api.Data.Repository;
using clout_api.Services;
using clout_api.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace clout_api.Controllers;

[Route("/api/v1/comments")]
[ApiController]
public class CommentController : ControllerBase
{

    private readonly ILogger<CommentController> _logger;

    private readonly CommentServiceBase _commentService;

    public CommentController(ILogger<CommentController> logger, CommentServiceBase commentService)
    {
        _logger = logger;
        _commentService = commentService;
    }

    [HttpGet]
    public async Task<ActionResult> FindAll()
    {
        var comments = await _commentService.FindAllAsync();
        return Ok(comments);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> FindById(int id)
    {
        var comment = await _commentService.FindByIdAsync(id);

        if (comment == null)
        {
            return NotFound(id);
        }

        return Ok(comment);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] RequestCommentDto requestCommentDto)
    {

        if (!ModelState.IsValid) //comes from base controller
        {
            return BadRequest(ModelState);
        }

        //todo fix

        var comment = await _commentService.CreateAsync(requestCommentDto);

        return CreatedAtAction(nameof(FindById), new { id = comment.Id }, comment);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] RequestCommentDto requestCommentDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var comment = await _commentService.UpdateAsync(id, requestCommentDto);
        return Ok(comment);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteById([FromRoute] int id)
    {
        var comment = await _commentService.DeleteByIdAsync(id);

        if (comment == null)
        {
            return NotFound(id);
        }

        return NoContent();
    }
}

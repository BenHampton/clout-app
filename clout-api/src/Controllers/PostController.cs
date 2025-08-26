// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Dtos.Post;
using clout_api.Helpers;
using clout_api.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace clout_api.Controllers;

[Route("/api/v1/posts")]
[ApiController]
public class PostController : ControllerBase
{

    private readonly ILogger<PostController> _logger;

    private readonly PostServiceBase _postService;

    public PostController(ILogger<PostController> logger, PostServiceBase postService)
    {
        _logger = logger;
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var posts = await _postService.FindAllAsync();
        return Ok(posts);
    }

    //pageable & queryable
    [HttpGet("queryable")]
    public async Task<IActionResult> FindByQuery([FromQuery] QueryObject query)
    {
        // //todo deferred execution
        // var posts = await _postRepository.FindAllByQueryAsync(query);
        //
        // var postDto = posts.Select(post => post.ToDto());
        //
        // return Ok(posts);

        var posts = await _postService.FindAllByQueryAsync(query);
        return Ok();
    }

    //todo route constraints
    //todo model binding
    [HttpGet("{id:int}")]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {

        var post = await _postService.FindByIdAsync(id);

        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RequestPostDto requestPostDto)
    {

        if (!ModelState.IsValid) //comes from base controller
        {
            return BadRequest(ModelState);
        }

        var post = await _postService.CreateAsync(requestPostDto);

        return CreatedAtAction(nameof(FindById), new { id = post.Id }, post);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] RequestPostDto requestPostDto)
    {

        if (!ModelState.IsValid) //comes from base controller
        {
            return BadRequest(ModelState);
        }

        var post = await _postService.UpdateAsync(id, requestPostDto);

        return post == null ? NotFound(id) : Ok(post);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById([FromRoute] int id)
    {

        var post = await _postService.DeleteByIdAsync(id);

        return post == null ? NotFound(id) : NoContent();
    }
}

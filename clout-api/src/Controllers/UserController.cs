// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Dtos.User;
using clout_api.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace clout_api.Controllers;

[ApiController]
[Route("/api/v1/users")]
public class UserController: ControllerBase
{

    private readonly ILogger<UserController> _logger;

    private readonly UserServiceBase _userService;

    public UserController(UserServiceBase userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [EndpointSummary("Find all user by Ids")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<UserDto>>> FindAllByIds([FromQuery] List<int> ids)
    {
        var userDtos = await _userService.FindAllByIdsAsync(ids);
        return Ok(userDtos);
    }

    [HttpGet("search")]
    [EndpointSummary("Search users by first or last name")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<SearchUserDto>>> SearchByFirstOrLastName([FromQuery] string name, int userId)
    {
        var userDtos = await _userService.SearchByFirstOrLastNameAsync(name, userId);
        return Ok(userDtos);
    }

    [HttpGet("{id:int}")]
    [EndpointSummary("Find user by Id")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> FindById([FromRoute] int id)
    {
        var userDto = await _userService.FindByIdAsync(id);
        return Ok(userDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RequestUserDto requestUserDto)
    {

        if (!ModelState.IsValid) //comes from base controller
        {
            return BadRequest(ModelState);
        }

        var user = await _userService.CreateAsync(requestUserDto);

        return CreatedAtAction(nameof(FindById), new { id = user.Id }, user);
    }

    //TODO temp endpoint just to batch creating users
    [HttpPost("list")]
    public async Task<IActionResult> CreateAll([FromBody] List<RequestUserDto> requestUserDtos)
    {

        if (!ModelState.IsValid) //comes from base controller
        {
            return BadRequest(ModelState);
        }

        var useDtos = await _userService.CreateAllAsync(requestUserDtos);

        var ids = useDtos.Select(u => u.Id).ToList();

        return CreatedAtAction(nameof(FindAllByIds), ids, useDtos);
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Dtos.User;
using clout_api.Data.Dtos.UserFriend;
using clout_api.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace clout_api.Controllers;

[ApiController]
[Route("/api/v1/user-friends")]
public class UserFriendController: ControllerBase
{

    private readonly ILogger<UserFriendController> _logger;

    private readonly UserFriendServiceBase _UserFriendService;

    public UserFriendController(UserFriendServiceBase UserFriendService)
    {
        _UserFriendService = UserFriendService;
    }

    [HttpGet]
    [HttpGet("{id:int}")]
    [EndpointSummary("Find UserFriend by Id")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserFriendDto>> FindById(int id)
    {
        var userDto = await _UserFriendService.FindByIdAsync(id);
        return Ok(userDto);
    }

    // [HttpPost]
    // [EndpointSummary("Create UserFriend")]
    // [Produces("application/json")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<UserFriendDto>> CreateAsync([FromBody] UserFriendRequestDto userFriendRequestDto)
    // {
    //
    //     if (!ModelState.IsValid) //comes from base controller
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //     var user = await _UserFriendService.CreateAsync(userFriendRequestDto);
    //
    //     return CreatedAtAction(nameof(FindById), new { id = user.Id }, user);
    // }

    // [HttpPut]
    // [Route("{statusId:int}")]
    // [EndpointSummary("Create UserFriend")]
    // [Produces("application/json")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    // public async Task<ActionResult<UserFriendDto>> UpdateAsync([FromRoute] int statusId, [FromBody] UserFriendRequestDto userFriendRequestDto)
    // {
    //
    //     if (!ModelState.IsValid) //comes from base controller
    //     {
    //         return BadRequest(ModelState);
    //     }
    //
    //     var UserFriendDto = await _UserFriendService.UpdateAsync(userFriendRequestDto, statusId);
    //
    //     return UserFriendDto == null ? NotFound(UserFriendDto) : Ok(UserFriendDto);
    // }
}

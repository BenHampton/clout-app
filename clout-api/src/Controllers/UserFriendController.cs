// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Dtos.Friend;
using clout_api.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace clout_api.Controllers;

[ApiController]
[Route("/api/v1/user-friends")]
public class UserFriendController: ControllerBase
{

    private readonly ILogger<UserFriendController> _logger;

    private readonly UserFriendServiceBase _userFriendService;

    public UserFriendController(UserFriendServiceBase userFriendService)
    {
        _userFriendService = userFriendService;
    }

    [HttpGet]
    [EndpointSummary("Find UserFriend by Id")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<MiniFriendDto>>> FindAllByUserIdAsync([FromQuery] int userId)
    {
        var miniFriendDtos = await _userFriendService.FindAllByUserIdAsync(userId);
        return Ok(miniFriendDtos);
    }

    [HttpGet("user/{userId:int}")]
    [EndpointSummary("Find UserFriend by Id")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FriendDto>> FindByFriendIdAndUserIdAsync([FromRoute] int userId, [FromQuery] int friendId)
    {
        var friendDto = await _userFriendService.FindAllByFriendIdAndUserIdAsync(friendId, userId);
        return Ok(friendDto);
    }

    [HttpDelete("user/{userId:int}")]
    public async Task<IActionResult> DeleteByUserIdAndFriendIdAsync([FromRoute] int userId, [FromQuery] int friendId)
    {

        var friendRequests = await _userFriendService.DeleteByUserIdAndFriendIdAsync(userId, friendId);

        return friendRequests == null ? NotFound(friendId) : NoContent();
    }
}

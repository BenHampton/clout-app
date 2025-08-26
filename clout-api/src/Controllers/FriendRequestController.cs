// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using clout_api.Data.Dtos.FriendRequest;
using clout_api.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace clout_api.Controllers;

[ApiController]
[Route("/api/v1/friend-requests")]
public class FriendRequestController: ControllerBase
{

    private readonly ILogger<FriendRequestController> _logger;

    private readonly FriendRequestServiceBase _friendRequestService;

    public FriendRequestController(FriendRequestServiceBase friendRequestService)
    {
        _friendRequestService = friendRequestService;
    }

    [HttpGet]
    [HttpGet("{id:int}")]
    [EndpointSummary("Find FriendRequest by Id")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FriendRequestDto>> FindById(int id)
    {
        var friendRequestDto = await _friendRequestService.FindByIdAsync(id);
        return Ok(friendRequestDto);
    }

    [HttpPost]
    [EndpointSummary("Create FriendRequest")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseFriendRequestDto>> CreateAsync([FromBody] RequestFriendRequestDto requestFriendRequestDto)
    {
        if (!ModelState.IsValid) //comes from base controller
        {
            return BadRequest(ModelState);
        }

        var friendRequestDto = await _friendRequestService.CreateAsync(requestFriendRequestDto);

        if (friendRequestDto == null)
        {
            return BadRequest("Failed to create friend request");
        }

        return CreatedAtAction(nameof(FindById), new { id = friendRequestDto.Requestor },  friendRequestDto);
    }

    [HttpPut]
    [EndpointSummary("Update FriendRequest")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseFriendRequestDto>> UpdateAsync([FromQuery(Name = "statusId")] int statusId, [FromBody] RequestFriendRequestDto requestFriendRequestDto)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var responseFriendRequestDto = await _friendRequestService.UpdateAsync(requestFriendRequestDto, statusId);

        return responseFriendRequestDto == null ? NotFound(responseFriendRequestDto) : Ok(responseFriendRequestDto);
    }

    [HttpDelete("user/{id:int}")]
    public async Task<IActionResult> DeleteByIdAndFriendIdAsync([FromRoute] int id, [FromQuery] int friendId)
    {

        var friendRequest = await _friendRequestService.DeleteByIdAndFriendIdAsync(id, friendId);

        return friendRequest == null ? NotFound(id) : NoContent();
    }
}

using System.IdentityModel.Tokens.Jwt;
using Ebay.Application.Users.CreateUser;
using Ebay.Application.Users.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers.Users;

[Route("api/users")]
[Authorize]
public class UserController : ParentController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody]CreateUserCommand request)
    {
        var result = await Sender.Send(request);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("profile")]
    public async Task<ActionResult<ApiResponse<GetUserResponse>>> GetUser()
    {
        var result = await Sender.Send(new GetUserQuery(Email));

        if (result.IsFailure)
            return Unauthorized("User not found");

        return new OkObjectResult(new ApiResponse<GetUserResponse>(result.Value));
    }
}

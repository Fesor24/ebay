using System.IdentityModel.Tokens.Jwt;
using Ebay.Application.Users.CreateUser;
using Ebay.Application.Users.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers.Users;

[ApiController]
[Route("api/users")]
[Authorize]
public class UserController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> CreateUser([FromBody]CreateUserCommand request)
    {
        var result = await _sender.Send(request);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("profile")]
    public async Task<ActionResult<ApiResponse<GetUserResponse>>> GetUser()
    {
        string? email = HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == JwtRegisteredClaimNames.Email)?.Value;

        if (string.IsNullOrWhiteSpace(email))
            return Unauthorized("User not authorized");

        var result = await _sender.Send(new GetUserQuery(email));

        if (result.IsFailure)
            return Unauthorized("User not found");

        return new OkObjectResult(new ApiResponse<GetUserResponse>(result.Value));
    }
}

using Ebay.Application.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers.Auth;

[Route("api/auth")]
[ApiController]
public class AuthController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> Login(LoginCommand request)
    {
        var result = await _sender.Send(request);

        if (result.IsFailure)
            return new BadRequestObjectResult(
                new ApiResponse(new Domain.Abstractions.Error(result.Error.Code, result.Error.Message)));

        return new OkObjectResult(new ApiResponse<LoginResponse>(result.Value));
    }
}

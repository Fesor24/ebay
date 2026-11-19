using Ebay.Application.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers.Auth;

[Route("api/auth")]
public class AuthController(ISender sender) : ParentController
{
    private readonly ISender _sender = sender;

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> Login(LoginCommand request)
    {
        var result = await _sender.Send(request);

        if (result.IsFailure)
            return new BadRequestObjectResult(
                new ApiResponse(new Domain.Abstractions.Error(result.Error.Code, result.Error.Message)));

        return new OkObjectResult(new ApiResponse<LoginResponse>(result.Value));
    }
}

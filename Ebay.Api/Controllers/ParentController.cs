using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Ebay.Api.Controllers;

[ApiController]
public class ParentController: ControllerBase
{
    protected ISender Sender
    {
        get
        {
            return HttpContext.RequestServices.GetRequiredService<ISender>();
        }
    }

    protected Guid UserId
    {
        get
        {
            string? userId = HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException("User not logged in");

            return Guid.Parse(userId);
        }
    }

    protected string Email
    {
        get
        {
            string? email = HttpContext.User.Claims.FirstOrDefault(cl => cl.Type == JwtRegisteredClaimNames.Email)?.Value;

            if (string.IsNullOrWhiteSpace(email))
                throw new UnauthorizedAccessException("User not logged in");

            return email;
        }
    }
}

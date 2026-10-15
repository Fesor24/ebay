using Ebay.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers;

[ApiController]
public class ParentController : ControllerBase
{
    protected readonly ISender Sender;

    public ParentController()
    {
        Sender = HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}

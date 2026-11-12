using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers;

[ApiController]
public class ParentController(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;
}

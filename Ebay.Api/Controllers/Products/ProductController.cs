using Ebay.Application.Products.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers.Products;


[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetProduct(Guid productId)
    {
        var request = new GetProductQuery(productId);

        var result = await _sender.Send(request);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }
}

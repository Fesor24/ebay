using Ebay.Application.Products.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers.Products;


[ApiController]
[Route("api/products")]
[Authorize]
public class ProductController(ISender sender) : ParentController(sender)
{
    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetProduct(Guid productId)
    {
        var request = new GetProductQuery(productId);

        var result = await Sender.Send(request);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }
}

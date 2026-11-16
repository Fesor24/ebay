using Ebay.Application.Products.CreateProduct;
using Ebay.Application.Products.GetProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers.Products;

[Route("api/products")]
[Authorize]
public class ProductController(ISender sender) : ParentController
{
    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> Get(Guid productId)
    {
        var request = new GetProductQuery(productId);

        var result = await sender.Send(request);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand request)
    {
        request = request with
        {
            UserId = UserId
        };

        var result = await Sender.Send(request);

        if(result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}

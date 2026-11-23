using Ebay.Application.Cart.AddUpdateShoppingCart;
using Ebay.Application.Cart.DeleteShoppingCart;
using Ebay.Application.Cart.GetShoppingCart;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers.Cart;

[Route("api/cart")]
public class ShoppingController : ParentController
{
    [HttpGet("{cartId:guid}")]
    public async Task<IActionResult> GetShoppingCart([FromQuery] Guid cartId, CancellationToken cancellationToken)
    {
        var query = new GetShoppingCartQuery(cartId);

        var result = await Sender.Send(query, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> AddUpdateShoppingCart([FromBody] AddUpdateShoppingCartCommand command, 
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }

    [HttpDelete("{cartId:guid}")]
    public async Task<IActionResult> DeleteShoppingCart([FromQuery] Guid cartId, CancellationToken cancellationToken)
    {
        var command = new DeleteShoppingCartCommand(cartId);

        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest(result.Error);
    }
}

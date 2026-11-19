using Ebay.Application.Wishlist.CreateWishlist;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers.Wishlist;

[Route("api/wishlist")]
public class WishlistController : ParentController
{
    public async Task<IActionResult> CreateWishlist([FromBody] CreateWishlistCommand request, 
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}

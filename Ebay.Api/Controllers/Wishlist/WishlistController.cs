using Ebay.Application.Wishlist.CreateWishlist;
using Ebay.Application.Wishlist.GetWishlistByUser;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Api.Controllers.Wishlist;

[Route("api/wishlist")]
public class WishlistController : ParentController
{
    public async Task<IActionResult> CreateWishlist([FromBody] CreateWishlistCommand request, 
        CancellationToken cancellationToken)
    {
        request = request with
        {
            UserId = UserId
        };

        var result = await Sender.Send(request, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetWishlistByUser(CancellationToken cancellationToken)
    {
        var request = new GetWishlistByUserQuery(UserId);

        var result = await Sender.Send(request, cancellationToken);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}

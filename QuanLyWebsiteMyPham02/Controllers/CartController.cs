using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLMP.BLL;
using QLMP.Common.Req;
using QLMP.Common.Rsp;

namespace QLMP.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private CartSvc cartSvc;

        public CartController()
        {
            cartSvc = new CartSvc();
        }

        [HttpPost("add-item")]
        public IActionResult AddItemToCart(int userId, int productId, int quantity)
        {
            var res = cartSvc.AddItemToCart(userId, productId, quantity);
            return Ok(res);
        }

        [HttpPost("update-item-quantity")]
        public IActionResult UpdateItemQuantity(int userId, int productId, int quantity)
        {
            var res = cartSvc.UpdateItemQuantity(userId, productId, quantity);
            return Ok(res);
        }

        [HttpDelete("remove-item")]
        public IActionResult RemoveItemFromCart(int userId, int productId)
        {
            var res = cartSvc.RemoveItemFromCart(userId, productId);
            return Ok(res);
        }

        [HttpGet("get-cart")]
        public IActionResult GetCartByUserId(int userId)
        {
            var res = cartSvc.GetCartByUserId(userId);
            return Ok(res);
        }
    }

}

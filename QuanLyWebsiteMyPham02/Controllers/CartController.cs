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
        [HttpGet("get-all-orders")]
        public IActionResult GetAllOrders()
        {
            var res = cartSvc.GetAllOrders();
            return Ok(res);
        }
        [HttpGet("get-order-by-id")]
        public IActionResult GetOrderById(int orderId)
        {
            var res = cartSvc.GetOrderById(orderId);
            return Ok(res);
        }
        [HttpGet("sales-statistics-by-product-type")]
        public IActionResult GetSalesStatisticsByProductType()
        {
            var res = cartSvc.GetSalesStatisticsByProductType();
            return Ok(res);
        }
        [HttpPost("add-product")]
        public IActionResult AddProductToCart(int userId, int productId, int quantity)
        {
            var res = cartSvc.AddProductToCart(userId, productId, quantity);

            return Ok(res);
        }

        [HttpPost("place-order")]
        public IActionResult PlaceOrder(int cartId)
        {
            var res = cartSvc.PlaceOrder(cartId);
            return Ok(res);
        }

        [HttpGet("get-cart-by-id")]
        public IActionResult GetCartById(int cartId)
        {
            var res = cartSvc.GetCartById(cartId);
            return Ok(res);
        }

        [HttpDelete("remove-product")]
        public IActionResult RemoveProductFromCart(int cartItemId)
        {
            var res = cartSvc.RemoveProductFromCart(cartItemId);
            return Ok(res);
        }
    }

}

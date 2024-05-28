using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("get-order-by-Userid")]
        public IActionResult GetOrderById(int userId)
        {
            var res = cartSvc.GetOrderById(userId);
            return Ok(res);
        }
        [HttpGet("get-order-by-orderid")]
        public IActionResult GetOrderByOrderId(int orderId)
        {
            var res = cartSvc.GetOrderByOrderId(orderId);
            return Ok(res);
        }

        [HttpGet("ThongKeGiaTheoLSP")]
        [Authorize(Roles = "admin")]
        public IActionResult GetSalesStatisticsByProductType()
        {
            var res = cartSvc.GetSalesStatisticsByProductType();
            return Ok(res);
        }
        [HttpGet("Thong-Ke")]
        [Authorize(Roles = "admin")]
        public IActionResult ThongKe()
        {
            var res = cartSvc.GetMonthlySalesStatistics();
            return Ok(res);
        }
        [HttpGet("Get-Recent-Order")]
        [Authorize(Roles = "admin")]
        public IActionResult GetRecentOrders()
        {
            var res = cartSvc.GetRecentOrders();
            return Ok(res);
        }
        [HttpPost("add-product")]
        public IActionResult AddProductToCart(AddToCartReq addToCartReq)
        {
            var res = cartSvc.AddProductToCart(addToCartReq.UserId,addToCartReq.ProductId,addToCartReq.Quantity);

            return Ok(res);
        }

        [HttpPost("place-order")]
        public IActionResult PlaceOrder([FromBody] PlaceOrderReq request)
        {
            var res = cartSvc.PlaceOrder(request.UserId);
            return Ok(res);
        }

        [HttpGet("get-cart-by-id")]
        public IActionResult GetCartById(int userId)
        {
            var res = cartSvc.GetCartById(userId);
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

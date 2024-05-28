using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QLMP.Common.Req;
using System.Text;

namespace FrontEnd.Controllers
{
    public class GioHangController : Controller
    {
        private readonly HttpClient _httpClient;

        public GioHangController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var role = HttpContext.Session.GetString("Role");

            if (userId == null)
            {
                return Redirect("/Login");
            }

            if (role == "admin")
            {
                return Redirect("/");
            }

            var addToCartReq = new
            {
                UserId = int.Parse(userId),
                ProductId = id,
                Quantity = 1
            };

            var json = JsonConvert.SerializeObject(addToCartReq);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("Cart/add-product", content);

            if (response.IsSuccessStatusCode)
            {
                return Redirect("/GioHang");
            }
            else
            {
                // Log error or return a specific error view
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}");
                return Redirect("/");
            }
        }
       
        public async Task<IActionResult> Index()
        {

            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Login");
            }
            CartReq cartReq = null;
            HttpResponseMessage response = await _httpClient.GetAsync($"Cart/get-cart-by-id?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                // Parse the JSON object
                JObject jsonResponse = JObject.Parse(responseData);

                // Check if the "data" property exists and is not null
                if (jsonResponse["data"] != null)
                {
                    // Deserialize the data into a CartReq object
                    cartReq = jsonResponse["data"].ToObject<CartReq>();
                }
            }

            if (cartReq == null)
            {
                // Handle the case where the cart is not found or the response is invalid
                return RedirectToAction("Index", "Home");
            }

            return View(cartReq);
        }
        public async Task<IActionResult> PlaceOrder()
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return Redirect("/Login");
            }

            // Create the JSON payload
            var json = new { UserId = userId };
            var content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("Cart/place-order", content);

            if (response.IsSuccessStatusCode)
            {
                return Redirect("/CheckOut");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}

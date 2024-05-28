using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FrontEnd.Controllers
{
    public class CheckOutController : Controller
    {

        private readonly HttpClient _httpClient;

        public CheckOutController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (userId == null)
            {
                return Redirect("/Login");
            }
            HoaDonVM hoaDonVM = null;
            HttpResponseMessage response = await _httpClient.GetAsync($"Cart/get-order-by-Userid?userId={userId}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                // Parse the JSON object
                JObject jsonResponse = JObject.Parse(responseData);

                // Check if the "data" property exists and is not null
                if (jsonResponse["data"] != null)
                {
                    // Deserialize the data into a CartReq object
                    hoaDonVM = jsonResponse["data"].ToObject<HoaDonVM>();
                }
            }

            if (hoaDonVM == null)
            {
                // Handle the case where the cart is not found or the response is invalid
                return Redirect("/");
            }

            return View(hoaDonVM);
        }

    }
}

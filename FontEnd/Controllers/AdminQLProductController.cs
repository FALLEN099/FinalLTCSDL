using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using FrontEnd.Models;
namespace FrontEnd.Controllers
{
    public class AdminQLProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminQLProductController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SanPhamVM> products = new List<SanPhamVM>();
            HttpResponseMessage response = await _httpClient.GetAsync("SanPham/get-all");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                // Parse the JSON object
                JObject jsonResponse = JObject.Parse(responseData);

                // Check if the "data" property exists and if it is an array
                if (jsonResponse["data"] != null && jsonResponse["data"].Type == JTokenType.Array)
                {
                    // Deserialize the array into a list of SanPhamVM
                    products = jsonResponse["data"].ToObject<List<SanPhamVM>>();
                }
            }

            return View(products);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"SanPham/Delete-Product?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Xử lý lỗi
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}

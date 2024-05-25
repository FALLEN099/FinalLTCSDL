using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using FrontEnd.Models;
namespace FrontEnd.Controllers
{
    public class AdminLoaiSanPhamController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminLoaiSanPhamController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LoaiSanPhamVM> products = new List<LoaiSanPhamVM>();
            HttpResponseMessage response = await _httpClient.GetAsync("LoaiSP/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                // Parse the JSON object
                JObject jsonResponse = JObject.Parse(responseData);

                // Check if the "data" property exists and if it is an array
                if (jsonResponse["data"] != null && jsonResponse["data"].Type == JTokenType.Array)
                {
                    // Deserialize the array into a list of LoaiSanPhamVM
                    products = jsonResponse["data"].ToObject<List<LoaiSanPhamVM>>();
                }
                else
                {
                    // Handle the case where the "data" property does not exist or is not an array
                    // You might want to log an error or handle it in some other way
                    // For example:
                    // ModelState.AddModelError("", "No data received from the API or the data is not in the expected format.");
                }
            }
            else
            {
                // Handle the case where the API request was not successful
                // You might want to log an error or handle it in some other way
                // For example:
                // ModelState.AddModelError("", "Failed to retrieve data from the API. Status code: " + response.StatusCode);
            }

            return View(products);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteCateProduct(string id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"LoaiSP/DeletaById?id={id}");
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

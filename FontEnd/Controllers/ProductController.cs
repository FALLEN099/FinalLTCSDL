using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using FontEnd.Models;
using Newtonsoft.Json.Linq;
using FrontEnd.Models;
using System.Threading.Tasks; // Thêm namespace này cho Task

namespace FrontEnd.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public ProductController()
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

        [HttpGet]
        public async Task<IActionResult> FilterByCategory(string categoryName)
        {
            List<SanPhamVM> products = new List<SanPhamVM>();
            HttpResponseMessage response = await _httpClient.GetAsync("SanPham/get-all");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                // Deserialize directly into a list of SanPhamVM
                products = JsonConvert.DeserializeObject<List<SanPhamVM>>(responseData);
            }

            // Filter products based on categoryName
            List<SanPhamVM> filteredProducts = products.Where(p => p.TenLoaiSp != null && p.TenLoaiSp.Equals(categoryName)).ToList();
            return View(filteredProducts); // Return the filtered result to the FilteredProducts view
        }
        [HttpGet]
        public async Task<IActionResult> Search(string productName)
        {
            List<SanPhamVM> products = await GetAllProductsAsync();
            List<SanPhamVM> searchedProducts = products.Where(p => p.TenSp != null && p.TenSp.ToLower().Contains(productName.ToLower())).ToList();
            return View("Index", searchedProducts); // Return the searched result to the Index view
        }

        private async Task<List<SanPhamVM>> GetAllProductsAsync()
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
            return products;
        }
    }
}

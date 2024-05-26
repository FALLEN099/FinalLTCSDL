using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using FontEnd.Models;
using Newtonsoft.Json.Linq;
using FrontEnd.Models;
using System.Threading.Tasks; // Thêm namespace này cho Task
using System.Text;

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
            HttpResponseMessage response = await _httpClient.GetAsync($"SanPham/search-by-category-name?categoryName={categoryName}");
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

            return View(products); // Return the filtered result to the FilteredProducts view
        }
        
        //public async Task<IActionResult> Search()
        //{

        //        return View(); // Initially return an empty list
            
        //}
        [HttpGet]
        public async Task<IActionResult> Search(string productName)
        {
            List<SanPhamVM> products = new List<SanPhamVM>();
            HttpResponseMessage response = await _httpClient.GetAsync($"SanPham/Tim Kiem-product?keyword={productName}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseData);

                if (jsonResponse["data"] != null && jsonResponse["data"].Type == JTokenType.Array)
                {
                    products = jsonResponse["data"].ToObject<List<SanPhamVM>>();
                }
            }

            return View(products);
        }
        //var jsonContent = JsonConvert.SerializeObject(productName);
        //var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        //HttpResponseMessage response = await _httpClient.GetAsync("SanPham/Tim Kiem-product", content);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var responseData = await response.Content.ReadAsStringAsync();

        //// Parse the JSON object
        //JObject jsonResponse = JObject.Parse(responseData);

        //        // Check if the "data" property exists and if it is an array
        //        if (jsonResponse["data"] != null && jsonResponse["data"].Type == JTokenType.Array)
        //        {
        //            // Deserialize the array into a list of SanPhamVM
        //            products = jsonResponse["data"].ToObject<List<SanPhamVM>>();
        //        }
    //}
        
    }
}

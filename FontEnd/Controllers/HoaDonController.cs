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
    public class HoaDonController : Controller
    {
        private readonly HttpClient _httpClient;

        public HoaDonController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<HoaDonVM> hoaDons = new List<HoaDonVM>();
            HttpResponseMessage response = await _httpClient.GetAsync("Cart/get-all-orders");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                JObject jsonResponse = JObject.Parse(responseData);

                if (jsonResponse["data"] != null && jsonResponse["data"].Type == JTokenType.Array)
                {
                    hoaDons = jsonResponse["data"].ToObject<List<HoaDonVM>>();
                }
            }

            return View(hoaDons);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            HoaDonVM hoaDonVM = null;
            HttpResponseMessage response = await _httpClient.GetAsync($"Cart/get-order-by-orderid?orderId={id}");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                JObject jsonResponse = JObject.Parse(responseData);

                if (jsonResponse["data"] != null)
                {
                    hoaDonVM = jsonResponse["data"].ToObject<HoaDonVM>();
                }
            }

            return View(hoaDonVM); 
        }
        
    }
}

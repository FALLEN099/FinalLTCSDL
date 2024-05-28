using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FrontEnd.Controllers
{
    public class HomeAdminController : Controller
    {
        private readonly HttpClient _httpClient;

        public HomeAdminController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            var role = HttpContext.Session.GetString("Role");
            if ((role != "admin"))
            {
                return Redirect("/");
            }
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var dashboardViewModel = new DashboardVM();
            HttpResponseMessage response = await _httpClient.GetAsync("Cart/Thong-Ke");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(responseData);
                if (jsonResponse["data"] != null && jsonResponse["data"].Type == JTokenType.Array)
                {
                    dashboardViewModel.ThongKeList = jsonResponse["data"].ToObject<List<ThongKeVM>>();
                }
            }

            HttpResponseMessage response1 = await _httpClient.GetAsync("Cart/Get-Recent-Order");
            if (response1.IsSuccessStatusCode)
            {
                var responseData1 = await response1.Content.ReadAsStringAsync();
                JObject jsonResponse1 = JObject.Parse(responseData1);
                if (jsonResponse1["data"] != null && jsonResponse1["data"].Type == JTokenType.Array)
                {
                    dashboardViewModel.RecentOrders = jsonResponse1["data"].ToObject<List<RecentOrderVM>>();
                }
            }
            HttpResponseMessage response2 = await _httpClient.GetAsync("Cart/ThongKeGiaTheoLSP");
            if (response1.IsSuccessStatusCode)
            {
                var responseData2 = await response2.Content.ReadAsStringAsync();
                JObject jsonResponse2 = JObject.Parse(responseData2);
                if (jsonResponse2["data"] != null && jsonResponse2["data"].Type == JTokenType.Array)
                {
                    dashboardViewModel.ThongKeSaleByCateVM = jsonResponse2["data"].ToObject<List<ThongKeSaleByCateVM>>();
                }
            }

            return View(dashboardViewModel);
        }
    }
}

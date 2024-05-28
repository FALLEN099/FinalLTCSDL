using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FrontEnd.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<KhachHangVM> khachHangVMs = new List<KhachHangVM>();
            HttpResponseMessage response = await _httpClient.GetAsync("KhachHang/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                JObject jsonResponse = JObject.Parse(responseData);

                if (jsonResponse["data"] != null && jsonResponse["data"].Type == JTokenType.Array)
                {
                    khachHangVMs = jsonResponse["data"].ToObject<List<KhachHangVM>>();
                }
            }

            return View(khachHangVMs);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int uid)
        {
            var token = HttpContext.Session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"User/DeleteUserById?id={uid}");
            if (response.IsSuccessStatusCode)
            {
                return Redirect("/HomeAdmin");
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}

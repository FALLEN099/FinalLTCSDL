using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QLMP.Common.Req;
using System.Text;

namespace FrontEnd.Controllers
{
    public class Register : Controller
    {
        private readonly HttpClient _httpClient;

        public Register()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DangKy(UserReq uerreq)
        {
            var jsonContent = JsonConvert.SerializeObject(uerreq);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("User/Register", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {

                return View(uerreq);
            }
        }
    }
}

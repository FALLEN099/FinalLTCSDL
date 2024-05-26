using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FrontEnd.Models; // Import namespace chứa model của sản phẩm
using QLMP.Common.Req;
using Newtonsoft.Json;
using QLMP.Common.Rsp;
using System.IdentityModel.Tokens.Jwt;

namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;

        public LoginController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginReq loginReq)
        {
            var jsonContent = JsonConvert.SerializeObject(loginReq);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("User/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var singleRsp = JsonConvert.DeserializeObject<SingleRsp>(jsonResponse);

                if (singleRsp.Success)
                {
                    string token = singleRsp.Data.ToString();
                    HttpContext.Session.SetString("Token", token);

                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);
                    var username = jwtToken.Claims.First(claim => claim.Type == "UserName").Value;
                    var userId = jwtToken.Claims.First(claim => claim.Type == "Id").Value;
                    var role = jwtToken.Claims.First(claim => claim.Type == "Role").Value;
                    HttpContext.Session.SetString("Username", username);
                    HttpContext.Session.SetString("UserId", userId);
                    HttpContext.Session.SetString("Role", role);

                    return Redirect("/");
                }
            }

            return View(loginReq);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}




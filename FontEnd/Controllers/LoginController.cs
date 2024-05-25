//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using FrontEnd.Models; // Import namespace chứa model của sản phẩm

//namespace FrontEnd.Controllers
//{
//    public class LoginController : Controller
//    {
//        private readonly HttpClient _httpClient;

//        public LoginController()
//        {
//            _httpClient = new HttpClient();
//            _httpClient.BaseAddress = new Uri("https://your-api-url.com/"); // Thay đổi URL API của bạn
//        }

//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}

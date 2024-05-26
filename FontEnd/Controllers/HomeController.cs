using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using FontEnd.Models;
using Newtonsoft.Json.Linq;
using FrontEnd.Models;

namespace FontEnd.Controllers
{
    public class HomeController : Controller
    {
        //private readonly HttpClient _httpClient;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _httpClient = new HttpClient();
        //    _httpClient.BaseAddress = new Uri("https://localhost:7289/Views/Home");
        //}
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7289/api/");
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Username = username;
            return View();
        }
    }
}
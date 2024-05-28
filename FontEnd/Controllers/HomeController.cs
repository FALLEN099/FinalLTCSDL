using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using FontEnd.Models;
using Newtonsoft.Json.Linq;
using FrontEnd.Models;

using QLMP.BLL;
using QLMP.DAL.Models;
namespace FontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly SanPhamSvc _anPhamSvc = new SanPhamSvc();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7289/api/");
        }
        [HttpGet]
        public async Task<IActionResult>  Index()
        {
            var products = _anPhamSvc.GetAllProduct();
            if (products == null)
            {
                return View("Error");
            }

            return View(products);
        }
    }
}
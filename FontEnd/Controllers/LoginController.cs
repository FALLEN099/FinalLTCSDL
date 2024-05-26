﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FrontEnd.Models; // Import namespace chứa model của sản phẩm
using QLMP.Common.Req;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Index(UserReq uerreq)
        {
            var jsonContent = JsonConvert.SerializeObject(uerreq);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("User/Login", content);

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

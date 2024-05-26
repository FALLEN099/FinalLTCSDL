using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FrontEnd.Models;
using QLMP.Common.Req;

namespace FrontEnd.Controllers
{
    public class AdminAddProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminAddProductController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        // Phương thức xử lý việc thêm sản phẩm
        [HttpPost]
        public async Task<IActionResult> CreateProduct(SanPhamReq sanPhamReq)
        {
            var jsonContent = JsonConvert.SerializeObject(sanPhamReq);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("SanPham/create-product", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminQLProduct");
            }
            else
            {
              
                return View(sanPhamReq);
            }
        }
    }
}

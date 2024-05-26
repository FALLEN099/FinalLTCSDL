using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using FrontEnd.Models; // Import namespace chứa model của sản phẩm
using FontEnd.Models;
using QLMP.Common.Req;
using System.Text;



namespace FrontEnd.Controllers
{
    public class AdminUpdateProductController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminUpdateProductController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }

        public async Task<IActionResult> Index(int id)
        {
            // Gửi yêu cầu GET để lấy thông tin sản phẩm từ API
            HttpResponseMessage response = await _httpClient.GetAsync($"SanPham/GetById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                var productData = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<SanPhamReq>(productData);
                return View(product); // Trả về view với dữ liệu sản phẩm
            }
            else
            {
                // Xử lý lỗi khi yêu cầu không thành công
                return StatusCode((int)response.StatusCode);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Index(int id, SanPhamReq model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"SanPham/Update-Product?Id={id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminQLProduct");
            }
            else
            {

                return View(model);
            }
        }

    }
}

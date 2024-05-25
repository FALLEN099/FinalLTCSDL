using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using FrontEnd.Models; // Import namespace chứa model của sản phẩm
using FontEnd.Models;
//using AspNetCore;


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
                // Đọc dữ liệu phản hồi và chuyển đổi thành đối tượng sản phẩm
                var productData = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<SanPhamVM>(productData);

                // Chuyển đổi đối tượng sản phẩm thành đối tượng ViewModel tương ứng
                var viewModel = new SanPhamVM
                {
                    maSp = product.maSp,
                    TenSp = product.TenSp,
                    Gia = product.Gia,
                    TenLoaiSp = product.TenLoaiSp
                    // Thêm các thuộc tính khác tương ứng với ViewModel của bạn
                    // ...
                };

                return View(viewModel); // Trả về view với dữ liệu sản phẩm
            }
            else
            {
                // Xử lý lỗi khi yêu cầu không thành công
                return StatusCode((int)response.StatusCode);
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using FrontEnd.Models;
using QLMP.Common.Req;
using System.Text;
using Newtonsoft.Json;

namespace FrontEnd.Controllers
{
    public class AdminLoaiSanPhamController : Controller
    {
        private readonly HttpClient _httpClient;

        public AdminLoaiSanPhamController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<LoaiSanPhamVM> products = new List<LoaiSanPhamVM>();
            HttpResponseMessage response = await _httpClient.GetAsync("LoaiSP/GetAll");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();

                // Parse the JSON object
                JObject jsonResponse = JObject.Parse(responseData);

                // Check if the "data" property exists and if it is an array
                if (jsonResponse["data"] != null && jsonResponse["data"].Type == JTokenType.Array)
                {
                    // Deserialize the array into a list of LoaiSanPhamVM
                    products = jsonResponse["data"].ToObject<List<LoaiSanPhamVM>>();
                }
                else
                {
                    
                   ModelState.AddModelError("", "No data received from the API or the data is not in the expected format.");
                }
            }
            else
            {
               
                ModelState.AddModelError("", "Failed to retrieve data from the API. Status code: " + response.StatusCode);
            }

            return View(products);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCateProduct(string id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"LoaiSP/DeletaById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Xử lý lỗi
                return StatusCode((int)response.StatusCode);
            }
        }
        public IActionResult CreateCate()
        {
            return View();
        }

        // Phương thức xử lý việc thêm sản phẩm
        [HttpPost]
        public async Task<IActionResult> CreateCate(LoaiSpReq loaiSpReq)
        {
            var jsonContent = JsonConvert.SerializeObject(loaiSpReq);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("LoaiSP/Create", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminLoaiSanPham");
            }
            else
            {
                // Xử lý lỗi khi yêu cầu không thành công
                return View(loaiSpReq);
            }
        }
        //sua
        public async Task<IActionResult> Update(int id)
        {
            // Gửi yêu cầu GET để lấy thông tin sản phẩm từ API
            HttpResponseMessage response = await _httpClient.GetAsync($"LoaiSP/GetById?id={id}");
            if (response.IsSuccessStatusCode)
            {
                // Đọc dữ liệu phản hồi và chuyển đổi thành đối tượng sản phẩm
                var productData = await response.Content.ReadAsStringAsync();
                var cate = JsonConvert.DeserializeObject<LoaiSpReq>(productData);
                return View(cate);
            }
            else
            {
               
                return StatusCode((int)response.StatusCode);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, LoaiSpReq model)
        {
            var jsonContent = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PutAsync($"LoaiSP/Update?Id={id}", content);

            if (response.IsSuccessStatusCode)
            {
              
                return RedirectToAction("Index", "AdminLoaiSanPham");
            }
            else
            {
               
                return View(model);
            }
        }


    }
}

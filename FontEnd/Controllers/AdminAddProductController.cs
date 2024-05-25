using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FrontEnd.Models;
using QLMP.Common.Req;

public class AdminAddProductController : Controller
{
    private readonly HttpClient _httpClient;

    public AdminAddProductController(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://localhost:7279/api/");
    }

 
    public async Task<IActionResult> Index(SanPhamVM model)
    {

        HttpResponseMessage response = await _httpClient.GetAsync("SanPham/create-product");
        if (ModelState.IsValid)
        {
            var productData = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<SanPhamVM>(productData);
            var viewModel = new SanPhamVM
            {
                maSp = product.maSp,
                TenSp = product.TenSp,
                Gia = product.Gia,
                TenLoaiSp = product.TenLoaiSp
                // Thêm các thuộc tính khác nếu cần
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");
            HttpResponseMessage postResponse = await _httpClient.PostAsync("SanPham/Create-Product", jsonContent);

            if (postResponse.IsSuccessStatusCode)
            {
                return RedirectToAction("AdminQLProduct"); // Điều hướng về trang danh sách sản phẩm sau khi thêm thành công
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Thêm sản phẩm không thành công");
            }
        }
        return View(model);
    }
}


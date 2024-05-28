using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLMP.BLL;
using QLMP.Common.Req;
using QLMP.Common.Rsp;
using QLMP.DAL.Models;

namespace QLMP.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private SanPhamSvc sanPhamSvc;
        public SanPhamController()
        {
            sanPhamSvc = new SanPhamSvc();
        }
        [HttpGet("GetById")]
        public IActionResult GetById( int id)
        {
            var res = new SingleRsp();
            res = sanPhamSvc.Read(id);
            return Ok(res);
        }
        [HttpGet("get-all")]
        public IActionResult getAllProduct()
        {
            var res = new SingleRsp();

            var products = sanPhamSvc.All.Select(p => new
            {
                MaSp=p.MaSp,
                TenSp = p.TenSp,
                Gia = p.Gia,
                TenLoaiSp=p.MaLoaiSpNavigation.TenLoaiSp,
                Image = p.HinhAnh 
            }).ToList();

            res.Data = products;
            return Ok(res);

        }
        [HttpPut("Update-Product")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateProduct(int Id,SanPhamReq sanPhamReq)
        {
            var res = sanPhamSvc.UpdateProduct(Id,sanPhamReq);
            res.Data = sanPhamReq;
            return Ok(res);
        }
        [HttpPost("create-product")]
        [Authorize(Roles = "admin")]
        public IActionResult CreateProduct([FromBody] SanPhamReq sanPhamReq)
        {
            var res = sanPhamSvc.CreateProduct(sanPhamReq);
            res.Data = sanPhamReq;
            return Ok(res);
        }
        [HttpGet("search-product")]
        public IActionResult SearchProduct([FromBody] SearchProductReq searchProductReq)
        {
            var res = new SingleRsp();
            res = sanPhamSvc.SearchProduct(searchProductReq);
            return Ok(res);
        }
        [HttpGet("Tim Kiem-product")]
        public IActionResult TimKiem(string keyword)
        {
            var res = new SingleRsp();
            res = sanPhamSvc.TimKiem(keyword);
            return Ok(res);
        }
        [HttpGet("search-by-category-name")]
        public IActionResult SearchProductByCategoryName(string categoryName)
        {
            var res = sanPhamSvc.SearchProductByCategoryName(categoryName);
            return Ok(res);
        }
        [HttpDelete("Delete-Product")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteProduct(int id)
        {
            var res = sanPhamSvc.Remove(id);
            return Ok(res);
        }
    }
}

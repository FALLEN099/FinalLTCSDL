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
        [HttpGet("get-all")]
        public IActionResult getAllProduct()
        {
            var res = new SingleRsp();

            // Assuming sanPhamSvc.All returns IEnumerable<SanPham>
            var products = sanPhamSvc.All.Select(p => new
            {
                TenSp = p.TenSp,
                Gia = p.Gia,
                TenLoaiSp=p.MaLoaiSpNavigation.TenLoaiSp,
                Image = p.HinhAnh // Assuming Image is a property representing the image
            }).ToList();

            res.Data = products;
            return Ok(res);

        }
        [HttpPut("Update-Product")]
        public IActionResult UpdateProduct(int Id,SanPhamReq sanPhamReq)
        {
            var res = sanPhamSvc.UpdateProduct(Id,sanPhamReq);
            return Ok(res);
        }
        [HttpPost("create-product")]
        public IActionResult CreateProduct([FromBody] SanPhamReq sanPhamReq)
        {
            var res = sanPhamSvc.CreateProduct(sanPhamReq);
            return Ok(res);
        }
        [HttpPost("search-product")]
        public IActionResult SearchProduct([FromBody] SearchProductReq searchProductReq)
        {
            var res = new SingleRsp();
            res = sanPhamSvc.SearchProduct(searchProductReq);
            return Ok(res);
        }
        [HttpDelete("Delete-Product")]
        public IActionResult DeleteProduct(int id)
        {
            QuanLyMyPhamContext context = new QuanLyMyPhamContext();

            var pr = sanPhamSvc.Read(id);
            if (pr.Data != null)
            {
                context.Remove(pr.Data);
                context.SaveChanges();
                return Ok(pr);
            }
            else
                return NotFound();
        }
    }
}

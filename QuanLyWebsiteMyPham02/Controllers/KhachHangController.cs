using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLMP.BLL;
using QLMP.Common.Req;
using QLMP.Common.Rsp;
using QLMP.DAL.Models;

namespace QLMP.Web.Controllers
{

    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private KhachHangSvc khachHangSvc;
        public KhachHangController()
        {
            khachHangSvc = new KhachHangSvc();
        }

        [HttpGet("GetById")]
        public IActionResult GetKhachHangById( int id)
        {
            var res = khachHangSvc.Read(id);
            return Ok(res);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllKhachHang()
        {
            var res = new SingleRsp();
            res.Data = khachHangSvc.All;
            return Ok(res);
        }

        [HttpPut("Update")]
        public IActionResult UpdateKhachHang(int Id, KhachHangReq khachHangReq)
        {
            var res = khachHangSvc.UpdateCustomer(Id,khachHangReq);
            return Ok(res);
        }


        [HttpPost("SearchByName")]
        public IActionResult SearchKhachHangByName([FromBody] SearchCateByName searchCateByName)
        {
            var res = khachHangSvc.SearchCustomer(searchCateByName.Keyword);
            return Ok(res);
        }
    }
}

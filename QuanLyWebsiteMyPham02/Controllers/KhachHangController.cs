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
    public class KhachHangController : ControllerBase
    {
        private KhachHangSvc khachHangSvc;
        public KhachHangController()
        {
            khachHangSvc = new KhachHangSvc();
        }

        [HttpPost("GetById")]
        public IActionResult GetKhachHangById([FromBody] SimpleReq simpleReq)
        {
            var res = khachHangSvc.Read(simpleReq.Id);
            return Ok(res);
        }

        [HttpPost("GetAll")]
        public IActionResult GetAllKhachHang()
        {
            var res = new SingleRsp();
            res.Data = khachHangSvc.All;
            return Ok(res);
        }

        [HttpPost("Create")]
        public IActionResult CreateKhachHang([FromBody] KhachHangReq khachHangReq)
        {
            var res = khachHangSvc.CreateCustomer(khachHangReq);
            res.Data = khachHangReq;
            return Ok(res.Data);
        }

        [HttpPut("Update")]
        public IActionResult UpdateKhachHang(int Id, KhachHangReq khachHangReq)
        {
            var res = khachHangSvc.UpdateCustomer(Id,khachHangReq);
            return Ok(res);
        }

        [HttpDelete("DeleteById")]
        public IActionResult DeleteKhachHangById(int id)
        {
            var res = khachHangSvc.Remove(id);
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

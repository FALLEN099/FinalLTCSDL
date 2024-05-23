using QLMP.Common.BLL;
using QLMP.Common.Rsp;
using QLMP.Common.Req;
using QLMP.DAL;
using QLMP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QLMP.BLL
{
    public class KhachHangSvc : GenericSvc<KhachHangRep, KhachHang>
    {
        private KhachHangRep khachHangRep;
        public KhachHangSvc()
        {
            khachHangRep = new KhachHangRep();
        }

        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }

        public override SingleRsp Update(KhachHang m)
        {
            var res = new SingleRsp();

            var m1 = m.MaKh > 0 ? _rep.Read(m.MaKh) : null;
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }

            return res;
        }

        public  SingleRsp Remove(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Remove(id);
            return res;
        }

        public SingleRsp CreateCustomer(KhachHangReq khachHangReq)
        {
            var res = new SingleRsp();
            KhachHang kh = new KhachHang();
            kh.TenKh = khachHangReq.TenKh;
            kh.DiaChi = khachHangReq.DiaChi;
            kh.Email = khachHangReq.Email;
            kh.Sdt = khachHangReq.Sdt;
            res = khachHangRep.CreateCustomer(kh);
            return res;
        }

        public SingleRsp UpdateCustomer(int Id,KhachHangReq khachHangReq)
        {
            //var res = new SingleRsp();

            //KhachHang kh = new KhachHang();
            //kh.TenKh = khachHangReq.TenKh;
            //kh.DiaChi = khachHangReq.DiaChi;
            //kh.Email = khachHangReq.Email;
            //kh.Sdt = khachHangReq.Sdt;
            //res = khachHangRep.UpdateCustomer(kh);
            //return res;
            var res = new SingleRsp();

            // Kiểm tra khách hàng có tồn tại trong cơ sở dữ liệu hay không
            var existingCustomer = khachHangRep.Read(Id);
            if (existingCustomer == null)
            {
                res.SetError("Customer not found.");
                
            }
            else
            {
                existingCustomer.TenKh = khachHangReq.TenKh;
                existingCustomer.DiaChi = khachHangReq.DiaChi;
                existingCustomer.Email = khachHangReq.Email;
                existingCustomer.Sdt = khachHangReq.Sdt;
                res = khachHangRep.UpdateCustomer(existingCustomer);
            }
            return res;
        }

        public SingleRsp SearchCustomer(string keyword)
        {
            var res = new SingleRsp();
            var cates = khachHangRep.SearchCustomer(keyword);
            res.Data = cates;
            return res;
        }
    }
}

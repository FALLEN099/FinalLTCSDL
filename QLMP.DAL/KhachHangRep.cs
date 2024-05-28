using QLMP.Common.DAL;
using QLMP.Common.Rsp;
using QLMP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLMP.DAL
{
    public class KhachHangRep : GenericRep<QuanLyMyPhamContext, KhachHang>
    {
        private QuanLyMyPhamContext _context = new QuanLyMyPhamContext();
        public KhachHangRep()
        {

        }
        public override KhachHang Read(int id)
        {
            var res = All.FirstOrDefault(c => c.MaKh == id);
            return res;
        }
      
        public SingleRsp CreateCustomer(KhachHang khachHangg)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                try
                {
                    var p = context.KhachHangs.Add(khachHangg);
                    context.SaveChanges();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    res.SetError(ex.StackTrace);
                }
            }
            return res;
        }

        public SingleRsp UpdateCustomer(KhachHang khachHangg)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                try
                {
                    var p = context.KhachHangs.Update(khachHangg);
                    context.SaveChanges();
                    tran.Commit();
                    res.Data = khachHangg;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    res.SetError(ex.StackTrace);
                }
            }
            return res;
        }

        public List<KhachHang> SearchCustomer(string keyword)
        {
            return All.Where(x => x.TenKh.Contains(keyword) || x.DiaChi.Contains(keyword) || x.Email.Contains(keyword)).ToList();
        }



    }
}

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
    public class SanPhamRep : GenericRep<QuanLyMyPhamContext, SanPham>
    {
        #region -- Overrides --


        public override SanPham Read(int id)
        {
            var res = All.FirstOrDefault(p => p.MaSp == id);
            return res;
        }

        public int Remove(int id)
        {
            var m = base.All.First(i => i.MaSp == id);
            m = base.Delete(m);
            return m.MaSp;
        }

        #endregion
        #region -- Method --
        public SingleRsp CreateProduct(SanPham sanPham)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                try
                {
                    var p = context.SanPhams.Add(sanPham);
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
        public SingleRsp UpdateProduct(SanPham sanPham)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                {
                    try
                    {
                        var p = context.SanPhams.Update(sanPham);
                        context.SaveChanges();
                        tran.Commit();
                        res.Data = sanPham;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        res.SetError(ex.StackTrace);
                    }
                }
               
            }
            return res;
        }
        public List<SanPham> SearchProduct(string keyword)
        {
            return All.Where(x => x.TenSp.Contains(keyword)).ToList();
        }
       
        public List<SanPham> SearchProductByCategoryName(string categoryName)
        {
            return All.Where(x => x.MaLoaiSpNavigation.TenLoaiSp.Contains(categoryName)).ToList();
        }

        #endregion
    }
}
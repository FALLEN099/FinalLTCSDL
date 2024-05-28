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
        public SingleRsp Remove(int Id)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                {
                    try
                    {
                        var sanPham = context.SanPhams.FirstOrDefault(s=>s.MaSp==Id);
                        if (sanPham != null)
                        {
                            context.SanPhams.Remove(sanPham);
                            context.SaveChanges();
                            tran.Commit();
                            res.Data = sanPham;
                        }
                        else
                        {
                            res.SetError("Product not found");
                        }
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

        public List<SanPham> GetAllProduct()
        {
            return All.ToList();
        }
        #endregion
    }
}
using QLMP.Common.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLMP.DAL.Models;
using QLMP.Common.Rsp;

namespace QLMP.DAL
{
    public class LoaiSpRep : GenericRep<QuanLyMyPhamContext, LoaiSanPham>
    {
        public LoaiSpRep()
        {

        }
        public override LoaiSanPham Read(int id)
        {
            var res = All.FirstOrDefault(c => c.MaLoaiSp == id);
            return res;
        }
        public SingleRsp Remove(int categoryId)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                try
                {
                    var category = context.LoaiSanPhams.Find(categoryId);
                    if (category != null)
                    {
                        context.LoaiSanPhams.Remove(category);
                        context.SaveChanges();
                        tran.Commit();
                        res.Data = category;
                    }
                    else
                    {
                        res.SetError("Category not found");
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    res.SetError(ex.StackTrace);
                }
            }
            return res;
        }

        public SingleRsp CreateCategory(LoaiSanPham loaiSanPham)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                try
                {
                    var p = context.LoaiSanPhams.Add(loaiSanPham);
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
        public SingleRsp UpdateCategory(LoaiSanPham loaiSanPham)
        {
            var res = new SingleRsp();
            using (var context = new QuanLyMyPhamContext())
            {
                using var tran = context.Database.BeginTransaction();
                try
                {
                    var p = context.LoaiSanPhams.Update(loaiSanPham);
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
        public List<LoaiSanPham> SearchCategory(string keyword)
        {
            return All.Where(x => x.TenLoaiSp.Contains(keyword)).ToList();
        }
    }
}
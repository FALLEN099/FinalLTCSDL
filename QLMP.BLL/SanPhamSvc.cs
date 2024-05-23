using QLMP.Common.BLL;
using QLMP.Common.Req;
using QLMP.Common.Rsp;
using QLMP.DAL;
using QLMP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLMP.BLL
{
    public class SanPhamSvc : GenericSvc<SanPhamRep, SanPham>
    {
        private SanPhamRep sanPhamRep;
        public SanPhamSvc()
        {
            sanPhamRep = new SanPhamRep();
        }
        #region -- Overrides --

        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }
        //public override SingleRsp Remove(int id)
        //{
        //    var res = new SingleRsp();
        //    res.Data = _rep.Remove(id);
        //    return res;
        //}
        public override SingleRsp Update(SanPham m)
        {
            var res = new SingleRsp();

            var m1 = m.MaSp > 0 ? _rep.Read(m.MaSp) : _rep.Read(m.TenSp);
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
        #endregion
        public SingleRsp CreateProduct(SanPhamReq sanPhamReq)
        {
            var res = new SingleRsp();
            SanPham sanPham = new SanPham();
            sanPham.TenSp = sanPhamReq.TenSp;
            sanPham.Gia = sanPhamReq.Gia;
            sanPham.HinhAnh = sanPhamReq.HinhAnh;
            sanPham.MaLoaiSp = sanPhamReq.MaLoaiSp;
            return res = sanPhamRep.CreateProduct(sanPham);
        }

        public SingleRsp UpdateProduct(int Id,SanPhamReq sanPhamReq)
        {
            var res = new SingleRsp();
            var existingCustomer = sanPhamRep.Read(Id);
            if (existingCustomer == null)
            {
                res.SetError("Customer not found.");
            }
            else
            {

                existingCustomer.TenSp = sanPhamReq.TenSp;
                existingCustomer.Gia = sanPhamReq.Gia;
                existingCustomer.HinhAnh = sanPhamReq.HinhAnh;
                res = sanPhamRep.UpdateProduct(existingCustomer);
            }
            return res;
        }

        public SingleRsp SearchProduct(SearchProductReq searchProductReq)
        {
            var res = new SingleRsp();
            //lay dssp theo keyword
            var sanPhams = sanPhamRep.SearchProduct(searchProductReq.Keyword);
            //xu ly phan trang
            int pCount, totalPage, offset;
            offset = searchProductReq.Size * (searchProductReq.Page - 1);
            pCount = sanPhams.Count;
            totalPage = (pCount % searchProductReq.Size) == 0 ? pCount / searchProductReq.Size : 1 + (pCount / searchProductReq.Size);
            var p = new
            {
                Data = sanPhams.Skip(offset).Take(searchProductReq.Size).ToList(),
                Page = searchProductReq.Page,
                Size = searchProductReq.Size
            };
            res.Data = p;
            return res;
        }
        public SingleRsp SearchProductByCategoryName(string categoryName)
        {
            var res = new SingleRsp();
            var sanPhams = sanPhamRep.SearchProductByCategoryName(categoryName);
            if (sanPhams == null)
            {
                res.SetError("CatagoryName not found.");
                return res;
            }
            res.Data = sanPhams;
            return res;
        }
    }
}
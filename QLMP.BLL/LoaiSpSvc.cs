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
    public class LoaiSpSvc : GenericSvc<LoaiSpRep, LoaiSanPham>
    {
        private LoaiSpRep loaiSpRep;
        public LoaiSpSvc()
        {
            loaiSpRep = new LoaiSpRep();
        }
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();
            res.Data = _rep.Read(id);
            return res;
        }
        public override SingleRsp Update(LoaiSanPham m)
        {
            var res = new SingleRsp();

            var m1 = m.MaLoaiSp > 0 ? _rep.Read(m.MaLoaiSp) : _rep.Read(m.TenLoaiSp);
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
        public SingleRsp Remove(int id)
        {

            return _rep.Remove(id);

        }
        public SingleRsp CreateCategory(LoaiSpReq loaiSpReq)
        {
            var res = new SingleRsp();
          
            LoaiSanPham l = new LoaiSanPham();
            l.TenLoaiSp = loaiSpReq.TenLoaiSp;
            res = loaiSpRep.CreateCategory(l);
            return res;
        }
        public SingleRsp UpdateCategory(int Id,LoaiSpReq loaiSpReq)
        {
            var res = new SingleRsp();
            var existingCategory = loaiSpRep.Read(Id);
            if (existingCategory == null)
            {
                res.SetError("Customer not found.");
            }
            else
            {
                existingCategory.TenLoaiSp = loaiSpReq.TenLoaiSp;   
                res = loaiSpRep.UpdateCategory(existingCategory);
            }
            return res;
        }
        public SingleRsp SearchCategory(SearchCateByName searchCateByName)
        {
            var res = new SingleRsp();
            //lay dssp theo keyword
            var cates = loaiSpRep.SearchCategory(searchCateByName.Keyword);
            res.Data = cates;
            return res;
        }
    }

}

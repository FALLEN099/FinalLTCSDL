using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLMP.Common.Req
{
    public class SanPhamReq
    {
        public int MaSp { get; set; }
        public string? TenSp { get; set; }
        public double? Gia { get; set; }
        public int? MaLoaiSp { get; set; }
        public string? HinhAnh { get; set; }
    }
}

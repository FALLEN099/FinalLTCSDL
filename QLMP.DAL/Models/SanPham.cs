using System;
using System.Collections.Generic;

namespace QLMP.DAL.Models
{
    public partial class SanPham
    {
        public SanPham()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public int MaSp { get; set; }
        public string? TenSp { get; set; }
        public double? Gia { get; set; }
        public int? MaLoaiSp { get; set; }
        public string? HinhAnh { get; set; }

        public virtual LoaiSanPham? MaLoaiSpNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace QLMP.DAL.Models
{
    public partial class HoaDon
    {
        public HoaDon()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        public int MaHoaDon { get; set; }
        public int? MaKh { get; set; }
        public DateTime? NgayLapHd { get; set; }
        public decimal? TongSl { get; set; }

        public virtual KhachHang? MaKhNavigation { get; set; }
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }
    }
}

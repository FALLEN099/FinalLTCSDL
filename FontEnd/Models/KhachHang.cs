using System;
using System.Collections.Generic;

namespace FrontEnd.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        public int MaKh { get; set; }
        public string? TenKh { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }
        public int? Sdt { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}

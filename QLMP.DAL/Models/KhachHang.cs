using System;
using System.Collections.Generic;

namespace QLMP.DAL.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            Carts = new HashSet<Cart>();
            HoaDons = new HashSet<HoaDon>();
        }

        public int MaKh { get; set; }
        public int UserId { get; set; }
        public string? TenKh { get; set; }
        public string? DiaChi { get; set; }
        public string? Email { get; set; }
        public int? Sdt { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}

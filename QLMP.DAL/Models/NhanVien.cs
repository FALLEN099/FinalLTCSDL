using System;
using System.Collections.Generic;

namespace QLMP.DAL.Models
{
    public partial class NhanVien
    {
        public int MaNv { get; set; }
        public string TenDn { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string HoTen { get; set; } = null!;
        public string? Email { get; set; }
        public int? Sdt { get; set; }
    }
}

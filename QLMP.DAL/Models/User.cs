using System;
using System.Collections.Generic;

namespace QLMP.DAL.Models
{
    public partial class User
    {
        public User()
        {
            KhachHangs = new HashSet<KhachHang>();
        }

        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string Role { get; set; } = null!;

        public virtual Role RoleNavigation { get; set; } = null!;
        public virtual ICollection<KhachHang> KhachHangs { get; set; }
    }
}

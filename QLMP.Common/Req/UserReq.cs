using System.ComponentModel.DataAnnotations;

namespace QLMP.Common.Req
{
    public class UserReq
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string PassWord { get; set; } = null!;
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Phone { get; set; }
        [Required]
        public string? Address { get; set; }
    }
}

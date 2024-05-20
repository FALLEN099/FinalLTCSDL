using System.ComponentModel.DataAnnotations;

namespace QLMP.Common.Req
{
    public class LoginReq
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(250)]
        public string Password { get; set; } = null!;
    }
}

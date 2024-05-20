namespace QLMP.Common.Req
{
    public class UserReq
    {
        public string UserName { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }
}

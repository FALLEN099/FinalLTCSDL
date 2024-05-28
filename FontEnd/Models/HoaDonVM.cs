namespace FrontEnd.Models
{
    public class HoaDonVM
    {
        public int MaHoaDon { get; set; }
        public string MaKh { get; set; }
        public DateTime dateTime { get; set; }
        public int TongSl { get; set; }

        public List<ChiTetHDVM> OrderDetails { get; set; }
    }
}

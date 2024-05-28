namespace FrontEnd.Models
{
    public class SanPhamVM
    {
       public SanPhamVM(int maSp, string? tenSp, double? gia, string tenLoaiSp, string? image)
        {
            this.maSp = maSp;
            TenSp = tenSp;
            Gia = gia;
            TenLoaiSp = tenLoaiSp;
            Image = image;
        }


        public int maSp { get; set; }
        public string? TenSp { get; set; }
        public double? Gia { get; set; }
        public string TenLoaiSp { get; set; }
        public string? Image { get; set; }
    }
}

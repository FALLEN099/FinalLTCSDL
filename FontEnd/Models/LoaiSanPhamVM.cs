namespace FrontEnd.Models
{
    public class LoaiSanPhamVM
    {
        public int maLoaiSp { get; set; }
        public string tenLoaiSp { get; set; }
        public List<SanPhamVM> sanPhams { get; set; }
    }
}

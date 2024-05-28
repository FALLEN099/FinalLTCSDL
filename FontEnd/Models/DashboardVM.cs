namespace FrontEnd.Models
{
    public class DashboardVM
    {
        public List<ThongKeVM> ThongKeList { get; set; }
        public List<RecentOrderVM> RecentOrders { get; set; }
        public List<ThongKeSaleByCateVM> ThongKeSaleByCateVM { get; set; }

        public DashboardVM()
        {
            ThongKeList = new List<ThongKeVM>();
            RecentOrders = new List<RecentOrderVM>();
            ThongKeSaleByCateVM = new List<ThongKeSaleByCateVM>();
        }
    }
}

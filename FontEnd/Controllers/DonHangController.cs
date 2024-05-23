using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class DonHangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

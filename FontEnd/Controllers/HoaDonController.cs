using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class HoaDonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

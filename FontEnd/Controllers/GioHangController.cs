using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class GioHangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

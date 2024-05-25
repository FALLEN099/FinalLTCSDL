using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class DangNhapAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class HomeAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

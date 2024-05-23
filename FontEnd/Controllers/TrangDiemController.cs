using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class TrangDiemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

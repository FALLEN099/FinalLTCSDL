using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class HomeAdmin : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

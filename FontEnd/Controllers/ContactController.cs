using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace FrontEnd.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            // Here you would typically authenticate the user
            // For simplicity, I'm just checking if username and password are not empty
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Authentication successful, you can set a cookie or session to indicate the user is logged in
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index", "Home"); // Redirect to the home page after successful login
            }
            else
            {
                // Authentication failed, return the login view with an error message
                ViewBag.Error = "Invalid username or password";
                return View();
            }
        }
    }
}

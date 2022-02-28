using Microsoft.AspNetCore.Mvc;

namespace PhonebookAndASPNETMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PhonebookMenu()
        {
            return View();
        }

        public IActionResult PhonesShow()
        {
            return View();
        }

        public IActionResult HelloWorld()
        {
            return View();
        }
    }
}

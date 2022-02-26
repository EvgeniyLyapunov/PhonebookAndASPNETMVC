using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhonebookAndASPNETMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IFA.Web.Controllers
{
    public class ReportFinController : Controller
    {
        public IActionResult LabaRugi()
        {
            return View();
        }
        public IActionResult Neraca()
        {
            return View();
        }
    }
}
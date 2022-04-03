using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontToBackEnd.Areas.Areas.Controllers
{
    public class DashboardController : Controller
    {
          [Area("AdminArea")]
            public IActionResult Index()
            {
                return View();
            }
        
    }
}

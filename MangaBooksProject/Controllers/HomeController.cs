using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MangaBooksProject.Models;
using MangaBooksProject.Services;
using Microsoft.AspNetCore.Hosting;

namespace MangaBooksProject.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ILogger<HomeController> _logger;
        private readonly IMangaData db;

        public HomeController(ILogger<HomeController> logger, IMangaData db)
        {
            _logger = logger;
            this.db = db;
        }

        public IActionResult Index(string searchString)
        {
            var model = db.GetBySearchString(searchString);
            return View(model);
        }

   
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

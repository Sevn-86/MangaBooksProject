using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MangaBooksProject.Models;

using Microsoft.AspNetCore.Hosting;
using MangaBooksProject.Data;

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

        // Returns the index view
        public async Task<ViewResult> Index(string searchString)
        {
            var model = await db.GetAllMangas(searchString);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

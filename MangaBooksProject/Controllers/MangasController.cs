using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MangaBooksProject.Models;
using MangaBooksProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Hosting;

namespace MangaBooksProject.Controllers
{
    public class MangasController : Controller

    {
        private readonly IMangaData db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public MangasController(IMangaData db, IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        //Get Mangas List
        public IActionResult Index(string searchString)
        {
            //var model = db.GetAll();

            var model = db.GetBySearchString(searchString);

            return View(model);
        }

        //Get Create
        [HttpGet]
        public IActionResult Create(int id)
        {
            var model = db.GetById(id);
            return View(model);
        }

        //Post Create
        [HttpPost]
        public async Task<IActionResult> Create(Manga manga, IFormFile mangaimage)
        {
            if (ModelState.IsValid)
            {

                if (mangaimage != null)
                {
                    string folder = "Images/Uploaded_covers/";
                    folder += mangaimage.FileName + Guid.NewGuid().ToString(); 
                    string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);
                    await mangaimage.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                    //db.Add(manga);
                    return View();
                }
            }
            return RedirectToPage("NotFound");
        }


        //public IActionResult SingleFile()
        //{

        //    var dir = env.WebRootPath;
        //    using (var fileStream = new FileStream(Path.Combine(dir, filename), FileMode.Create, FileAccess.Write))
        //    {
        //        file.CopyTo(fileStream);
        //    }

        //    return RedirectToAction("Create ", "Mangas");
        //}



        //Get Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = db.GetById(id);
            return View(model);
        }

        //Get Delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = db.GetById(id);
            if (model == null)
            {
                return RedirectToAction("NotFound");
            }
            return View(model);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection form)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }


        //Get Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = db.GetById(id);
            if (model == null)
            {
                return RedirectToAction("NotFound");
            }
            return View(model);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Manga manga)
        {
            if (ModelState.IsValid)
            {
                db.Update(manga);
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult HotManga()
        {
            var model = db.GetByRating();
            return View(model);
        }

        [HttpGet]
        public IActionResult FinishedManga()
        {
            var model = db.GetByStatus();
            return View(model);
        }




        
        
        


    }

}

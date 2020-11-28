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
        public IActionResult Create(MangaModelCreate model)
        {
            if (model.MangaImage != null && ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.MangaImage != null)
                {
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/Uploaded_covers");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.MangaImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.MangaImage.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                MangaModel newManga = new MangaModel
                {
                    MangaImagePath = uniqueFileName,
                    Chapters = model.Chapters,
                    Description = model.Description,
                    Title = model.Title,
                    Author = model.Author,
                    Status = model.Status,
                    Rating = model.Rating,
                    Releasedate = model.Releasedate,
                    Genre = model.Genre
                };
                db.Add(newManga);
                return RedirectToAction("Details", new { id = newManga.Id });
            }
            return View();
        }

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
                return RedirectToAction("Error");
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
        public IActionResult Edit(MangaModel manga)
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

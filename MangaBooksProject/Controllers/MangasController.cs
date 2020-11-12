using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MangaBooksProject.Models;
using MangaBooksProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangaBooksProject.Controllers
{
    public class MangasController : Controller

    {
        private readonly IMangaData db;

        public MangasController(IMangaData db)
        {
            this.db = db;
        }

        //Get Mangas List
        public IActionResult Index()
        {
            var model = db.GetAll();
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
        public IActionResult Create(Manga manga)
        {
            if (ModelState.IsValid)
            {
                db.Add(manga);
                return RedirectToAction("Index");
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

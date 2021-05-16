using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MangaBooksProject.Models;
using MangaBooksProject.Data;
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

        //returns all manga 
        public async Task<ViewResult> Index(string searchString)
        {
            var model = await db.GetAllMangas(searchString);
            return View(model);
        }

        //returns all manga where Rating > 3
        public async Task<ViewResult> PopulairManga(string searchString)
        {
            var model = await db.GetPopulairManga(searchString);
            return View(model);
        }

        //returns all manga where Status == true
        public async Task<ViewResult> FinishedManga(string searchString)
        {
            var model = await db.GetFinishedManga(searchString);
            return View(model);
        }

        //request data from Create view
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        //sends data from create view to server
        //creates a new filestream with unique GUID for every uploaded image
        [HttpPost]
        public async Task<IActionResult> Create(MangaModel model)
        {
            if (model.MangaImage != null)
            {
                string folder = "images/Uploaded_covers/";
                string imagePath = Path.Combine(webHostEnvironment.WebRootPath, folder);

                var fileName = Guid.NewGuid().ToString() + model.MangaImage.FileName;
                model.fileName = fileName;
                await model.MangaImage.CopyToAsync(new FileStream(imagePath + fileName, FileMode.Create));

                await db.Add(model);

                return RedirectToAction("Index", "Home");
            };
            return RedirectToAction(nameof(Create));
        }

        //request data from Details view
        [HttpGet]
        public async Task<ViewResult> Details(int Id)
        {
            var data = await db.GetById(Id);
            return View(data);
        }

        //request data from Delete view
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await db.GetById(id);
            if (data == null)
            {
                return RedirectToAction("Error");
            }
            return View(data);
        }

        //sends data from delete view to server
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection form)
        {
            db.Delete(id);
            return RedirectToAction("Index");
        }


        //request data from Edit view
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await db.GetById(id);
        
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("NotFound");
            
        }

        //sends data from Edit view to server
        //if the ModelState is valid update the imagepath  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MangaModel model) 
        {
            if (ModelState.IsValid)
            {
                string folder = "images/Uploaded_covers/";
                string imagePath = Path.Combine(webHostEnvironment.WebRootPath, folder);

                var fileName = Guid.NewGuid().ToString() + model.MangaImage.FileName;
                model.fileName = fileName;
                await model.MangaImage.CopyToAsync(new FileStream(imagePath + fileName, FileMode.Create));

                db.Update(model);

                return RedirectToAction("Index", "Home");
            };
            return RedirectToAction(nameof(Create));
        }
    }
}

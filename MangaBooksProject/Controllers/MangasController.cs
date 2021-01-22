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


        public async Task<ViewResult> Index()
        {

            var model = await db.GetAll();
            return View(model);
        }



        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }


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
    


        //[Route("Details/{id}", Name = "mangaDetailsroute")]
        [HttpGet]
        public async Task<ViewResult> Details(int Id)
        {
            var data = await db.GetById(Id);
            return View(data);
        }

        //Get Delete
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
        public async Task<IActionResult> Edit(int id)
        {
            var model = await db.GetById(id);
        
            if (model != null)
            {
                return View(model);
            }
            return RedirectToAction("NotFound");
            
        }

        //Post Edit
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

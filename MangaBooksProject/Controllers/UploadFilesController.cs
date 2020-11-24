using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MangaBooksProject.Controllers
{
    public class UploadFilesController : Controller
    {
        private readonly IWebHostEnvironment env;

        public UploadFilesController(IWebHostEnvironment env)
        {
            this.env = env;
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult SingleFile(IFormFile file)
        {
            var filename = System.IO.Path.GetFileName(file.FileName);
            var dir = env.WebRootPath;
            using (var fileStream = new FileStream(Path.Combine(dir, filename), FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }

            return RedirectToAction("Create ", "Mangas");
        }

    }
}


    //    [HttpPost("FileUpload")]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IActionResult> Index(List<IFormFile> files)
    //        {
    //            long size = files.Sum(f => f.Length);

    //            var filePaths = new List<string>();
    //            foreach (var formFile in files)
    //            {
    //                if (formFile.Length > 0)
    //                {
    //                // full path to file in temp location 
    //                var filePath = Path.GetTempPath(); //we are using Temp file name just for the example. Add your own file path.
    //                    filePaths.Add(filePath);
    //                    using (var stream = new FileStream(filePath, FileMode.Create))
    //                    {
    //                        await formFile.CopyToAsync(stream);
    //                    }
    //                }
    //            }
    //            // process uploaded files
    //            // Don't rely on or trust the FileName property without validation.
    //            return Ok(new { count = files.Count, size, filePaths });
    //        }
    //    }
    //}

        

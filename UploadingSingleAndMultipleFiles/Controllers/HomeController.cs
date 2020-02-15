using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UploadingSingleAndMultipleFiles.Models;

namespace UploadingSingleAndMultipleFiles.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger,IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {

            var dir = _env.WebRootPath;
            var images = "images/" + file.Name + "file.jpg";
            var path = Path.Combine(dir,images );
            using (var fileStream = new FileStream(path,FileMode.Create,FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }

                return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UploadMany(IEnumerable<IFormFile> files)
        {
            var i = 0;
            var dir = _env.WebRootPath;
            foreach (var file in files)
            {
              
                var image = "images/" + file.Name + $"{i++}.jpg";
                var path = Path.Combine(dir, image);
                using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
            };

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

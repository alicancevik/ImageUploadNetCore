using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ImageUploadNetCore.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace ImageUploadNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult UploadImage()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/");

            var imageList = Directory.GetFiles(path);

            List<UploadImageViewModel> uploadedImages = new List<UploadImageViewModel>();

            foreach (var image in imageList)
            {
                FileInfo fileInfo = new FileInfo(image);

                UploadImageViewModel model = new UploadImageViewModel();
                model.FullName = image.Substring(image.IndexOf("wwwroot")).Replace("wwwroot/", string.Empty);
                model.FileName = fileInfo.Name;
                model.Size = fileInfo.Length / 1024;

                uploadedImages.Add(model);
            }

            return View(uploadedImages);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null)
            {
                string imageExtension = Path.GetExtension(file.FileName);

                string imageName = Guid.NewGuid() + imageExtension;

                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{imageName}");

                using var stream = new FileStream(path, FileMode.Create);

                await file.CopyToAsync(stream);

            }

            return RedirectToAction("UploadImage");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

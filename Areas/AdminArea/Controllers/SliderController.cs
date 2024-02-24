using Fiorello.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fiorello.Areas.ViewModels.Slider;
using Fiorello.Models;
using Fiorello.Extensions;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index()
        {
            var datas=_context.Sliders
                .AsNoTracking()
                .ToList();

            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(SliderCreateVM createVM)
        {
            var photos = createVM.Photo;
            if(photos == null || photos.Length==0 )
            {
                ModelState.AddModelError("Photo","Please upload file");
                return View();
            }
            if (!photos.CheckFile())
            {
                ModelState.AddModelError("Photo", "Please upload right file");
                return View();
            }
            if (photos.CheckSize(500))
            {
                ModelState.AddModelError("Photo", "Please choose normal file");
                return View();
            }
            var slider=new Slider(){ ImageUrl=photos.SaveFile("img")};
            _context.Sliders.Add(slider);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

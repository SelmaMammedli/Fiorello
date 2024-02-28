using Fiorello.Areas.ViewModels.Product;
using Fiorello.DAL;
using Fiorello.Extensions;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var datas=_context.Products
                .Include(c=>c.Category)
                .Include(c=>c.ProductImages)
                .AsNoTracking()
                .ToList();
            return View(datas);
        }
        public IActionResult Create()
        {
            ViewBag.Category = _context.Category.ToList();
            
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ProductCreateVM productCreateVM)
        {
            ViewBag.Category = _context.Category.ToList();
            if (!ModelState.IsValid) return View();

            var photos=productCreateVM.Photos;
            Product newProduct = new Product();
            foreach (var photo in photos)
            {
                if (photo == null || photo.Length == 0)
                {
                    ModelState.AddModelError("Photo", "Please upload file");
                    return View();
                }
                if (!photo.CheckFile())
                {
                    ModelState.AddModelError("Photo", "Please upload right file");
                    return View();
                }
                if (photo.CheckSize(500))
                {
                    ModelState.AddModelError("Photo", "Please choose normal file");
                    return View();
                }
                ProductImage productImage = new();
                productImage.ImageUrl = photo.SaveFile("img");
                productImage.ProductId = newProduct.Id;
                if (photos[0]==photo)
                {
                    productImage.IsMain = true;
                }

                
                newProduct.ProductImages.Add(productImage);
            }


            newProduct.Name = productCreateVM.Name;
            newProduct.CategoryId = productCreateVM.CategoryId;
            newProduct.Price = productCreateVM.Price;

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

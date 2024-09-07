using Fiorello.Areas.ViewModels.Product;
using Fiorello.DAL;
using Fiorello.Extensions;
using Fiorello.Helper;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult Index(int page=1,int take=3)
        {
            var datas=_context.Products
                .Include(c=>c.Category)
                .Include(c=>c.ProductImages)
                .AsNoTracking()
                .Skip((page-1)*take)
                .Take(take)
                .ToList();
            var allProductsCount=_context.Products.Count();
            int totalPage =(int)Math.Ceiling((decimal) (allProductsCount) / take);
            PaginationHelper<Product> paginationVM = new();
            paginationVM.Items= datas;
            paginationVM.CurrentPage= page;
            paginationVM.TotalPage =totalPage;
            return View(paginationVM);
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
        public IActionResult Update(int? id)
        {
            //ViewBag.Category = _context.Category.ToList();
            ViewBag.Category = new SelectList(_context.Category.ToList(),"Id","Name");
            if (id == null) return NotFound();
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            ProductUpdateVM productUpdateVM = new ProductUpdateVM();
            productUpdateVM.Name=product.Name;
            productUpdateVM.Price = product.Price;
            productUpdateVM.CategoryId= product.CategoryId;
            productUpdateVM.ProductImages= product.ProductImages;
            return View(productUpdateVM);

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(int?id,ProductUpdateVM productUpdateVM)
        {
            ViewBag.Category = new SelectList(_context.Category.ToList(), "Id", "Name");
            if(id == null) return NotFound();
            var product = _context.Products
                .Include(p=>p.ProductImages)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            productUpdateVM.ProductImages = product.ProductImages;
            if (!ModelState.IsValid) return View(productUpdateVM);
            var photos=productUpdateVM.Photos;
            if(photos!=null && photos.Length> 0)
            {
                foreach (var photo in photos)
                {
                   
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
                    productImage.ProductId = product.Id;
                    if (!product.ProductImages.Any(p=>p.IsMain))
                    {
                        if (photos[0] == photo)
                        {
                            productImage.IsMain = true;
                        }
                    }
                   


                    product.ProductImages.Add(productImage);
                }
            }



            product.Name = productUpdateVM.Name;
            product.Price = productUpdateVM.Price;
            product.CategoryId = productUpdateVM.CategoryId;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult DeleteImage(int? id)
        {
            if (id == null) return NotFound();
            var image=_context.ProductImages.FirstOrDefault(p => p.Id == id);
            if (image == null) return NotFound();
            _context.ProductImages.Remove(image);
            _context.SaveChanges();
            return RedirectToAction("Update", new {id=image.ProductId});


        }
        public IActionResult Delete(int? id)
        {
            if(id== null)return NotFound();
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p=>p.ProductImages)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null) return NotFound();
            var product = _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();
            foreach (var image in product.ProductImages)
            {
                DeleteFileHelper.DeleteFile("img", image.ImageUrl);
            }
 
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int? id)
        {
            if (id == null) return NotFound();
            var product = _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);

        }
        public IActionResult SetMainImage(int? id)
        {
            if (id == null) return NotFound();
            var image = _context.ProductImages.FirstOrDefault(p => p.Id == id);
            if (image == null) return NotFound();
            image.IsMain = true;
            var existIsMain = _context.ProductImages.FirstOrDefault(p => p.IsMain && p.ProductId==image.ProductId);
            if(existIsMain is not null)
            {
                existIsMain.IsMain = false;
            }
            _context.SaveChanges();
            return RedirectToAction("Detail", new { id = image.ProductId });


        }
    }
}

using Fiorello.Models;

namespace Fiorello.Areas.ViewModels.Product
{
    public class ProductUpdateVM
    {
  
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile[]? Photos { get; set; }
        public List<ProductImage>?ProductImages { get; set; }
    }
}

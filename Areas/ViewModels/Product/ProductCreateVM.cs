using System.ComponentModel.DataAnnotations;

namespace Fiorello.Areas.ViewModels.Product
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public IFormFile[] Photos { get; set; }

    }
}

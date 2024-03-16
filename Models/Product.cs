using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorello.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required,StringLength(50)]
        public string Name { get; set; }
        [Column(TypeName ="money")]
        public double Price { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
       
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [NotMapped]
        public string? MainImageUrl => ProductImages.Count > 0 ? (ProductImages.Any(p => p.IsMain) ?
            ProductImages.FirstOrDefault(p => p.IsMain).ImageUrl :
            ProductImages.FirstOrDefault().ImageUrl) : null;
        public Product()
        {
            ProductImages = new(); 
        }
    }
}

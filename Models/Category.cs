using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorello.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [NotMapped]
        public string ShortDesc => Description.Length > 10 ? Description.Substring(0, 10)+"..." : Description;
        public string Description { get; set; }
        public List<Product> Products { get; set; }
    }
}

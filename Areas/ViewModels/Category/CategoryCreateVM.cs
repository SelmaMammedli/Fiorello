using System.ComponentModel.DataAnnotations;

namespace Fiorello.Areas.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

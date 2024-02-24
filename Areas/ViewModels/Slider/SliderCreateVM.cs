using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fiorello.Areas.ViewModels.Slider
{
    public class SliderCreateVM
    {
        [Required(ErrorMessage ="PLEASE FILL")]
        public IFormFile Photo { get; set; }
    }
}

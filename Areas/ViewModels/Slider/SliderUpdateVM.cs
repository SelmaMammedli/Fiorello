using System.ComponentModel.DataAnnotations;

namespace Fiorello.Areas.ViewModels.Slider
{
    public class SliderUpdateVM
    {
        public IFormFile? Photo { get; set; }
        public string ImageUrl { get; set; }
    }
}

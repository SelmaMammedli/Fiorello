using Fiorello.Models;

namespace Fiorello.ViewModels
{
    public class HomeVm
    {
        public IEnumerable<Slider>Sliders { get; set; }
        public SliderContent SliderContent { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

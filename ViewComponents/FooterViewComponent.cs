using Fiorello.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var bios = _context.Bios
                .ToDictionary(b => b.Key, b => b.Value);




            return View(await Task.FromResult(bios));

        }
    }
}

using Fiorello.DAL;
using Fiorello.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.ViewComponents
{
    public class HeaderViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public HeaderViewComponent(AppDbContext context)
        {
            _context = context;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
           
            var bios = _context.Bios
                .ToDictionary(b=>b.Key,b=>b.Value);




            return View(await Task.FromResult(bios));

        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToyChange.Data;

namespace ToyChange.Areas.Admin
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["EmailSortParm"] = sortOrder == "Price" ? "Price_desc" : "Price";
            ViewData["CurrentFilter"] = searchString;

            var items = from i in _context.Item
                        select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.Title.ToLower().Contains(searchString.ToLower()));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    items = items.OrderByDescending(i => i.Title);
                    break;
                case "Price":
                    items = items.OrderBy(u => u.Price);
                    break;
                case "Price_desc":
                    items = items.OrderByDescending(u => u.Price);
                    break;
                default:
                    items = items.OrderBy(i => i.ItemId);
                    break;
            }
            return View(await items.AsNoTracking().ToListAsync());
        }
    }
}

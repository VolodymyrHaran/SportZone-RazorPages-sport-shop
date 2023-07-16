using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Models;

namespace SportZone.Pages.Products
{
	public class DetailedPageModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public DetailedPageModel(ApplicationDbContext context)
		{
			_context = context;
		}

		[BindProperty]
		public Product Product { get; set; } = default!;

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.Products == null)
			{
				return NotFound();
			}

			var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			Product = product;
			return Page();
		}
	}
}

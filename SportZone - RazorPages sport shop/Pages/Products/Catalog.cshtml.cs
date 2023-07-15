using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportZone.Data;
using SportZone.Models;

namespace SportZone.Pages.Products
{
	public class CatalogModel : PageModel
	{
		private readonly ApplicationDbContext _context;

		public CatalogModel(ApplicationDbContext context)
		{
			_context = context;
		}

		public IList<Product> Products { get; set; } = default!;

		public async Task OnGetAsync(string category, float? minPrice, float? maxPrice)
		{
			if (string.IsNullOrEmpty(category) && !minPrice.HasValue && !maxPrice.HasValue)
			{
				if (_context.Products != null)
				{
					Products = await _context.Products.ToListAsync();
				}

			}
			else
			{
				if (!minPrice.HasValue) minPrice = 0;
				if (!maxPrice.HasValue) maxPrice = 10000;
				Products = await GetFilteredProducts(category, minPrice.Value, maxPrice.Value);
			}
		}

		private Task<List<Product>> GetFilteredProducts(string category, float? minPrice, float? maxPrice)
		{
			if (category.Equals("All"))
			{
				return _context.Products
				.Where(p => p.Price >= minPrice && p.Price <= maxPrice)
				.ToListAsync();
			}
			Categories result;
			if (Enum.TryParse(category, out result))
			{
				return _context.Products
				.Where(p => p.Category.Equals(result) && p.Price >= minPrice && p.Price <= maxPrice)
				.ToListAsync();
			}
			else
			{
				return _context.Products
				.Where(p => p.Category.Equals(result) && p.Price >= minPrice && p.Price <= maxPrice)
				.ToListAsync();
			}

		}
	}
}

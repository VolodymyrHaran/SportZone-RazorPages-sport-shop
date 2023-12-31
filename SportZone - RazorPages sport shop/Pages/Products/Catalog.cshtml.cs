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

		public async Task OnGetAsync(string category, float? minPrice, float? maxPrice, string searchName)
		{
			if (string.IsNullOrEmpty(category) && !minPrice.HasValue && !maxPrice.HasValue && string.IsNullOrEmpty(searchName))
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
				Products = await GetFilteredProducts(category, minPrice.Value, maxPrice.Value, searchName);
			}
		}

		private Task<List<Product>> GetFilteredProducts(string category, float? minPrice, float? maxPrice, string searchName)
		{
			var filtered = _context.Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice);
			if (!string.IsNullOrEmpty(searchName)) filtered = filtered.Where(p => p.Name.ToUpper().Contains(searchName.ToUpper()));
			if (category.Equals("All")) return filtered.ToListAsync();

			Categories result;
			Enum.TryParse(category, out result);
			return filtered
				.Where(p => p.Category.Equals(result))
				.ToListAsync();

		}
	}
}

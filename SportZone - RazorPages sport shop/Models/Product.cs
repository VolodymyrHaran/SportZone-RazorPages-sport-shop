using SportZone.Pages.Products;
using System.ComponentModel.DataAnnotations;

namespace SportZone.Models
{
	public class Product
	{
		public int Id { get; set; }
		[Required]
		[MinLength(2), MaxLength(100)]
		public string Name { get; set; }
		[MinLength(10), MaxLength(1000)]
		public string? Description { get; set; }
		public string? ImageUrl { get; set; }
		public Categories Category { get; set; }
		[Required, Range(0, 10000)]
		public float Price { get; set; }
		[Required]
		public bool IsAvaliable { get; set; }
		[MaxLength(10)]
		public string? Size { get; set; }
		[MaxLength(50)]
		public string? Brand { get; set; }
		[Range(0, 5)]
		public float Rating { get; set; } = 0;
		[MaxLength(50)]
		public string? AdditionalParameters { get; set; }
		public DateTime? CreationDate { get; set; } = DateTime.Now;
		public DateTime? UpdateDate { get; set; }

	}
}

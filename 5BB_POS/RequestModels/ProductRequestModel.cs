using System.ComponentModel.DataAnnotations;

namespace _5BB_POS.RequestModels
{
	public class ProductRequestModel
	{
		[Required(ErrorMessage = "Product name is required")]
		[RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Only letters and numbers are allowed.")]
		[StringLength(100, MinimumLength = 3)]
		public string Name { get; set; } = null!;

		[Required]
		[Range(0.01, 1000000, ErrorMessage = "Price must be greater than 0")]
		[RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid price format (e.g., 10.99)")]
		public decimal Price { get; set; }

		[Required]
		[Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
		public int StockQty { get; set; }

		[Required]
		public int CategoryId { get; set; }
	}
}

namespace _5BB_POS.ResponseModels
{
	public class ProductResponseModel
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; } = null!;
		public decimal Price { get; set; }
		public int StockQty { get; set; }
		public bool IsActive { get; set; }

		public int CategoryId { get; set; }
		public string? CategoryName { get; set; }
	}
}

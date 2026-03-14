namespace _5BB_POS.ResponseModels
{
    public class CategoryResponseModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}

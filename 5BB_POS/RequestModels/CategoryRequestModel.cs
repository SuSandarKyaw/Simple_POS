using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace _5BB_POS.RequestModels;

public class CategoryRequestModel
{
    [Required(ErrorMessage = "Category name is requried")]
    [DisplayName("Category နာမည်")]
	[RegularExpression(@"^[a-zA-Z0-9\s]{3,}$", ErrorMessage = "Name must be at least 3 characters.")]
	public string name { get; set; } = null!;

    public int id { get; set; }
}

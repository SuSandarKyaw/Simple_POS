using _5BB_POS.Models;
using _5BB_POS.Repositories.IRepository;
using _5BB_POS.RequestModels;
using _5BB_POS.ResponseModels;

namespace _5BB_POS.Services;

public class CategoryService
{
    protected readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryResponseModel>> IndexAsync()
    {
        var categories = await _categoryRepository.Index();

        return categories.Select(category => new CategoryResponseModel
        {
            CategoryId = category.CategoryId,
            CategoryName = category.Name,
            IsActive = category.IsActive
        });
    }

    public async Task<CategoryResponseModel> ShowAsync(int id)
    {
        var category = await _categoryRepository.Show(id);
        if (category == null)
        {
            throw new Exception($"Category id {id} Not Found");
        }
        return new CategoryResponseModel
        {
            CategoryId = category.CategoryId,
            CategoryName = category.Name,
            IsActive = category.IsActive
        };
    }

    public async Task<CategoryResponseModel> StoreAsync(CategoryRequestModel data)
    {
        var category = new TblCategory();
        category.Name = data.name;
		
		var res = await _categoryRepository.Store(category);

        return new CategoryResponseModel
        {
            CategoryId = res.CategoryId,
            CategoryName = res.Name,
            IsActive = res.IsActive
        };
    }

	public async Task<CategoryResponseModel> UpdateAsync(int id, CategoryRequestModel data)
	{
		
		var existingCategory = await _categoryRepository.Show(id);
		if (existingCategory == null)
		{
			throw new Exception($"Category id {id} Not Found");
		}

		
		existingCategory.Name = data.name;
		existingCategory.UpdatedAt = DateTime.Now;
		existingCategory.UpdatedBy = "systemUser";

		
		var res = await _categoryRepository.Update(existingCategory);

	
		return new CategoryResponseModel
		{
			CategoryId = res.CategoryId,
			CategoryName = res.Name,
			IsActive = res.IsActive
		};
	}

	public async Task DeleteAsync(int id)
	{
		var category = await _categoryRepository.Show(id);
		if (category == null)
		{
			throw new Exception("Category not found");
		}

		await _categoryRepository.Delete(id);
	}
}

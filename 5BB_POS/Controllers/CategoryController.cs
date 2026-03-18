using _5BB_POS.RequestModels;
using _5BB_POS.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;


namespace _5BB_POS.Controllers;

public class CategoryController : Controller
{
    protected readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var categories = await _categoryService.IndexAsync();
            Log.Information("Category List Fetch Success");
            return View(categories);
        }
        catch (Exception ex)
        {
            Log.Error("Cannot fetch categories: Error: " + ex);
            throw;
        }
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category = await _categoryService.ShowAsync(id);
        var data = new CategoryRequestModel
        {
            name = category.CategoryName,
            id = category.CategoryId
        };
        return View(data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> StoreCategory(CategoryRequestModel reqeust)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Input data is Invalid");
            }

            await _categoryService.StoreAsync(reqeust);
            Log.Information($"New Category - {reqeust.name} add success");
            return RedirectToAction("Index","Category");
        }
        catch (Exception ex)
        {
            Log.Error("Cannot Store Category. Error: " + ex);
            throw;
        }

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCategory(CategoryRequestModel reqeust)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Input data is Invalid");
            }

            await _categoryService.UpdateAsync(reqeust.id,reqeust);
            Log.Information($"New Category - {reqeust.name} add success");
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            Log.Error("Cannot Store Category. Error: " + ex);
            throw;
        }

    }
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteCategory(int id)
	{
		try
		{
			await _categoryService.DeleteAsync(id);
			TempData["Success"] = "Category has been successfully deactivated.";
		}
		catch (Exception ex)
		{
			TempData["Error"] = "Unable to delete category: " + ex.Message;
		}
		return RedirectToAction(nameof(Index));
	}
}

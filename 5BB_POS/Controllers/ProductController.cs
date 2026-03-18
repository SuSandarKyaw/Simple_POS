using _5BB_POS.RequestModels;
using _5BB_POS.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace _5BB_POS.Controllers
{
	public class ProductController : Controller
	{
		protected readonly ProductService _productService;
		protected readonly CategoryService _categoryService;

		public ProductController(ProductService productService, CategoryService categoryService)
		{
			_productService = productService;
			_categoryService = categoryService;
		}

		public async Task<IActionResult> Index()
		{
			try
			{
				var products = await _productService.IndexAsync();
				return View(products);
			}
			catch (Exception ex)
			{
				Log.Error("Cannot fetch products: Error: " + ex);
				throw;
			}
		}

		public async Task<IActionResult> Create()
		{
			var categories = await _categoryService.IndexAsync();
			ViewBag.Categories = categories;
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProductRequestModel request)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Categories = await _categoryService.IndexAsync();
				throw new Exception("Input data is Invalid");
			}
			try
			{
				await _productService.StoreAsync(request);
				Log.Information($"New product -{request.Name} add success");
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex) 
			{
				ViewBag.Categories = await _categoryService.IndexAsync();
				Log.Error("Store failed: " + ex);
				return View(request);
			}
		}
	}
}

using _5BB_POS.RequestModels;
using _5BB_POS.ResponseModels;
using _5BB_POS.Services;
using Azure.Core;
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

		public async Task<IActionResult> Edit(int id)
		{
			try
			{
				var product = await _productService.ShowAsync(id);

				var data = new ProductRequestModel
				{
					Name = product.ProductName,
					Price = product.Price,
					StockQty = product.StockQty,
					CategoryId = product.CategoryId
				};

				ViewBag.Categories = await _categoryService.IndexAsync();
				return View(data);
			}
			catch (Exception)
			{
				return NotFound();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateProduct(int id,ProductRequestModel request)
		{
			if (!ModelState.IsValid)
			{
				ViewBag.Categories = await _categoryService.IndexAsync();
				return View(request);
			}
			try
			{
				await _productService.UpdateAsync(id,request);
				Log.Information($"New Product - {request.Name} add success");
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				Log.Error(ex, "Update failed");

				ModelState.AddModelError(string.Empty, "Server error: " + ex.Message);

				ViewBag.Categories = await _categoryService.IndexAsync();
				return View(request);
			}
			
		}

		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _productService.DeleteAsync(id);
				Log.Information($"Product with id - {id} delete success");
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				Log.Error("Delete failed: " + ex);
				return RedirectToAction(nameof(Index));
			}
		}
	}
}

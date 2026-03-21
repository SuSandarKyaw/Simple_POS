using _5BB_POS.Models;
using _5BB_POS.Repositories;
using _5BB_POS.Repositories.IRepository;
using _5BB_POS.RequestModels;
using _5BB_POS.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace _5BB_POS.Services
{
	public class ProductService 
	{
		protected readonly IProductRepository _repository;

		public ProductService(IProductRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<ProductResponseModel>> IndexAsync()
		{
			var products =await _repository.Index();
			return products.Select(product => new ProductResponseModel
			{
				ProductId=product.ProductId,
				ProductName = product.Name,
				Price = product.Price,
				StockQty = product.StockQty,
				IsActive = product.IsActive,
				CategoryId = product.CategoryId,
				CategoryName = product?.Category.Name,
			} );
		}

		public async Task<ProductResponseModel> ShowAsync(int id)
		{
			var product = await _repository.Show(id);
			if(product == null)
			{
				throw new Exception("Product not found");
			}
			return new ProductResponseModel
			{
				ProductName = product.Name,
				Price = product.Price,
				StockQty = product.StockQty,
				IsActive = product.IsActive,
				CategoryId = product.CategoryId,
				CategoryName=product?.Category.Name,
			};
			
		}

		public async Task<ProductResponseModel> StoreAsync(ProductRequestModel request)
		{
			//var existingProduct = await _repository.GetByName(request.Name);
			//if (existingProduct != null) 
			//{
			//    existingProduct.Price = request.Price;
			//	existingProduct.StockQty += request.StockQty;
			//	await _repository.Update(existingProduct);
			//}
			var product = new TblProduct();
			product.Name = request.Name;
			product.Price = request.Price;
			product.StockQty = request.StockQty;
			product.CategoryId = request.CategoryId;
			

			var data= await _repository.Store(product);
			return new ProductResponseModel 
			{ 
			  ProductName= product.Name,
			  Price = product.Price,
			  StockQty = product.StockQty,
			  CategoryId=product.CategoryId,
			  IsActive = product.IsActive,
			};

		}

		public async Task<ProductResponseModel> UpdateAsync(int id, ProductRequestModel request)
		{
			var existingProduct = await _repository.Show(id);
			if (existingProduct == null)
			{
				throw new Exception($"Product-{request.Name} Not found");
			}
			existingProduct.Name = request.Name;
			existingProduct.Price = request.Price;
			existingProduct.StockQty = request.StockQty;
			existingProduct.CategoryId = request.CategoryId;
			

			var res= await _repository.Update(existingProduct);
			return new ProductResponseModel
			{
				CategoryName = res.Name,
				Price = res.Price,
				StockQty = res.StockQty,
				CategoryId = res.CategoryId,
			};

		}

		public async Task DeleteAsync(int id)
		{
			var existingProduct = await _repository.Show(id);
			if (existingProduct == null)
			{
				throw new Exception($"Product with id-{id} Not found");
			}
			await _repository.Delete(id);
		}

	}
}

using _5BB_POS.Models;
using _5BB_POS.Repositories;
using _5BB_POS.Repositories.IRepository;
using _5BB_POS.RequestModels;
using _5BB_POS.ResponseModels;
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
				ProductName = product.Name,
				Price = product.Price,
				StockQty = product.StockQty,
				IsActive = product.IsActive,
			} );
		}

		public async Task<ProductResponseModel> StoreAsync(ProductRequestModel request)
		{
			var existingProduct = await _repository.GetByName(request.Name);
			if (existingProduct != null) 
			{
			    existingProduct.Price = request.Price;
				existingProduct.StockQty += request.StockQty;
				await _repository.Update(existingProduct);
			}
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
	}
}

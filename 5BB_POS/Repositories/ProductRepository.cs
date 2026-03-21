using _5BB_POS.Models;
using _5BB_POS.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace _5BB_POS.Repositories
{
	public class ProductRepository : IProductRepository
	{
		protected readonly SimplePosContext _context;

		public ProductRepository(SimplePosContext context)
		{
			_context = context;
		}

		public async Task Delete(int id)
		{
			var product = await Show(id);
			if (product != null)
			{
				product.IsActive = false;
				await _context.SaveChangesAsync();
			}
		}

		public async Task<TblProduct?> GetByName(string name)
		{
			return await _context.TblProducts.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.IsActive);

		}

		public async Task<List<TblProduct>> Index()
		{
			return await _context.TblProducts.
				Include(x => x.Category).
				Where(x => x.IsActive).ToListAsync();
			
		}

		public async Task<TblProduct?> Show(int id)
		{
			return await _context.TblProducts.Include(x=> x.Category).FirstOrDefaultAsync(x => x.ProductId == id && x.IsActive==true);
		}

		public async Task<TblProduct> Store(TblProduct data)
		{
			await _context.TblProducts.AddAsync(data);
			await _context.SaveChangesAsync();
			return data;
		}

		public async Task<TblProduct> Update(TblProduct data)
		{
			_context.Entry(data).State = EntityState.Modified;
			_context.TblProducts.Update(data);
			await _context.SaveChangesAsync();
			return data;

		}
	}
}

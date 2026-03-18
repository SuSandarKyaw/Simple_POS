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

		public Task Delete(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<TblProduct?> GetByName(string name)
		{
			return await _context.TblProducts.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.IsActive);

		}

		public async Task<List<TblProduct>> Index()
		{
			return await _context.TblProducts.Where(x => x.IsActive).ToListAsync();
			
		}

		public Task<TblProduct?> Show(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<TblProduct> Store(TblProduct data)
		{
			await _context.TblProducts.AddAsync(data);
			await _context.SaveChangesAsync();
			return data;
		}

		public Task<TblProduct> Update(TblProduct data)
		{
			throw new NotImplementedException();
		}
	}
}

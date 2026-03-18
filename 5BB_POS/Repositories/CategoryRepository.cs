using _5BB_POS.Models;
using _5BB_POS.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;

namespace _5BB_POS.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly SimplePosContext _context;

        public CategoryRepository(SimplePosContext context)
        {
            _context = context;
        }

		public async Task Delete(int id)
		{
			var category = await Show(id);
			if (category != null)
			{
				category.IsActive = false; 
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<TblCategory>> Index()
        {
            return await _context.TblCategories.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<TblCategory?> Show(int id)
        {
            return await _context.TblCategories.Where(x => x.CategoryId == id).FirstOrDefaultAsync();
        }

		public async Task<TblCategory> Update(TblCategory data)
		{
			_context.TblCategories.Update(data);
			await _context.SaveChangesAsync();

			return data;
		}

		public async Task<TblCategory> Store(TblCategory data)
		{
			await _context.TblCategories.AddAsync(data);
			await _context.SaveChangesAsync();

			return data;
		}

		public async Task<TblCategory?> GetByName(string name)
		{
			return await _context.TblCategories.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.IsActive);
		}
	}
}

using Microsoft.EntityFrameworkCore;
using WebProject.Interfaces;
using WebProject.Models;

namespace WebProject.Repositories
{
    public class BrandRepository : IRepository<Brand>
    {
        protected readonly ApplicationContext _context;
        public BrandRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Brand item)
        {
            await _context.Brands.AddAsync(item);
        }

        public Task DeleteAsync(int id)
        {
            var brand = new Brand() { Id = id };
            _context.Brands.Remove(brand);
            return Task.CompletedTask;
        }

        public async Task<Brand?> GetAsync(int id)
        {
            return await _context.Brands.FindAsync(id);
        }

        public async Task<IEnumerable<Brand>> ListAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Brand item)
        {
            _context.Brands.Update(item);
            return Task.CompletedTask;
        }
    }
}

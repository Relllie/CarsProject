using Microsoft.EntityFrameworkCore;
using WebProject.Interfaces;
using WebProject.Models;

namespace WebProject.Repositories
{
    public class ModelRepository : IRepository<Model>
    {
        protected readonly ApplicationContext _context;
        public ModelRepository(ApplicationContext context)
        { 
            _context = context; 
        }

        public async Task CreateAsync(Model item)
        {
            await _context.Models.AddAsync(item);
        }

        public Task DeleteAsync(int id)
        {
            var model = new Model() { Id = id };
            _context.Models.Remove(model);
            return Task.CompletedTask;
        }

        public async Task<Model?> GetAsync(int id)
        {
            return await _context.Models.FindAsync(id);
        }

        public async Task<IEnumerable<Model>> ListAsync()
        {
            return await _context.Models.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task UpdateAsync(Model item)
        {
            _context.Models.Update(item);
            return Task.CompletedTask;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace WebProject.Models
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Model> Models => Set<Model>();
        public ApplicationContext([NotNullAttribute] DbContextOptions options, IWebHostEnvironment env) :base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>();
            modelBuilder.Entity<Model>();
            base.OnModelCreating(modelBuilder);
        }
    }
}

using FullStackPractice.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FullStackPractice.Persistence
{
    public class FullStackPracticeDbContext : DbContext
    {


        public FullStackPracticeDbContext(DbContextOptions<FullStackPracticeDbContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FullStackPracticeDbContext).Assembly);

        }
    }
}

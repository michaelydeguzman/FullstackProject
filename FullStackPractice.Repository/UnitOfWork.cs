using FullStackPractice.Persistence;
using FullStackPractice.Persistence.Models;
using FullStackPractice.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly FullStackPracticeDbContext _dbcontext;
        public IDepartmentRepository DepartmentRepository { get; private set; }
        public IEmployeeRepository EmployeeRepository { get; private set; }

        public UnitOfWork(FullStackPracticeDbContext dbContext)
        {
            _dbcontext = dbContext;
            DepartmentRepository = new DepartmentRepository(_dbcontext);
            EmployeeRepository = new EmployeeRepository(_dbcontext);
        }

        public async Task<int> Complete()
        {
            return await _dbcontext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}

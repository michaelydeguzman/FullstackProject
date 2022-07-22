using FullStackPractice.Domain.Entities;
using FullStackPractice.Persistence;

using FullStackPractice.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FullStackPractice.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(FullStackPracticeDbContext dbContext) : base(dbContext)
        {
        }

    }
}

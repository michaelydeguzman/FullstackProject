using FullStackPractice.Persistence;
using FullStackPractice.Persistence.Models;
using FullStackPractice.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FullStackPractice.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(FullStackPracticeDbContext dbContext) : base(dbContext)
        {
        }
    }
}

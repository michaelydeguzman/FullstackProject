using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDepartmentRepository DepartmentRepository { get; }

        IEmployeeRepository EmployeeRepository { get; }

        Task<int> Complete();
    }
}

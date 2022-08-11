using FullStackPractice.Contracts;
using FullStackPractice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FullStackPractice.Security
{
    public interface ISecurityManager
    {
        string GenerateToken(Employee user);

        Task<Employee> Authenticate(LoginUserDto userLogin);

        Employee GetCurrentUser(ClaimsIdentity identity);

        string GeneratePasswordHash(string password);
    }
}

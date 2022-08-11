using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Contracts
{
    public class LoginDetailDto
    {

        public int EmployeeId { get; set; }

        public string Email { get; set; }

        public string EmployeeName { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}

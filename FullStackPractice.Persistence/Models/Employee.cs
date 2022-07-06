using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullStackPractice.Persistence.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        public DateTime DateofJoining { get; set; }

        public string PhotoFileName { get; set; }

    }
}

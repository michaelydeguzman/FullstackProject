using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Services.Models
{
    public class ServiceResult<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public string OutputMessage { get; set; }

        public T Output { get; set; } 
    }
}

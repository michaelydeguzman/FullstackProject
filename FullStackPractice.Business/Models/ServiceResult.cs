using System;
using System.Collections.Generic;
using System.Text;

namespace FullStackPractice.Services.Models
{
    public class ServiceResponse<T> where T : class
    {
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public List<string> Messages { get; set; }
    }   
}

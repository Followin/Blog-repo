using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Infrastructure
{
    public class ValidationException : Exception
    {
        public String Property { get; set; }

        public ValidationException(String prop, String message) : base(message)
        {
            Property = prop;
        }
    }
}

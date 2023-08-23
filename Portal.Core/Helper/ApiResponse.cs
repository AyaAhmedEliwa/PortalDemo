using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Core.Helper
{
    public class ApiResponse<T>
    {
        public string Code { get; set; }
        public string Status { get; set; }
        public T? Success { get; set; }
        public T? Erorr { get; set; }
    }
}

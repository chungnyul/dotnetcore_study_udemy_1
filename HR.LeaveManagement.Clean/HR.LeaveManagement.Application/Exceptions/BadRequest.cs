using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class BadRequest : Exception
    {
        public BadRequest(string message) : base(message)
        {

        }
    }
}

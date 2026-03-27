using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException()
        : base("Access denied") { }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Exceptions
{
    public class NoIdProvidedException : Exception
    {
        public NoIdProvidedException() : base("No user id provided.")
        {

        }
    }
}
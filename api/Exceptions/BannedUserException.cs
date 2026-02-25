using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Exceptions
{
    public class BannedUserException : Exception
    {
        public BannedUserException() : base("This user is banned")
        {

        }
    }
}
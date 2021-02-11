using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Exceptions
{
    internal class IPv4ValueParsingException : Exception
    {
        public IPv4ValueParsingException()
        {
        }

        public IPv4ValueParsingException(string message) : base(message)
        {
        }
    }
}
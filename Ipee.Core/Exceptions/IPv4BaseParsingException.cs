using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Exceptions
{
    internal class IPv4BaseParsingException : Exception
    {
        public IPv4BaseParsingException()
        {
        }

        public IPv4BaseParsingException(string message) : base(message)
        {
        }
    }
}
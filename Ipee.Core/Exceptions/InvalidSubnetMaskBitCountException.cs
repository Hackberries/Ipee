using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Exceptions
{
    internal class InvalidSubnetMaskBitCountException : Exception
    {
        public InvalidSubnetMaskBitCountException() : base("Only specifications between 1 and 32 bits are valid.")
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Exceptions
{
    internal class NetworkOutOfRangeException : Exception
    {
        public NetworkOutOfRangeException() : base("Ihr Netzwerk ist zu klein! \n Wählen Sie eine kleine")
        {
        }

        public NetworkOutOfRangeException(string message) : base(message)
        {
        }
    }
}
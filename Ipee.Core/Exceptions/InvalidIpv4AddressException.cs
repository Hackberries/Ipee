using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Exceptions
{
    public class InvalidIpv4AddressException : Exception
    {
        public InvalidIpv4AddressException() : base("Bitte geben Sie eine gültige IP-Adresse ein!")
        {
        }

        public InvalidIpv4AddressException(string message) : base(message)
        {
        }
    }
}

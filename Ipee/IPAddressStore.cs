using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ipee
{
    public class IPAddressStore
    {
        public static IPAddressStore Instance { get; } = new IPAddressStore();

        private List<IPAddress> addresses = new List<IPAddress>();

        public void AddAddress(IPAddress address)
        {
            if (address is null)
                throw new NullReferenceException();

            if (addresses.Contains(address))
                throw new AdressAlreadyExistException();

            this.addresses.Add(address);
        }
    }
}
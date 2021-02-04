using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Store
{
    /// <summary>
    /// Regelt die Vergabe von IPv4-Adressen, hält diese, schlägt Adressen für neue Clients vor und stellt die Kompatibilität sicher.
    /// </summary>
    public class IPAddressStore
    {
        /// <summary>
        /// Singleton Instanz der Klasse.
        /// </summary>
        public static IPAddressStore Instance { get; } = new IPAddressStore();

        private List<IPAddress> addresses = new List<IPAddress>();

        /// <summary>
        /// Fügt eine neue IpAdresse hinzu.
        /// </summary>
        /// <exception cref="AdressAlreadyExistException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <param name="address">Die Adresse, welche hinzugefügt werden soll.</param>
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
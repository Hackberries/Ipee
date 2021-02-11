using Ipee.Core.Addressing;
using Ipee.Core.DataPersistence;
using Ipee.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Store
{
    /// <summary>
    /// Der AppStore hält den gesammten Zustand der Anwendung und dient als Schnittstelle zwischen Logik und Oberfläche.
    /// Regelt ebenso die Vergabe von IPv4-Adressen, hält diese, schlägt Adressen für neue Clients vor und stellt die Kompatibilität sicher.
    /// </summary>
    public class AppStore
    {
        /// <summary>
        /// Singleton Instanz der Klasse.
        /// </summary>
        public static AppStore Instance { get; set; } = new AppStore();

        private List<IPv4Address> addresses = new();

        private List<IPv4Subnet> subnets = new();

        public ConfigManager ConfigManager;

        public void Initialized(string configPath)
        {
            this.ConfigManager = new ConfigManager(configPath);
        }

        /// <summary>
        /// Fügt eine neue IpAdresse hinzu.
        /// </summary>
        /// <exception cref="AddressAlreadyExistException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        /// <param name="address">Die Adresse, welche hinzugefügt werden soll.</param>
        public void AddAddress(IPv4Address address)
        {
            if (address is null)
                throw new NullReferenceException();

            if (addresses.Contains(address))
                throw new AddressAlreadyExistException();

            this.addresses.Add(address);
        }

        public void AddSubnet(IPv4Subnet subnet)
        {
            if (subnet is null)
                throw new NullReferenceException();

            this.subnets.Add(subnet);
        }
    }
}
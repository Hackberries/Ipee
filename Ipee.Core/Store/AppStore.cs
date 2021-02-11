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

        public IPv4Network MainNetwork { get; set; } = new IPv4Network(new IPv4Address("0.0.0.0"), new IPv4SubnetMask("255.255.255.255"));

        public ConfigManager ConfigManager = new ConfigManager("./subnetConfig.json");

        public IEnumerable<IPv4Value> GivenAddresses => MainNetwork?.GivenAddresses;
    }
}
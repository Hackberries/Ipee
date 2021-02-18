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

        public Action OnRefresh { get; set; }

        public Action<IPv4Network> OnMainNetworkChanged { get; set; }

        private IPv4Network mainNetwork;

        public IPv4Network MainNetwork
        {
            get => mainNetwork;
            set
            {
                mainNetwork = value;
                OnMainNetworkChanged?.Invoke(value);
            }
        }

        public void Refresh()
        {
            OnRefresh?.Invoke();
        }

        public ConfigManager ConfigManager = new ConfigManager("./subnetConfig.json");

        public IEnumerable<IPv4Value> GivenAddresses => MainNetwork?.GivenAddresses;
    }
}
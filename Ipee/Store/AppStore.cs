using Ipee.data_persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Store
{
    /// <summary>
    /// Der AppStore hält den gesammten Zustand der Anwendung und dient als Schnittstelle zwischen Logik und Oberfläche.
    /// </summary>
    public class AppStore
    {
        /// <summary>
        /// Singleton Instanz der Klasse.
        /// </summary>
        public static AppStore Instance { get; set; } = new AppStore();

        private IPAddressStore addressStore = new IPAddressStore();
        private ConfigManager configManager = new ConfigManager();
    }
}
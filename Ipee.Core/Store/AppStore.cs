using Ipee.Core.DataPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ipee.Core.Store
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
        public ConfigManager ConfigManager;

        public void Initialized(string configPath)
        {
            this.ConfigManager = new ConfigManager(configPath);
        }
    }
}
using System;
using System.Collections.Generic;

// using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Ipee.Core.DataPersistence.Models;

namespace Ipee.Core.DataPersistence
{
    public class ConfigManager
    {
        private List<SubnetConfig> configFileList;

        // Constructor:
        public ConfigManager()
        {
            this.configFileList = ReadConfigFileAndGetAsList();
        }

        // Public functions
        public List<SubnetConfig> GetAllSubnetsAsList()
        {
            return this.configFileList;
        }

        public SubnetConfig FindSubnetConfigByDescription(string description)
        {
            foreach (SubnetConfig subnetConfig in this.configFileList)
            {
                if (description == subnetConfig.Description)
                {
                    return subnetConfig;
                }
            }

            return null;
        }

        public SubnetConfig FindSubnetConfigByClientIp(string ipAddress)
        {
            foreach (SubnetConfig subnetConfig in this.configFileList)
            {
                foreach (string address in subnetConfig.IpAddresses)
                {
                    if (ipAddress == address)
                    {
                        return subnetConfig;
                    }
                }
            }

            return null;
        }

        public SubnetConfig FindSubnetConfigBySubnetIp(string subnetIp)
        {
            foreach (SubnetConfig subnetConfig in this.configFileList)
            {
                if (subnetIp == subnetConfig.SubnetIp)
                {
                    return subnetConfig;
                }
            }

            return null;
        }

        // Private functions below ....
        private List<SubnetConfig> ReadConfigFileAndGetAsList()
        {
            /* NOTE:
             * ONLY LOCAL ABSOLUTH PATH WORKING! 
             * PLEASE ADJUST FOR YOUR OWN SYSTEM! */

            string filepath = "C:/Users/User1080/source/repos/Ipee/Ipee.Core/DataPersistence/files/subnetConfig.json";
            
            // string filepath = Path.Combine(Directory.GetCurrentDirectory()  , "files", "subnetConfig.json");

            string jsonString = File.ReadAllText(filepath, Encoding.ASCII);
            SubnetConfig[] subnetLists = System.Text.Json.JsonSerializer.Deserialize<SubnetConfig[]>(jsonString);

            List<SubnetConfig> subnetConfigList = new List<SubnetConfig>();

            foreach (SubnetConfig subnet in subnetLists)
            {
                SubnetConfig config = new SubnetConfig { };
                List<string> ipAddressList = new List<string>();

                config.Description = subnet.Description;
                config.SubnetIp = subnet.SubnetIp;
                var ipAddresses = subnet.IpAddresses;

                foreach (string ipAddress in ipAddresses)
                {
                    ipAddressList.Add(ipAddress);
                }

                config.IpAddresses = ipAddressList;
                subnetConfigList.Add(config);
            }

            if (subnetConfigList.Count == 0 || subnetConfigList == null)
            {
                Console.WriteLine("Unable to read config file!");

                return null;
            }

            Console.WriteLine("Config file successfully loaded!");

            return subnetConfigList;
        }

        private object ReadConfigFile()
        {
            string filepath = "/files/ipConfig.json";
            object configFileAsObject = JsonConvert.DeserializeObject(File.ReadAllText(filepath, Encoding.ASCII));

            /** Note: Create JsonElement from file if needed, otherwise delete before final version */
            string configFileAsString = File.ReadAllText(filepath, Encoding.ASCII);
            using JsonDocument doc = JsonDocument.Parse(configFileAsString);
            JsonElement configFile = doc.RootElement;
            var u1 = configFile[0];
            var u2 = configFile[1];
            Console.WriteLine(u1);
            Console.WriteLine(u2);
            Console.WriteLine(u1.GetProperty("Subnets"));
            Console.WriteLine(u1.GetProperty("Description"));
            Console.WriteLine(u2.GetProperty("Subnets"));
            Console.WriteLine(u2.GetProperty("Description"));

            var configObject = JsonConvert.DeserializeObject(File.ReadAllText(filepath, Encoding.ASCII));
            // Console.WriteLine(configObject);

            return u1;
        }
    }
}
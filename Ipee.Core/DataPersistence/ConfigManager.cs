using System;
using System.Collections.Generic;
using System.Linq;
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
        public ConfigManager(string filepath)
        {
            this.configFileList = ReadConfigFileAndGetAsList(filepath);
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
        private List<SubnetConfig> ReadConfigFileAndGetAsList(string filepath)
        {
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

        public void UpdateConfigFile(string filepath)
        {
            string jsonConfigFile = JsonConvert.SerializeObject(this.configFileList.ToArray());
            // write string to file
            File.WriteAllText(filepath, jsonConfigFile);
        }
    }
}
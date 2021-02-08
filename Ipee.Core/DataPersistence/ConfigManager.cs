using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Ipee.Core.DataPersistence.Models;
using Ipee.Core.Exceptions;

namespace Ipee.Core.DataPersistence
{
    public class ConfigManager
    {
        private List<SubnetConfig> configFileList;

        // Constructor:
        public ConfigManager(string filepath)
        {
            configFileList = ReadConfigFileAndGetAsList(filepath);
        }

        // Public functions
        public List<SubnetConfig> GetAllSubnetsAsList()
        {
            return configFileList;
        }

        public void AddSubnet(string subnetIp, string? description)
        {
            List<SubnetConfig> subnetConfigList = configFileList;

            SubnetConfig newSubnetConfig = new SubnetConfig();
            newSubnetConfig.SubnetIp = subnetIp;
            if (description is not null)
            {
                newSubnetConfig.Description = description;
            }
            subnetConfigList.Add(newSubnetConfig);
        }

        public void DeleteSubnet(string subnetIp)
        {
            List<SubnetConfig> subnetConfigList = configFileList;
            SubnetConfig subnetConfig = FindSubnetConfigBySubnetIp(subnetIp);

            if (subnetConfig != null)
            {
                bool isRemoved = subnetConfigList.Remove(subnetConfig);
                
                if (isRemoved == false)
                {
                    Console.WriteLine("Subnet connot be removed!");
                }
            }
        }

        public void EditIpAddress (string oldIpAddress, string newIpAddress)
        {
            // Note: Handle invalid input data
            if (oldIpAddress == "" || oldIpAddress == null)
            {
                throw new SomethingWentWrongException("Bitte wählen Sie eine IP-Adresse aus!");
            }

            if (newIpAddress == "" || newIpAddress == null)
            {
                throw new SomethingWentWrongException("Bitte geben sie eine IP-Adresse ein!");
            }

            try {
                var configFile = configFileList.Where((c) => c.IpAddresses.Contains(oldIpAddress)).FirstOrDefault();
                configFile.IpAddresses.Remove(oldIpAddress);
                configFile.IpAddresses.Add(newIpAddress);
            }
            catch (Exception exception) {
                throw new EntryNotFoundException("IP-Adresse konnte nicht geändert werden:" + exception.Message.ToString());
            };
        }

        public SubnetConfig GetSubnetConfigByDescription(string description)
        {
            // Note: Handle invalid description input
            if (description == "")
            {
                throw new SomethingWentWrongException("Bitte geben Sie Beschreibung ein!");
            }

            SubnetConfig foundConfigFile = configFileList.Where((config) => config.Description == description).FirstOrDefault();

            return foundConfigFile;

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

        public void UpdateConfigFile(string filepath)
        {
            string jsonConfigFile = JsonConvert.SerializeObject(this.configFileList.ToArray());
            // write string to file
            File.WriteAllText(filepath, jsonConfigFile);
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

                if (ipAddresses is not null)
                {
                    foreach (string ipAddress in ipAddresses)
                    {
                        ipAddressList.Add(ipAddress);
                    }

                    config.IpAddresses = ipAddressList;
                }
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
    }
}
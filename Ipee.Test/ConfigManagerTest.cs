using Ipee.Core.DataPersistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ipee.Test
{
    public class ConfigManagerTest
    {
        private const string filepath = "./subnetConfig.json";

        [Fact]
        public void CanBeCreated()
        {
            ConfigManager manager = new(filepath);
        }

        [Fact]
        public void CanFindByClientIp()
        {
            ConfigManager manager = new(filepath);
            Assert.NotNull(manager.FindSubnetConfigByClientIp("127.0.0.10"));
        }

        [Fact]
        public void CanFindBySubnetIp()
        {
            ConfigManager manager = new(filepath);
            Assert.NotNull(manager.FindSubnetConfigBySubnetIp("127.0.0.1"));
        }

        [Fact]
        public void CanGetAll()
        {
            ConfigManager manager = new(filepath);
            Assert.NotNull(manager.GetAllSubnetsAsList());
        }
    }
}
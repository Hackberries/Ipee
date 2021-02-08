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

        [Fact]
        public void CanAddItems()
        {
            ConfigManager manager = new(filepath);

            var before = manager.GetAllSubnetsAsList().Count();
            manager.AddSubnet("99.0.0.1", "Lorem Ipsum");
            var after = manager.GetAllSubnetsAsList().Count();

            Assert.True(after > before);
        }

        [Fact]
        public void CanRemoveItems()
        {
            ConfigManager manager = new(filepath);

            manager.AddSubnet("99.0.0.1", "Lorem Ipsum");
            var before = manager.GetAllSubnetsAsList().Count();
            manager.DeleteSubnet("99.0.0.1");
            var after = manager.GetAllSubnetsAsList().Count();

            Assert.True(after < before);
        }

        [Fact]
        public void CanFindSubnetConfigBySubnetIp()
        {
            ConfigManager manager = new(filepath);

            manager.AddSubnet("99.0.0.1", "Lorem Ipsum");
            var found = manager.FindSubnetConfigBySubnetIp("99.0.0.1");

            Assert.Equal("99.0.0.1", found.SubnetIp);
        }

        [Fact]
        public void CanEditItems()
        {
            ConfigManager manager = new(filepath);

            manager.AddSubnet("99.0.0.1", "Lorem Ipsum");
            var configItem = manager.FindSubnetConfigBySubnetIp("99.0.0.1");
            configItem.IpAddresses = new List<string>() { "1.1.1.1" };

            manager.EditIpAddress("1.1.1.1", "2.2.2.2");
            var found = manager.FindSubnetConfigByClientIp("2.2.2.2");

            Assert.NotNull(found);
        }
    }
}
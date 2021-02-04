using Ipee.Core.DataPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ipee.Test
{
    public class ConfigManagerTest
    {
        [Fact]
        public void CanBeCreated()
        {
            ConfigManager manager = new();
        }

        [Fact]
        public void CanFindByClientIp()
        {
            ConfigManager manager = new();
            Assert.NotNull(manager.FindSubnetConfigByClientIp("127.0.0.10"));
        }

        [Fact]
        public void CanFindBySubnetIp()
        {
            ConfigManager manager = new();
            Assert.NotNull(manager.FindSubnetConfigBySubnetIp("127.0.0.1"));
        }

        [Fact]
        public void CanGetAll()
        {
            ConfigManager manager = new();
            Assert.NotNull(manager.GetAllSubnetsAsList());
        }
    }
}
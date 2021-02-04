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
    }
}
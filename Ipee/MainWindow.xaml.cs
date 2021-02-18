using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Ipee.Core.Addressing;
using Ipee.Core.Store;


namespace Ipee
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenDebugWindowButtonClick(object sender, RoutedEventArgs e)
        {
            var debugWindow = new DebugWindow();
            debugWindow.Show();
        }

        private void OpenAddressConverterWindowButtonClick(object sender, RoutedEventArgs e)
        {
            var converterWindow = new IP4toIP6Window();
            converterWindow.Show();
        }

        private void Subnets_DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Subnets_DataGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Subnets_DataGrid_Initialized(object sender, EventArgs e)
        {
            var address1 = new IPv4Address("192.168.10.5");
            var mask1 = new IPv4SubnetMask("255.255.252.0");
            var network1 = new IPv4Network(address1, mask1);
            network1.AddAddress(new IPv4Address("192.168.10.7"));
            network1.AddAddress(new IPv4Address("192.168.10.8"));
            var subnet1 = new IPv4Network(new IPv4Address("192.168.10.5"), new IPv4SubnetMask("255.255.254.0"));
            subnet1.AddAddress(new IPv4Address("192.168.11.204"));
            subnet1.Description = "Just a test";
            network1.AddSubnet(subnet1);

            AppStore.Instance.MainNetwork = network1;

            var subnetList = AppStore.Instance.MainNetwork.Subnets.ToList();

            foreach (IPv4Network network in subnetList)
            {
                Subnets_DataGrid.Items.Add(network);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
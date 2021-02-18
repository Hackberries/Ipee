using Ipee.Core.Addressing;
using Ipee.Core.Store;
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

namespace Ipee
{
    /// <summary>
    /// Interaktionslogik für AddNetworkDialog.xaml
    /// </summary>
    public partial class AddNetworkDialog : Window
    {
        private IPv4Network network;

        public AddNetworkDialog(IPv4Network baseNetwork)
        {
            InitializeComponent();
            this.network = baseNetwork;
            SourceAddressBox.Text = baseNetwork.AllPossibleAddresses.First().ToString();
        }

        public AddNetworkDialog()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var address = new IPv4Address(SourceAddressBox.Text);

            var newNetwork = new IPv4Network(address, IPv4SubnetMask.ByAddressCount(int.Parse(AddresCountBox.Text)));
            newNetwork.Description = this.DescriptionBox.Text;

            if (network is not null)
            {
                this.network.AddSubnet(newNetwork);
            }
            else
            {
                AppStore.Instance.MainNetwork = newNetwork;
            }

            AppStore.Instance.Refresh();
        }
    }
}
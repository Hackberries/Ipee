using Ipee.Core.Converter;
using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Ipee.Core.DataPersistence.Models;
using Ipee.Core.Store;
using Ipee.Core.Exceptions;
using Ipee.Core.Addressing;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Text.Encodings.Web;

namespace Ipee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        public DebugWindow()
        {
            InitializeComponent();
        }

        // NOTE: Exception handling
        public void HandleSomethingWentWrongException(string text)
        {
            exception_textBlock.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var adress = new IPv4Address(IPBox.Text);
                IPv6Label.Content = AddressConverter.IPv4toIPv6(adress);
                AddressConverter.IPtoBinary(IPBox.Text);
            }
            catch (Exception ex)
            {
                TextBlock txtBlock = new TextBlock();
                txtBlock.TextWrapping = TextWrapping.Wrap;
                txtBlock.Text = ex.Message;
                IPv6Label.Content = txtBlock;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void subnetListBox_button_Click(object sender, RoutedEventArgs e)
        {
            subnetListBox.Items.Clear();
            List<SubnetConfig> subnetList = AppStore.Instance.ConfigManager.GetAllSubnetsAsList();

            foreach (SubnetConfig subnet in subnetList)
            {
                subnetListBox.Items.Add(subnet.Description + " - " + subnet.SubnetIp);
            }
        }

        private void UpdateConfigFile_button(object sender, RoutedEventArgs e)
        {
            try
            {
                AppStore.Instance.ConfigManager.UpdateConfigFile("./subnetConfig.json");
            }
            catch (Exception)
            {
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }

        private void ChooseSubnet_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ipAddressesListBox.Items.Clear();

                string selectedItem = subnetListBox.Items[subnetListBox.SelectedIndex].ToString();
                string[] splittedItem = selectedItem.Split(" - ");
                string selectedSubnetAsString = splittedItem[1];

                List<SubnetConfig> subnetList = AppStore.Instance.ConfigManager.GetAllSubnetsAsList();

                foreach (SubnetConfig subnetConfig in subnetList)
                {
                    List<string> ipAddresses = subnetConfig.IpAddresses;
                    SubnetConfig selectedSubnetConfig = AppStore.Instance.ConfigManager.FindSubnetConfigBySubnetIp(selectedSubnetAsString);

                    if (selectedSubnetConfig == subnetConfig)
                    {
                        if (ipAddresses is not null)
                        {
                            foreach (string ipAddress in ipAddresses)
                            {
                                ipAddressesListBox.Items.Add(ipAddress);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void subnetListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
        }

        private void chooseIpAddress_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedIpAddress = ipAddressesListBox.Items[ipAddressesListBox.SelectedIndex].ToString();
                ipAddress_textBox.Text = selectedIpAddress;
            }
            catch (Exception)
            {
            }
        }

        private void editIpAddress_button_Click(object sender, RoutedEventArgs e)
        {
            var newIpAddress = ipAddress_textBox.Text;
            string selectedIpAddress = ipAddressesListBox.Items[ipAddressesListBox.SelectedIndex].ToString();

            AppStore.Instance.ConfigManager.EditIpAddress(selectedIpAddress, newIpAddress);
        }

        private void ipAddress_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
        }

        private void newSubnet_button_Click(object sender, RoutedEventArgs e)
        {
            string newSubnetIp = newSubnet_textBox.Text;
            AppStore.Instance.ConfigManager.AddSubnet(newSubnetIp, "New subnet");
            subnetListBox_button_Click(sender, e);
        }

        private void deleteSubnet_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // TODO: needs to be validated!
                string selectedSubnet = subnetListBox.Items[subnetListBox.SelectedIndex].ToString();
                string[] subs = selectedSubnet.Split(" - ");
                // TODO: pay attention of splitting the string when no description is given !!! check return value!

                AppStore.Instance.ConfigManager.DeleteSubnet(subs[1]);
                subnetListBox.Items.Remove(selectedSubnet);
            }
            catch (Exception)
            {
            }
        }

        private void alleNetzwerke_Click(object sender, RoutedEventArgs e)
        {
            var address = new IPv4Address(IPBox.Text);
            var mask = new IPv4SubnetMask(MaskBox.Text);

            var subnet = new IPv4Network(address, mask);
            IPListe.Items.Clear();
            foreach (var item in subnet.AllPossibleAddresses.Take(100000))
            {
                IPListe.Items.Add(item);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var address = IPv4Address.Random();
            var mask = IPv4SubnetMask.Random();

            var view = new NetworkView(new IPv4Network(address, mask));
            view.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var address = new IPv4Address("192.168.10.5");
            var mask = new IPv4SubnetMask("255.255.252.0");
            var network = new IPv4Network(address, mask);
            network.AddAddress(new IPv4Address("192.168.10.7"));
            network.AddAddress(new IPv4Address("192.168.10.8"));
            var subnet = new IPv4Network(new IPv4Address("192.168.10.5"), new IPv4SubnetMask("255.255.254.0"));
            subnet.AddAddress(new IPv4Address("192.168.11.204"));
            network.AddSubnet(subnet);

            AppStore.Instance.MainNetwork = network;

            System.IO.File.WriteAllText(@"C:\Users\FSN\Desktop\Network.json", JsonSerializer.Serialize(AppStore.Instance.MainNetwork));
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var network = JsonSerializer.Deserialize<IPv4Network>(System.IO.File.ReadAllText(@"C:\Users\FSN\Desktop\Network.json"));
        }
    }
}
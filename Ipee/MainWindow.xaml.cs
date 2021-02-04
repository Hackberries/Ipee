using Ipee.Core.Converter;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Ipee.Core.DataPersistence.Models;
using Ipee.Core.Store;

namespace Ipee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppStore.Instance.Initialized("./subnetConfig.json");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IPAddress myAddress;
            var countDots = IPBox.Text.Split('.').Length - 1;
            if (countDots == 3)
            {
                if (System.Net.IPAddress.TryParse(IPBox.Text, out myAddress))
                {
                    IPv6Label.Content = AdressConverter.IPtoHex(IPBox.Text);
                    AdressConverter.IPtoBinary(IPBox.Text);
                }
                else
                {
                    TextBlock txtBlock = new TextBlock();
                    txtBlock.TextWrapping = TextWrapping.Wrap;
                    txtBlock.Text = "Bitte geben Sie eine GÜLTIGE IPv4 Adresse ein!";
                    IPv6Label.Content = txtBlock;
                }
            }
            else
            {
                TextBlock txtBlock = new TextBlock();
                txtBlock.TextWrapping = TextWrapping.Wrap;
                txtBlock.Text = "Bitte geben Sie eine GÜLTIGE IPv4 Adresse ein!";
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
            AppStore.Instance.ConfigManager.UpdateConfigFile("./subnetConfig.json");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ChooseSubnet_button_Click(object sender, RoutedEventArgs e)
        {
            ipAddressesListBox.Items.Clear();

            string selectedItem = subnetListBox.Items[subnetListBox.SelectedIndex].ToString();
            string[] splittedItem = selectedItem.Split(" - ");
            string selectedSubnetAsString = splittedItem[1];

            List<SubnetConfig> subnetList = AppStore.Instance.ConfigManager.GetAllSubnetsAsList();

            foreach (SubnetConfig subnetConfig in subnetList)
            {
                List<string> ipAddresses = subnetConfig.IpAddresses;
                SubnetConfig selectedSubnetConfig =  AppStore.Instance.ConfigManager.FindSubnetConfigBySubnetIp(selectedSubnetAsString);
                
                if (selectedSubnetConfig == subnetConfig )
                {
                    foreach (string ipAddress in ipAddresses)
                    {
                        ipAddressesListBox.Items.Add(ipAddress);
                    }
                }
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
            string selectedIpAddress = ipAddressesListBox.Items[ipAddressesListBox.SelectedIndex].ToString();
            ipAddress_textBox.Text = selectedIpAddress;
        }

        private void editIpAddress_button_Click(object sender, RoutedEventArgs e)
        {
            var newIpAddress = ipAddress_textBox.Text;
            AppStore.Instance.ConfigManager.F;
        }

        private void ipAddress_textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
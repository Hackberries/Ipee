using Ipee.Core.Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;

using Ipee.Core.DataPersistence;
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
            List<SubnetConfig> subnetList = AppStore.Instance.ConfigManager.GetAllSubnetsAsList();

            foreach (SubnetConfig subnet in subnetList)
            {
                subnetListBox.Items.Add(subnet.Description);
            }
        }
    }
}
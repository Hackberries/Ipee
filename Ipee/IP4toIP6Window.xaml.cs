using Ipee.Core.Addressing;
using Ipee.Core.Converter;
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
    /// Interaction logic for IP4toIP6Window.xaml
    /// </summary>
    public partial class IP4toIP6Window : Window
    {
        public IP4toIP6Window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

                try
                {
                    var adress = new IPv4Address(Ipv4TextBox.Text);
                    IPv6_Addresse.Content = AddressConverter.IPv4toIPv6(adress);
                    Binary_Addresse_Copy.Content = AddressConverter.IPtoBinary(Ipv4TextBox.Text);
                }
                catch (Exception ex)
                {
                    TextBlock txtBlock = new TextBlock();
                    txtBlock.TextWrapping = TextWrapping.Wrap;
                    txtBlock.Text = "Bitte geben sie eine ordentliche IPv4 Addresse ein!";
                    IPv6_Addresse.Content = txtBlock;
            }
            
        }
    }
}

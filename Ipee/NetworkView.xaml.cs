﻿using Ipee.Core.Addressing;
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
    /// Interaktionslogik für NetworkView.xaml
    /// </summary>
    public partial class NetworkView : Window
    {
        private IPv4Network network;

        public NetworkView(IPv4Network network)
        {
            this.network = network;

            InitializeComponent();

            this.SourceAddressBox.Text = this.network.SourceAddress.ToString();
            this.SubnetMaskBox.Text = this.network.SubnetMask.ToString();

            this.NetAddressText.Text = this.network.NetAddress.ToString();
            this.HostAddressText.Text = this.network.HostAddress.ToString();
            this.BroadcastAddressText.Text = this.network.BroadcastAddress.ToString();

            var allAddresses = network.AllPossibleAddresses.ToArray();

            PossibleAddressCountLabel.Content = allAddresses.Length;

            foreach (var address in allAddresses.Take(2500))
                PossibleAddressBox.Items.Add(address);
        }
    }
}
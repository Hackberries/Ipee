using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using Microsoft.Win32;

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
            AppStore.Instance.OnMainNetworkChanged += UpdateMainNetwork;
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

        }

        private void UpdateMainNetwork(IPv4Network network)
        {
            Subnets_DataGrid.Items.Clear();
            Subnets_DataGrid.Items.Add(AppStore.Instance.MainNetwork);
            foreach (IPv4Network subnet in AppStore.Instance.MainNetwork.Subnets)
            {
                Subnets_DataGrid.Items.Add(subnet);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_SubnetImport(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                var importedSubnet = File.ReadAllText(openFileDialog.FileName);
                var network = JsonSerializer.Deserialize<IPv4Network>(importedSubnet);
                AppStore.Instance.MainNetwork = network;
            }
                
        }

        private void MenuItem_SubnetAdd(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var importedSubnet = File.ReadAllText(openFileDialog.FileName);
                var network = JsonSerializer.Deserialize<IPv4Network>(importedSubnet);
                AppStore.Instance.MainNetwork.AddSubnet(network);
                UpdateMainNetwork(network);
            }

        }

        private void MenuItem_SubnetExport(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.Filter = "JSON files (*.json)| *.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                System.IO.File.WriteAllText(saveFileDialog.FileName, JsonSerializer.Serialize(AppStore.Instance.MainNetwork));
            }

        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)sender;
            var selectedNetwork = (IPv4Network)row.DataContext;
            var view = new NetworkView(selectedNetwork);
            view.Show();

        }
    }
}
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
using DataAccesLibrary.Internal.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RetailManagerUI.ViewModels;
using RetailManagerUI.ViewModels.Common.IoC;

namespace RetailManagerUI.Views
{
    /// <summary>
    /// Interaction logic for AddInvoiceDetails.xaml
    /// </summary>
    public partial class AddInvoiceDetails : Window
    {
        public AddInvoiceDetails()
        {
            InitializeComponent();
            AddInvoiceDetailsVM vm = new AddInvoiceDetailsVM(DependencyInjection.ServiceProvider.GetService<IStartUpData>());
            DataContext = vm;
        }

        private void NetPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NetPrice.Text == "0")
            {
                NetPrice.Text = string.Empty;
            }
        }

        private void Tax_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NetPrice.Text == "0")
            {
                NetPrice.Text = string.Empty;
            }
        }

        private void Quantity_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Quantity.Text == "0")
            {
                Quantity.Text = string.Empty;
            }
        }
    }
}

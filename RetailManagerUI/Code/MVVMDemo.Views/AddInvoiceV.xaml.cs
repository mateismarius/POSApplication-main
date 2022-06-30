using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Interactivity;
using RetailManagerUI.Models.Models;
using System.Collections.ObjectModel;
using System.Globalization;
using RetailManagerUI.ViewModels;
using RetailManagerUI.ViewModels.Common.IoC;
using Microsoft.Extensions.DependencyInjection;
using DataAccesLibrary.Internal.Interfaces;

namespace RetailManagerUI.Views
{
    /// <summary>
    /// Interaction logic for AddInvoiceV.xaml
    /// </summary>
    public partial class AddInvoiceV : Window
    {
        public AddInvoiceV()
        {
            InitializeComponent();
            AddInvoiceVM vm = new AddInvoiceVM(DependencyInjection.ServiceProvider.GetService<IInvoiceData>());
            DataContext = vm;
        }

        private void InvoiceDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            invoiceDate.Text = invoiceDatePicker.SelectedDate.ToString();
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView _list_view = sender as ListView;
            GridView _grid_view = _list_view.View as GridView;
            double _working_width = _list_view.ActualWidth - 35;
            _grid_view.Columns[0].Width = _working_width * 0.40;
            _grid_view.Columns[1].Width = _working_width * 0.15;
            _grid_view.Columns[2].Width = _working_width * 0.15;
            _grid_view.Columns[3].Width = _working_width * 0.15;
            _grid_view.Columns[4].Width = _working_width * 0.15;
        }

        private void Subtotal_GotFocus(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = string.Empty;
            }
        }

        private void Tax_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tax.Text == "0")
            {
                tax.Text = string.Empty;
            }
        }
    }
}

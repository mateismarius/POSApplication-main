using DataAccesLibrary.Internal.Interfaces;
using RetailManagerUI.ViewModels;
using RetailManagerUI.ViewModels.Common.IoC;
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
using Microsoft.Extensions.DependencyInjection;

namespace RetailManagerUI.Views
{
    /// <summary>
    /// Interaction logic for AddProductV.xaml
    /// </summary>
    public partial class AddProductV : Window
    {
        public AddProductV()
        {
            InitializeComponent();
            AddProductVM vm = new AddProductVM(DependencyInjection.ServiceProvider.GetService<IStartUpData>());
            DataContext = vm;
        }

        private void ProductDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            productDate.Text = productDatePicker.SelectedDate.ToString();
        }

        private void SalePrice_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SalePrice.Text == "0")
                SalePrice.Text = string.Empty;
        }

        private void Tax_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Tax.Text == "0")
                Tax.Text = string.Empty;
        }
    }
}

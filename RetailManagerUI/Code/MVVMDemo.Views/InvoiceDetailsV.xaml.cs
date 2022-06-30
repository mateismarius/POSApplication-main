using DataAccesLibrary.Internal.Interfaces;
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
using RetailManagerUI.ViewModels;

namespace RetailManagerUI.Views
{
    /// <summary>
    /// Interaction logic for InvoiceDetails.xaml
    /// </summary>
    public partial class InvoiceDetailsV : Window
    {
        public InvoiceDetailsV()
        {
            InitializeComponent();
            InvoiceDetailsVM vm = new InvoiceDetailsVM(DependencyInjection.ServiceProvider.GetService<IInvoiceData>());
            DataContext = vm;
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView _list_view = sender as ListView;
            GridView _grid_view = _list_view.View as GridView;
            double _working_width = _list_view.ActualWidth - 35;
            _grid_view.Columns[0].Width = _working_width * 0.30;
            _grid_view.Columns[1].Width = _working_width * 0.175;
            _grid_view.Columns[2].Width = _working_width * 0.175;
            _grid_view.Columns[3].Width = _working_width * 0.175;
            _grid_view.Columns[4].Width = _working_width * 0.175;
        }
    }
}

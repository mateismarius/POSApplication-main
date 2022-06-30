using RetailManagerUI.ViewModels;
using RetailManagerUI.ViewModels.Common.IoC;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using DataAccesLibrary.Internal.Interfaces;

namespace RetailManagerUI.Views
{
    /// <summary>
    /// Interaction logic for InvoiceV.xaml
    /// </summary>
    public partial class InvoiceV : Window
    {
        public InvoiceV()
        {
            InitializeComponent();
            InvoiceVM vm = new InvoiceVM(DependencyInjection.ServiceProvider.GetService<IInvoiceData>());
            DataContext = vm;
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView _list_view = sender as ListView;
            GridView _grid_view = _list_view.View as GridView;
            double _working_width = _list_view.ActualWidth - 35;
            _grid_view.Columns[0].Width = _working_width * 0.20;
            _grid_view.Columns[1].Width = _working_width * 0.20;
            _grid_view.Columns[2].Width = _working_width * 0.15;
            _grid_view.Columns[3].Width = _working_width * 0.15;
            _grid_view.Columns[4].Width = _working_width * 0.15;
            _grid_view.Columns[5].Width = _working_width * 0.15;
        }
    }
}

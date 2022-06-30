using DataAccesLibrary.Internal.Interfaces;
using RetailManagerUI.ViewModels.Sales;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using RetailManagerUI.ViewModels.Common.IoC;

namespace RetailManagerUI.Views
{
    /// <summary>
    /// Interaction logic for SalesV.xaml
    /// </summary>
    public partial class SalesV : Window
    {
        public SalesV()
        {
            InitializeComponent();
            SalesVM vm = new SalesVM(DependencyInjection.ServiceProvider.GetService<ISalesData>());
            DataContext = vm;
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView _list_view = sender as ListView;
            GridView _grid_view = _list_view.View as GridView;
            double _working_width = _list_view.ActualWidth - 35;
            _grid_view.Columns[0].Width = _working_width * 0.20;
            _grid_view.Columns[1].Width = _working_width * 0.30;
            _grid_view.Columns[2].Width = _working_width * 0.25;
            _grid_view.Columns[3].Width = _working_width * 0.25;
        }
    }
}

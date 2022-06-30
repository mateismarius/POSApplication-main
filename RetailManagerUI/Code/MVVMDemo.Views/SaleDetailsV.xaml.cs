using DataAccesLibrary.Internal.Interfaces;
using RetailManagerUI.ViewModels.Common.IoC;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using RetailManagerUI.ViewModels;

namespace RetailManagerUI.Views
{
    /// <summary>
    /// Interaction logic for SaleDetailsV.xaml
    /// </summary>
    public partial class SaleDetailsV : Window
    {
        public SaleDetailsV()
        {
            InitializeComponent();
            SaleDetailsVM vm = new SaleDetailsVM(DependencyInjection.ServiceProvider.GetService<ISalesData>());
            DataContext = vm;
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView _list_view = sender as ListView;
            GridView _grid_view = _list_view.View as GridView;
            double _working_width = _list_view.ActualWidth - 35;
            _grid_view.Columns[0].Width = _working_width * 0.20;
            _grid_view.Columns[1].Width = _working_width * 0.25;
            _grid_view.Columns[2].Width = _working_width * 0.175;
            _grid_view.Columns[3].Width = _working_width * 0.175;
            _grid_view.Columns[4].Width = _working_width * 0.20;
        }
    }
}

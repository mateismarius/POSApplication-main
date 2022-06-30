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
    /// Interaction logic for ProductV.xaml
    /// </summary>
    public partial class ProductV : Window
    {
        public ProductV()
        {
            InitializeComponent();
            ProductVM vm = new ProductVM(DependencyInjection.ServiceProvider.GetService<IStartUpData>());
            DataContext = vm;
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView _list_view = sender as ListView;
            GridView _grid_view = _list_view.View as GridView;
            double _working_width = _list_view.ActualWidth - 35;
            _grid_view.Columns[0].Width = _working_width * 0.20;
            _grid_view.Columns[1].Width = _working_width * 0.15;
            _grid_view.Columns[2].Width = _working_width * 0.10;
            _grid_view.Columns[3].Width = _working_width * 0.10;
            _grid_view.Columns[4].Width = _working_width * 0.10;
            _grid_view.Columns[5].Width = _working_width * 0.10;
            _grid_view.Columns[6].Width = _working_width * 0.075;
            _grid_view.Columns[7].Width = _working_width * 0.075;
            _grid_view.Columns[8].Width = _working_width * 0.10;
        }
    }
}

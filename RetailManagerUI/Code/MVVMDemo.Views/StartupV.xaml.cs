using RetailManagerUI.ViewModels;
using RetailManagerUI.ViewModels.Common.IoC;
using RetailManagerUI.ViewModels.Common.MessageBox;
using RetailManagerUI.Views.Common.UI;
using RetailManagerUI.Views.Common.UIFactory;
using RetailManagerUI.Views.Dispatching;
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
using DataAccesLibrary.Internal.Interfaces;
using RetailManagerUI.Models.Models;
using RetailManagerUI.ViewModels.Authentication;
using RetailManagerUI.Views.UI.Authentication;

namespace RetailManagerUI.Views
{
    /// <summary>
    /// Interaction logic for StartupV.xaml
    /// </summary>
    public partial class StartupV : Window
    {
        public StartupV()
        {
            InitializeComponent();
            StartupVM vm = new StartupVM(DependencyInjection.ServiceProvider.GetService<IStartUpData>());
            DataContext = vm;
            (DataContext as StartupVM).ClosingView += (sender, e) => Close();
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView _list_view = sender as ListView;
            GridView _grid_view = _list_view.View as GridView;
            double _working_width = _list_view.ActualWidth - 25;
            _grid_view.Columns[0].Width = _working_width * 0.075;
            _grid_view.Columns[1].Width = _working_width * 0.30;
            _grid_view.Columns[2].Width = _working_width * 0.075;
            _grid_view.Columns[3].Width = _working_width * 0.075;
            _grid_view.Columns[4].Width = _working_width * 0.075;
            _grid_view.Columns[5].Width = _working_width * 0.125;
            _grid_view.Columns[6].Width = _working_width * 0.125;
            _grid_view.Columns[7].Width = _working_width * 0.15;
        }

        private void ThemeChanged_Click(object sender, RoutedEventArgs e)
        {
            string _chosen_theme = (sender as MenuItem).Header.ToString();
            Application.Current.Resources.MergedDictionaries[0].Source = new Uri("/Themes/" + _chosen_theme + ".xaml", UriKind.RelativeOrAbsolute);
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            acbProducts.Focus();
        }
    }
}

using DataAccesLibrary.Internal.Interfaces;
using RetailManagerUI.ViewModels.Authentication;
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

namespace RetailManagerUI.Views.UI.Authentication
{
    /// <summary>
    /// Interaction logic for RegisterV.xaml
    /// </summary>
    public partial class RegisterV : Window
    {
        public RegisterV()
        {
            InitializeComponent();
            RegisterVM vm = new RegisterVM(DependencyInjection.ServiceProvider.GetService<IUserData>());
            DataContext = vm;
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null && (sender as PasswordBox).IsFocused)
                ((dynamic)DataContext).Password = (sender as PasswordBox).SecurePassword;
        }

        private void PasswordChangedConfirm(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null && (sender as PasswordBox).IsFocused)
                ((dynamic)DataContext).ConfirmPassword = (sender as PasswordBox).SecurePassword;
        }
    }
}

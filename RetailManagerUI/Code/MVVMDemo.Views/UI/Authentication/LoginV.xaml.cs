using DataAccesLibrary.Internal.Interfaces;
using RetailManagerUI.ViewModels;
using RetailManagerUI.ViewModels.Authentication;
using RetailManagerUI.ViewModels.Common.IoC;
using RetailManagerUI.ViewModels.Common.MessageBox;
using RetailManagerUI.Views.Common.UI;
using RetailManagerUI.Views.Common.UIFactory;
using RetailManagerUI.Views.Dispatching;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using RetailManagerUI.ViewModels.Sales;

namespace RetailManagerUI.Views.UI.Authentication
{
    /// <summary>
    /// Interaction logic for LoginV.xaml
    /// </summary>
    public partial class LoginV : Window
    {
        public LoginV()
        {
            InitializeComponent();
            DependencyInjection.Instance.RegisterDispatcherServices(typeof(ApplicationDispatcher));
            LoginVM vm = new LoginVM(DependencyInjection.ServiceProvider.GetService<IUserData>());
            DataContext = vm;
            vm.LoginShown += Vm_LoginShown;
            // register the application dispatcher
            // map abstract implementation of views to names of viewmodels
            StartupVM.UIDispatcher.Add(nameof(AddInvoiceVM), new UIFactory(typeof(AddInvoiceV).Namespace + "." + nameof(AddInvoiceV)));
            StartupVM.UIDispatcher.Add(nameof(InvoiceVM), new UIFactory(typeof(InvoiceV).Namespace + "." + nameof(InvoiceV)));
            StartupVM.UIDispatcher.Add(nameof(InvoiceDetailsVM), new UIFactory(typeof(InvoiceDetailsV).Namespace + "." + nameof(InvoiceDetailsV)));
            StartupVM.UIDispatcher.Add(nameof(MsgBoxVM), new UIFactory(typeof(MsgBoxV).Namespace + "." + nameof(MsgBoxV)));
            StartupVM.UIDispatcher.Add(nameof(ProductVM), new UIFactory(typeof(ProductV).Namespace + "." + nameof(ProductV)));
            StartupVM.UIDispatcher.Add(nameof(AddProductVM), new UIFactory(typeof(AddProductV).Namespace + "." + nameof(AddProductV)));
            StartupVM.UIDispatcher.Add(nameof(AddInvoiceDetailsVM), new UIFactory(typeof(AddInvoiceDetails).Namespace + "." + nameof(AddInvoiceDetails)));
            StartupVM.UIDispatcher.Add(nameof(LoginVM), new UIFactory(typeof(LoginV).Namespace + "." + nameof(LoginV)));
            StartupVM.UIDispatcher.Add(nameof(StartupVM), new UIFactory(typeof(StartupV).Namespace + "." + nameof(StartupV)));
            StartupVM.UIDispatcher.Add(nameof(RegisterVM), new UIFactory(typeof(RegisterV).Namespace + "." + nameof(RegisterV)));
            StartupVM.UIDispatcher.Add(nameof(SalesVM), new UIFactory(typeof(SalesV).Namespace + "." + nameof(SalesV)));
            StartupVM.UIDispatcher.Add(nameof(SaleDetailsVM), new UIFactory(typeof(SaleDetailsV).Namespace + "." + nameof(SaleDetailsV)));
            // allow closing the View from within the ViewModel, without breaking MVVM patterns
            (DataContext as LoginVM).ClosingView += (sender, e) => Close();
        }

        private void Vm_LoginShown(object sender, EventArgs e)
        {
            pwd.Password = "";
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            (DataContext as LoginVM).LoginShown -= Vm_LoginShown;
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null && (sender as PasswordBox).IsFocused)
                ((dynamic)DataContext).Password = (sender as PasswordBox).SecurePassword;
            
        }
    }
}

using RetailManagerUI.ViewModels.Common.Interfaces;
using System;
using System.Windows;

namespace RetailManagerUI.Views.Common.UIFactory
{
    public class UIFactory : IUserInterface
    {
        private readonly string instance;
        private Window wnd;
        public UIFactory(string windowName)
        {
            instance = windowName;
        }

        public void ShowModalUI()
        {
            wnd = Activator.CreateInstance(Type.GetType(instance, true)) as Window;
            wnd.ShowDialog();
        }

        public void ShowModalUI(string id, string type)
        {
            wnd = Activator.CreateInstance(Type.GetType(instance, true)) as Window;
            dynamic vm = Convert.ChangeType(wnd.DataContext, Type.GetType(type));
            vm.Id = id;
            wnd.ShowDialog();
        }

        public void ShowModalUI(string type, params object[] arguments)
        {
            wnd = Activator.CreateInstance(Type.GetType(instance, true), arguments) as Window;
            _ = Convert.ChangeType(wnd.DataContext, Type.GetType(type));
            wnd.ShowDialog();
        }

        

        public void CloseUI()
        {
            if (wnd != null)
                wnd.Close();
        }

        public void HideUI()
        {
            if (wnd != null)
                wnd.Hide();
        }

        

        public void ShowUI()
        {
            wnd = Activator.CreateInstance(Type.GetType(instance, true)) as Window;
            wnd.Show();
        }

        public void ShowUI(string id, string type)
        {
            wnd = Activator.CreateInstance(Type.GetType(instance, true)) as Window;
            dynamic vm = Convert.ChangeType(wnd.DataContext, Type.GetType(type));
            vm.Id = id;
            wnd.Show();
        }

        public void ShowUI(string type, params object[] arguments)
        {
            wnd = Activator.CreateInstance(Type.GetType(instance, true), arguments) as Window;
            _ = Convert.ChangeType(wnd.DataContext, Type.GetType(type));
            wnd.Show();
        }
    }
}

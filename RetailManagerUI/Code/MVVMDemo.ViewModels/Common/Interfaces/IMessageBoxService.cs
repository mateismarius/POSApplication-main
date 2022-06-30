using RetailManagerUI.ViewModels.Common.Enums;

namespace RetailManagerUI.ViewModels.Common.Interfaces
{
    public interface IMessageBoxService
    {
        MessageBoxResult Show(string message);
        MessageBoxResult Show(string message, string title);
        MessageBoxResult Show(string message, string title, MessageBoxButton buttons);
        MessageBoxResult Show(string message, string title, MessageBoxButton buttons, MessageBoxImage image);
        void ChangeInjectedDialogResult(bool? dialogResult);
    }
}

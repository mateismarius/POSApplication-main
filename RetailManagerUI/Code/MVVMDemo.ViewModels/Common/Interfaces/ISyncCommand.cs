using System.Windows.Input;

namespace RetailManagerUI.ViewModels.Common.Interfaces
{
    public interface ISyncCommand<T> : ICommand
    {
        void ExecuteSync(T param);
        bool CanExecute(T param);
    }
}

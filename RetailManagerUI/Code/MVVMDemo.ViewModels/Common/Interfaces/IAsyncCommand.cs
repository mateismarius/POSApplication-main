using System.Threading.Tasks;
using System.Windows.Input;

namespace RetailManagerUI.ViewModels.Common.Interfaces
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }

    public interface IAsyncCommand<T> : ICommand
    {
        Task ExecuteAsync(T param);
        bool CanExecute(T param);
    }
}

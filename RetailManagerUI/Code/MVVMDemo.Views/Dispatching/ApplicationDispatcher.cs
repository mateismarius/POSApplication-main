using RetailManagerUI.ViewModels.Common.Interfaces;
using System;
using System.Windows;
using System.Windows.Threading;

namespace RetailManagerUI.Views.Dispatching
{
    public class ApplicationDispatcher : IDispatcher
    {
        Dispatcher UnderlyingDispatcher
        {
            get
            {
                if (Application.Current == null)
                    throw new InvalidOperationException("You must call this method from within a running WPF application!");
                if (Application.Current.Dispatcher == null)
                    throw new InvalidOperationException("You must call this method from within a running WPF application with an active dispatcher!");
                return Application.Current.Dispatcher;
            }
        }

        public TResult Dispatch<TResult>(Func<TResult> callback)
        {
            return UnderlyingDispatcher.Invoke(callback);
        }

        public void Dispatch(Delegate method, params object[] args)
        {
            UnderlyingDispatcher.Invoke(DispatcherPriority.Background, method, args);
        }
    }
}

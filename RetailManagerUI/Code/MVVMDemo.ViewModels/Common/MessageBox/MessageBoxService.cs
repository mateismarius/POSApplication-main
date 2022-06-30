using RetailManagerUI.ViewModels.Common.Enums;
using RetailManagerUI.ViewModels.Common.Interfaces;
using System;

namespace RetailManagerUI.ViewModels.Common.MessageBox
{
    public class MessageBoxService : IMessageBoxService
    {
        private readonly IDispatcher dispatcher;

        public MessageBoxService(IDispatcher _dispatcher)
        {
            dispatcher = _dispatcher;
        }

        public void ChangeInjectedDialogResult(bool? dialogResult)
        {
            throw new NotSupportedException("Dialog results are set from UI only!");
        }

        public MessageBoxResult Show(string message)
        {
            return (MessageBoxResult)(dispatcher?.Dispatch(new Func<MessageBoxResult>(() =>
            {
                return new MsgBoxVM().Show(message);
            })));
        }

        public MessageBoxResult Show(string message, string title)
        {
            return (MessageBoxResult)(dispatcher?.Dispatch(new Func<MessageBoxResult>(() =>
            {
                return new MsgBoxVM().Show(message, title);
            })));
        }

        public MessageBoxResult Show(string message, string title, MessageBoxButton buttons)
        {
            return (MessageBoxResult)(dispatcher?.Dispatch(new Func<MessageBoxResult>(() =>
            {
                return new MsgBoxVM().Show(message, title, buttons);
            })));
        }

        public MessageBoxResult Show(string message, string title, MessageBoxButton buttons, MessageBoxImage image)
        {
            return (MessageBoxResult)(dispatcher?.Dispatch(new Func<MessageBoxResult>(() =>
            {
                return new MsgBoxVM().Show(message, title, buttons, image);
            })));
        }
    }
}

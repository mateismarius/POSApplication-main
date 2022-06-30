using RetailManagerUI.ViewModels.Common.Enums;
using RetailManagerUI.ViewModels.Common.Interfaces;
using System;

namespace RetailManagerUI.ViewModels.Common.MessageBox
{
    public class ConsoleService : IMessageBoxService
    {
        private bool? injectedDialogResult = null;

        public ConsoleService(bool? _injectedDialogResult)
        {
            injectedDialogResult = _injectedDialogResult;
        }

        public void ChangeInjectedDialogResult(bool? dialogResult)
        {
            injectedDialogResult = dialogResult;
        }

        public MessageBoxResult Show(string message)
        {
            Console.WriteLine("Message was " + message);
            switch (injectedDialogResult)
            {
                case true:
                    Console.WriteLine("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    Console.WriteLine("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }

        public MessageBoxResult Show(string message, string title)
        {
            Console.WriteLine("Message was " + message);
            Console.WriteLine("Title was " + title);
            switch (injectedDialogResult)
            {
                case true:
                    Console.WriteLine("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    Console.WriteLine("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }

        public MessageBoxResult Show(string message, string title, MessageBoxButton buttons)
        {
            Console.WriteLine("Message was " + message);
            Console.WriteLine("Title was " + title);
            switch (injectedDialogResult)
            {
                case true:
                    Console.WriteLine("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    Console.WriteLine("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }

        public MessageBoxResult Show(string message, string title, MessageBoxButton buttons, MessageBoxImage image)
        {
            Console.WriteLine("Message was " + message);
            Console.WriteLine("Title was " + title);
            switch (injectedDialogResult)
            {
                case true:
                    Console.WriteLine("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                    Console.WriteLine("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                    Console.WriteLine("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }
    }
}

using RetailManagerUI.ViewModels.Common.Enums;
using RetailManagerUI.ViewModels.Common.Interfaces;
using RetailManagerUI.ViewModels.Common.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RetailManagerUI.ViewModels.Common.MessageBox
{
    public class LoggingService : IMessageBoxService
    {
        private readonly ILoggerManager<LoggingService> logger = DependencyInjection.ServiceProvider.GetService<ILoggerManager<LoggingService>>();
        private bool? injectedDialogResult = null;

        public LoggingService(bool? _injectedDialogResult)
        {
            injectedDialogResult = _injectedDialogResult;
        }

        public void ChangeInjectedDialogResult(bool? dialogResult)
        {
            injectedDialogResult = dialogResult;
        }

        public MessageBoxResult Show(string message)
        {
           logger.LogInfo("Message was " + message);
            switch (injectedDialogResult)
            {
                case true:
                   logger.LogInfo("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                   logger.LogInfo("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                   logger.LogInfo("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                   logger.LogInfo("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }

        public MessageBoxResult Show(string message, string title)
        {
           logger.LogInfo("Message was " + message);
           logger.LogInfo("Title was " + title);
            switch (injectedDialogResult)
            {
                case true:
                   logger.LogInfo("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                   logger.LogInfo("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                   logger.LogInfo("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                   logger.LogInfo("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }

        public MessageBoxResult Show(string message, string title, MessageBoxButton buttons)
        {
           logger.LogInfo("Message was " + message);
           logger.LogInfo("Title was " + title);
            switch (injectedDialogResult)
            {
                case true:
                   logger.LogInfo("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                   logger.LogInfo("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                   logger.LogInfo("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                   logger.LogInfo("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }

        public MessageBoxResult Show(string message, string title, MessageBoxButton buttons, MessageBoxImage image)
        {
           logger.LogInfo("Message was " + message);
           logger.LogInfo("Title was " + title);
            switch (injectedDialogResult)
            {
                case true:
                   logger.LogInfo("Response was: MessageBoxResult.Yes");
                    return MessageBoxResult.Yes;
                case false:
                   logger.LogInfo("Response was: MessageBoxResult.No");
                    return MessageBoxResult.No;
                case null:
                   logger.LogInfo("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
                default:
                   logger.LogInfo("Response was: MessageBoxResult.Cancel");
                    return MessageBoxResult.Cancel;
            }
        }
    }
}

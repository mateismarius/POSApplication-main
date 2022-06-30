using System;

namespace RetailManagerUI.ViewModels.Common.Interfaces
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}

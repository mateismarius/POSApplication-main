
using System;
using System.Threading.Tasks;
using RetailManagerUI.ViewModels.Common.Interfaces;

namespace RetailManagerUI.ViewModels.Common.Extensions
{
    public static class TaskUtilities
    {
        public static async void FireAndForgetAsync(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}

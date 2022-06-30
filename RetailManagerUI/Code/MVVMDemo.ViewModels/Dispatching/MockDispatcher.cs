using RetailManagerUI.ViewModels.Common.Interfaces;
using System;

namespace RetailManagerUI.ViewModels.Dispatching
{
    public class MockDispatcher : IDispatcher
    {
        public void Dispatch(Delegate method, params object[] args)
        {
            method.DynamicInvoke((object)args);
        }

        public TResult Dispatch<TResult>(Func<TResult> callback)
        {
            throw new NotImplementedException();
        }
    }
}

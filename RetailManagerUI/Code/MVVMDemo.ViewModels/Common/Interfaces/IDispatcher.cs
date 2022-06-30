using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagerUI.ViewModels.Common.Interfaces
{
    public interface IDispatcher
    {
        TResult Dispatch<TResult>(Func<TResult> callback);
        void Dispatch(Delegate method, params object[] args);
    }
}

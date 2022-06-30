using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagerUI.ViewModels.Common.Interfaces
{
    public interface IUserInterface
    {
        void ShowUI();
        void HideUI();
        void ShowUI(string id, string type);
        void ShowUI(string type, params object[] arguments);
        void ShowModalUI();
        void ShowModalUI(string id, string type);
        void ShowModalUI(string type, params object[] arguments);
        void CloseUI();

    }
}

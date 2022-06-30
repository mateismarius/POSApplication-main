using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RetailManagerUI.Models.Common.MVVM
{
    public class LightBaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies the UI about a binded property's value being changed
        /// </summary>
        /// <param name="_propertyName">The property that had the value changed</param>
        public void Notify([CallerMemberName] string _propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_propertyName));
        }
    }
}

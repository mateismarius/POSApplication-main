// Written by: Yulia Danilova
/// Creation Date: 5th of November, 2019
/// Purpose: Various data structures used throughout the application
#region ========================================================================= USING =====================================================================================
using System.Windows.Controls;
#endregion

namespace RetailManagerUI.Models.Common
{
    /// <summary>
    /// Used for passing more than one PasswordBox control as parameter, from the View (must check New Password and Confirm New Password for being equal, and Old Password for being correct)
    /// </summary>
    public class PasswordBoxControl
    {
        public PasswordBox FirstPasswordBoxValue { get; set; }
        public PasswordBox SecondPasswordBoxValue { get; set; }
        public PasswordBox ThirdPasswordBoxValue { get; set; }
    }

    /// <summary>
    /// Used as data of combobox elements
    /// </summary>
    public class ComboboxItemData
    {
        public string Text { get; set; }

        public object Value { get; set; }

        public object Hover { get; set; }
    }
}

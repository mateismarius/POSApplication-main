/// Written by: Ciprian Horeanu
/// Creation Date: 20th of October, 2019
/// Purpose: Handles Title Bar double click behavior for Windows
#region ========================================================================= USING =====================================================================================
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
#endregion

namespace RetailManagerUI.StyleableWindow
{
    public static class ControlDoubleClickBehavior
    {
        #region ========================================================== DEPENDENCY PROPERTIES ============================================================================
        public static readonly DependencyProperty ExecuteCommand = DependencyProperty.RegisterAttached("ExecuteCommand",          
            typeof(ICommand), typeof(ControlDoubleClickBehavior),
            new UIPropertyMetadata(null, OnExecuteCommandChanged));

        public static readonly DependencyProperty ExecuteCommandParameter = DependencyProperty.RegisterAttached("ExecuteCommandParameter",
            typeof(Window), typeof(ControlDoubleClickBehavior));
        #endregion

        #region ================================================================= METHODS ===================================================================================
        public static ICommand GetExecuteCommand(DependencyObject _object)
        {
            return (ICommand)_object.GetValue(ExecuteCommand);
        }

        public static void SetExecuteCommand(DependencyObject _object, ICommand _command)
        {
            _object.SetValue(ExecuteCommand, _command);
        }

        public static Window GetExecuteCommandParameter(DependencyObject _object)
        {
            return (Window) _object.GetValue(ExecuteCommandParameter);
        }

        public static void SetExecuteCommandParameter(DependencyObject _object, ICommand _command)
        {
            _object.SetValue(ExecuteCommandParameter, _command);
        }

        private static void OnExecuteCommandChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Control control)
                control.MouseDoubleClick += control_MouseDoubleClick;
        }

        static void control_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is Control control)
            {
                ICommand command = control.GetValue(ExecuteCommand) as ICommand;
                object commandParameter = control.GetValue(ExecuteCommandParameter);
                if (command.CanExecute(e))
                    command.Execute(commandParameter);
            }
        }
        #endregion
    }
}
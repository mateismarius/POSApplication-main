using RetailManagerUI.ViewModels.Common.Enums;
using RetailManagerUI.ViewModels.Common.Interfaces;
using RetailManagerUI.ViewModels.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagerUI.ViewModels.Common.MessageBox
{
    public class MsgBoxVM : BaseModel
    {
        public SyncCommand Yes_Command { get; private set; }
        public SyncCommand No_Command { get; private set; }
        public SyncCommand Copy_Command { get; private set; }


        private bool? dialogResult = null;
        public bool? DialogResult
        {
            get { return dialogResult; }
            set { dialogResult = value; Notify(); }
        }

        private string yesLabel = "";
        public string YesLabel
        {
            get { return yesLabel; }
            set { yesLabel = value; Notify(); }
        }

        private string noLabel = "";
        public string NoLabel
        {
            get { return noLabel; }
            set { noLabel = value; Notify(); }
        }

        private string cancelLabel = "";
        public string CancelLabel
        {
            get { return cancelLabel; }
            set { cancelLabel = value; Notify(); }
        }

        private string prompt = "";
        public string Prompt
        {
            get { return prompt; }
            set { prompt = value; Notify(); }
        }

        private string messageBoxIcon = "mb_iconasterisk.png";
        public string MessageBoxIcon
        {
            get { return messageBoxIcon; }
            set { messageBoxIcon = value; Notify(); }
        }

        private bool isNoVisible;
        public bool IsNoVisible
        {
            get { return isNoVisible; }
            set { isNoVisible = value; Notify(); }
        }

        private bool isCancelVisible;
        public bool IsCancelVisible
        {
            get { return isCancelVisible; }
            set { isCancelVisible = value; Notify(); }
        }

        public IClipboard Clipboard { get; set; }

        public MsgBoxVM()
        {
            No_Command = new SyncCommand(No);
            Yes_Command = new SyncCommand(Yes);
            Copy_Command = new SyncCommand(Copy);
        }

        private void Yes()
        {
            DialogResult = true;
        }

        private void No()
        {
            DialogResult = false;
        }

        private void Copy()
        {
            Clipboard?.SetText(Prompt);
        }

        public MessageBoxResult Show(string text)
        {
            Prompt = text;
            YesLabel = "OK";
            IsNoVisible = false;
            IsCancelVisible = false;
            if (StartupVM.UIDispatcher.ContainsKey(nameof(MsgBoxVM)) && testAutomationEnvironment == TestAutomationEnvironment.None)
                StartupVM.UIDispatcher[nameof(MsgBoxVM)].ShowModalUI(typeof(MsgBoxVM).Namespace + "." + nameof(MsgBoxVM) + ", " + Assembly.GetExecutingAssembly().GetName().Name, this);
            return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.None;
        }

        public MessageBoxResult Show(string text, string caption)
        {
            Prompt = text;
            YesLabel = "OK";
            IsNoVisible = false;
            IsCancelVisible = false;
            WindowTitle = caption;
            if (StartupVM.UIDispatcher.ContainsKey(nameof(MsgBoxVM)) && testAutomationEnvironment == TestAutomationEnvironment.None)
                StartupVM.UIDispatcher[nameof(MsgBoxVM)].ShowModalUI(typeof(MsgBoxVM).Namespace + "." + nameof(MsgBoxVM) + ", " + Assembly.GetExecutingAssembly().GetName().Name, this);
            return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.None;
        }

        public MessageBoxResult Show(string text, string caption, MessageBoxButton messageType)
        {
            switch (messageType)
            {
                case MessageBoxButton.OK:
                    YesLabel = "OK";
                    IsNoVisible = false;
                    IsCancelVisible = false;
                    break;
                case MessageBoxButton.YesNo:
                    YesLabel = "Da";
                    NoLabel = "Nu";
                    IsNoVisible = true;
                    IsCancelVisible = false;
                    break;
                case MessageBoxButton.YesNoCancel:
                    YesLabel = "Da";
                    NoLabel = "Nu";
                    NoLabel = "Anuleaza";
                    IsNoVisible = true;
                    IsCancelVisible = true;
                    break;
                case MessageBoxButton.OKCancel:
                    YesLabel = "Da";
                    NoLabel = "Anuleaza";
                    IsNoVisible = true;
                    IsCancelVisible = false;
                    break;
                default:
                    break;
            }

            Prompt = text;
            WindowTitle = caption;

            if (StartupVM.UIDispatcher.ContainsKey(nameof(MsgBoxVM)) && testAutomationEnvironment == TestAutomationEnvironment.None)
                StartupVM.UIDispatcher[nameof(MsgBoxVM)].ShowModalUI(typeof(MsgBoxVM).Namespace + "." + nameof(MsgBoxVM) + ", " + Assembly.GetExecutingAssembly().GetName().Name, this);
            switch (messageType)
            {
                case MessageBoxButton.OK:
                    return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.None;
                case MessageBoxButton.OKCancel:
                    return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.Cancel;
                case MessageBoxButton.YesNoCancel:
                    return DialogResult == true ? MessageBoxResult.Yes : (DialogResult == false ? MessageBoxResult.No : MessageBoxResult.Cancel);
                case MessageBoxButton.YesNo:
                    return DialogResult == true ? MessageBoxResult.Yes : MessageBoxResult.No;
                default:
                    return MessageBoxResult.None;
            }
        }

        public MessageBoxResult Show(string text, string caption, MessageBoxButton messageType, MessageBoxImage messageBoxImage)
        {
            switch (messageType)
            {
                case MessageBoxButton.OK:
                    YesLabel = "OK";
                    IsNoVisible = false;
                    IsCancelVisible = false;
                    break;
                case MessageBoxButton.YesNo:
                    YesLabel = "Da";
                    NoLabel = "Nu";
                    IsNoVisible = true;
                    IsCancelVisible = false;
                    break;
                case MessageBoxButton.YesNoCancel:
                    YesLabel = "Da";
                    NoLabel = "Nu";
                    NoLabel = "Anuleaza";
                    IsNoVisible = true;
                    IsCancelVisible = true;
                    break;
                case MessageBoxButton.OKCancel:
                    YesLabel = "Da";
                    NoLabel = "Anuleaza";
                    IsNoVisible = true;
                    IsCancelVisible = false;
                    break;
                default:
                    break;
            }
            switch (messageBoxImage)
            {
                case MessageBoxImage.Asterisk:
                    MessageBoxIcon = "mb_iconasterisk.png";
                    break;
                case MessageBoxImage.Exclamation:
                    MessageBoxIcon = "mb_iconexclamation.png";
                    break;
                case MessageBoxImage.Hand:
                    MessageBoxIcon = "mb_iconhand.png";
                    break;
                case MessageBoxImage.Question:
                    MessageBoxIcon = "mb_iconquestion.png";
                    break;
            }

            Prompt = text;
            WindowTitle = caption;

            if (StartupVM.UIDispatcher.ContainsKey(nameof(MsgBoxVM)) && testAutomationEnvironment == TestAutomationEnvironment.None)
                StartupVM.UIDispatcher[nameof(MsgBoxVM)].ShowModalUI(typeof(MsgBoxVM).Namespace + "." + nameof(MsgBoxVM) + ", " + Assembly.GetExecutingAssembly().GetName().Name, this);
            switch (messageType)
            {
                case MessageBoxButton.OK:
                    return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.None;
                case MessageBoxButton.OKCancel:
                    return DialogResult == true ? MessageBoxResult.OK : MessageBoxResult.Cancel;
                case MessageBoxButton.YesNoCancel:
                    return DialogResult == true ? MessageBoxResult.Yes : (DialogResult == false ? MessageBoxResult.No : MessageBoxResult.Cancel);
                case MessageBoxButton.YesNo:
                    return DialogResult == true ? MessageBoxResult.Yes : MessageBoxResult.No;
                default:
                    return MessageBoxResult.None;
            }
        }
    }
}

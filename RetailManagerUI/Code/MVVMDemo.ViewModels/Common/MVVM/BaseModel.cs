using System;
using RetailManagerUI.Models.Common.MVVM;
using RetailManagerUI.ViewModels.Common.Enums;
using RetailManagerUI.ViewModels.Common.Interfaces;
using System.Timers;


namespace RetailManagerUI.ViewModels.Common.MVVM
{
    public class BaseModel : LightBaseModel
    {
        public event EventHandler ClosingView;
        public static TestAutomationEnvironment testAutomationEnvironment = TestAutomationEnvironment.None;
        public static IMessageBoxService MsgBox;
        private static readonly Timer productionMediumIndicatorTimer = new Timer();
        public event Action<bool> ProductionMediumChanged;
        
        public static bool isProductionMedium;

        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }


        private string windowTitle;
        public string WindowTitle
        {
            get { return windowTitle; }
            set { windowTitle = value; Notify(); }
        }

        private bool isProgressBarVisible;
        public bool IsProgressBarVisible
        {
            get { return isProgressBarVisible; }
            set { isProgressBarVisible = value; Notify(); }
        }

        private bool isProductionMediumVisible;
        public bool IsProductionMediumVisible
        {
            get { return isProductionMediumVisible; }
            set { isProductionMediumVisible = value; Notify(); }
        }

        private bool isHelpButtonVisible;
        public bool IsHelpButtonVisible
        {
            get { return isHelpButtonVisible; }
            set { isHelpButtonVisible = value; Notify(); }
        }

        private bool isButtonEnabled;
        public bool IsButonEnabled
        {
            get { return isButtonEnabled; }
            set { isButtonEnabled = value; Notify(); }
        }

        private string helpText;
        public string HelpText
        {
            get { return helpText; }
            set { helpText = value; Notify(); }
        }


        private string productionMediumIcon = "green.png";
        public string ProductionMediumIcon
        {
            get { return productionMediumIcon; }
            set { productionMediumIcon = value; Notify(); }
        }

        private double titleFontSize = 14;
        public double TitleFontSize
        {
            get { return titleFontSize; }
            set { titleFontSize = value; Notify(); }
        }

        public SyncCommand ShowHelp_Command { get; private set; }
        public BaseModel()
        {
            productionMediumIndicatorTimer.Interval = 500;
            productionMediumIndicatorTimer.Elapsed += ProductionMediumIndicatorTimer_Elapsed;
            productionMediumIndicatorTimer.Start();
            ShowHelp_Command = new SyncCommand(ShowHelp);
        }

        private void ProductionMediumIndicatorTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IsProductionMediumVisible = !IsProductionMediumVisible;
            ProductionMediumIcon = isProductionMedium ? "green.png" : "red.png";
        }

        public virtual void ShowHelp()
        {
            MsgBox?.Show(HelpText, "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        protected void CloseView()
        {
            ClosingView?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Shows the progress bar that indicates busy operation
        /// </summary>
        protected void ShowProgressBar()
        {
            IsProgressBarVisible = true;
        }

        /// <summary>
        /// Hides the progress bar that indicates busy operation
        /// </summary>
        protected void HideProgressBar()
        {
            IsProgressBarVisible = false;
        }

        /// <summary>
        /// Triggers the event signaling the change of the production environment
        /// </summary>
        protected virtual void OnProductionMediumChanged()
        {
            ProductionMediumChanged?.Invoke(isProductionMedium);
        }
    }
}

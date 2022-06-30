using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using RetailManagerUI.ViewModels.Common.Enums;
using RetailManagerUI.ViewModels.Common.Interfaces;
using RetailManagerUI.ViewModels.Common.IoC;
using RetailManagerUI.ViewModels.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RetailManagerUI.ViewModels.Authentication
{
    public class LoginVM : BaseModel
    {
        private readonly IUserData userData;
        private readonly IDispatcher dispatcher;
        public event EventHandler LoginShown;

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; Notify(); Login_Command.RaiseCanExecuteChanged(); }
        }

        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; Notify(); Login_Command.RaiseCanExecuteChanged(); }
        }

        private SecureString password = new SecureString();
        public SecureString Password
        {
            get { return password; }
            set { password = value; Notify(); Login_Command.RaiseCanExecuteChanged(); }
        }

        private bool isLoginVisible = true;
        public bool IsLoginVisible
        {
            get { return isLoginVisible; }
            set { isLoginVisible = value; Notify(); }
        }

        public SyncCommand Login_Command { get; private set; }

        public LoginVM()
        {
            dispatcher = DependencyInjection.ServiceProvider.GetService<IDispatcher>(); // service locator anti-pattern
            MsgBox = DependencyInjection.ServiceProvider.GetService<IMessageBoxService>();
        }

        public LoginVM(IUserData _userData) : this()
        {
            userData = _userData;
            Login_Command = new SyncCommand(Login, EnableButton);
        }

        private void Login()
        {
            var user = GetUser(lastName, firstName);
            string pwd = SecureStringToString(password);
            string hashedPwd = user != null ? user.Password : " ";

            if (user != null)
            {
                string sql = "BEGIN TRY" +
                    " BEGIN TRANSACTION" +
                    $" INSERT INTO[dbo].[Loggs] (UserId) VALUES ({user.Id}) " +
                    $" UPDATE[dbo].[User] SET IsActive = 1 WHERE Id = {user.Id} " +
                    "COMMIT " +
                    "END TRY " +
                    "BEGIN CATCH " +
                    "ROLLBACK " +
                    "END CATCH";
                if (!PasswordHash.Verify(pwd, hashedPwd))
                {
                    MsgBox.Show("Parola este incorecta!", " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    var spResult = userData.LogInUser(sql);
                    IsLoginVisible = false;
                    if (StartupVM.UIDispatcher.ContainsKey(nameof(StartupVM)) && testAutomationEnvironment == TestAutomationEnvironment.None)
                        StartupVM.UIDispatcher[nameof(StartupVM)].ShowModalUI(typeof(StartupVM).Namespace + "." + nameof(StartupVM) + ", " + Assembly.GetExecutingAssembly().GetName().Name);

                    FirstName = "";
                    LastName = "";
                    LoginShown?.Invoke(this, EventArgs.Empty);
                    IsLoginVisible = true;
                }
            }
            else
            {
                MsgBox.Show("Nu exista niciun utilizator cu acest nume!", " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        private UserModel GetUser(string last, string first)
        {
            var username = userData.GetUsersList().Data.ToList();
            UserModel user = (from u in username
                              where u.FirstName.Equals(first) && u.LastName.Equals(last)
                              select u).FirstOrDefault();
            return user;
        }

        private bool EnableButton()
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(SecureStringToString(password));
            
        }

    }
}

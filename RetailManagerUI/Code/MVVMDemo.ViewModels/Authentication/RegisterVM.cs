using DataAccesLibrary.Internal.Interfaces;
using RetailManagerUI.ViewModels.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using RetailManagerUI.ViewModels.Common.Enums;
using System.Text.RegularExpressions;

namespace RetailManagerUI.ViewModels.Authentication
{
    public class RegisterVM: BaseModel
    {
        #region ========================================================PROPERTIES===========================================================================
        private readonly IUserData userData;

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; Notify(); Register_Command.RaiseCanExecuteChanged(); }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; Notify(); Register_Command.RaiseCanExecuteChanged(); }
        }

        private SecureString password = new SecureString();
        public SecureString Password
        {
            get { return password; }
            set { password = value; Notify(); Register_Command.RaiseCanExecuteChanged(); }
        }

        private SecureString confirmPassword = new SecureString();
        public SecureString ConfirmPassword
        {
            get { return confirmPassword; }
            set { confirmPassword = value; Notify(); Register_Command.RaiseCanExecuteChanged(); }
        }

        private string barcode;

        public string Barcode
        {
            get { return barcode; }
            set { barcode = value; Notify(); Register_Command.RaiseCanExecuteChanged(); }
        }



        public SyncCommand Register_Command { get; private set; }
        #endregion

        public RegisterVM(IUserData _userData)
        {
            userData = _userData;
            Register_Command = new SyncCommand(Register, ValidateRegisterForm);
        }
        public RegisterVM()
        {

        }

        #region =======================================================METHODS===================================================================================
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

        private void Register()
        {
            var validatePwd = ValidateConfirmPassword(password, confirmPassword);
            var validateUserName = ValidateUserName(firstName, lastName);

            if (!validatePwd)
            {
                MsgBox.Show("Parolele trebuie sa fie identice!", " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if (validateUserName)
            {
                MsgBox.Show("Un alt utilizator este inregistrat cu acelasi nume!", " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                InsertNewUser();
            }
        }

        private bool ValidateRegisterForm()
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(barcode) && !string.IsNullOrEmpty(SecureStringToString(password)) && !string.IsNullOrEmpty(SecureStringToString(confirmPassword));
            
        }

        private bool ValidatePassword(SecureString pwd)
        {
            string input = SecureStringToString(pwd);
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            return hasNumber.IsMatch(input) && hasUpperChar.IsMatch(input) && hasMinimum8Chars.IsMatch(input);
        }

        private bool ValidateConfirmPassword(SecureString pwd1, SecureString pwd2)
        {
            string pwd = SecureStringToString(pwd1);
            string confirmPwd = SecureStringToString(pwd2);
            return pwd == confirmPwd;
        }

        private bool ValidateUserName(string first, string last)
        {
            var username = userData.GetUserByName(first, last).Data.ToList();
            return username.Count > 0;
        }

        private void InsertNewUser()
        {
            string query = $"INSERT INTO [dbo].[User] (FirstName, LastName, Password, Barcode) VALUES ('{firstName}', '{lastName}', '{PasswordHash.Hash(SecureStringToString(password))}', '{barcode}')";
            var response = userData.AddNewUser(query);

            if (response.Count > 0 && response.Error == null)
            {
                MsgBox.Show("Un nou utilizator a fost adaugat in baza de date!", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                StartupVM.UIDispatcher[nameof(RegisterVM)].CloseUI();
            }
            else
            {
                MsgBox.Show("Utilizatorul nu a fost adaugat in baza de date din cauza unei erori. Va rog sa incercati din nou!", " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}

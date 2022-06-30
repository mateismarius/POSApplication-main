using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using RetailManagerUI.ViewModels.Common.Enums;
using RetailManagerUI.ViewModels.Common.MVVM;
using System;

namespace RetailManagerUI.ViewModels
{
    public class AddProductVM: BaseModel
    {
        #region======================================================================PROPERTIES==================================================================================================
        private readonly IStartUpData productData;

        private string brand;
        
        public string Brand
        {
            get { return brand; }
            set { 
                brand = value; 
                SubmitForm_Command.RaiseCanExecuteChanged();
                Notify();
                }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { 
                name = value;
                SubmitForm_Command.RaiseCanExecuteChanged();
                Notify();
            }
        }

        private string barcode;

        public string Barcode
        {
            get { return barcode; }
            set { 
                barcode = value;
                SubmitForm_Command.RaiseCanExecuteChanged();
                Notify();
            }
        }

        private string unit;

        public string Unit
        {
            get { return unit; }
            set { 
                unit = value;
                SubmitForm_Command.RaiseCanExecuteChanged();
                Notify();
            }
        }

        private decimal retailPrice;

        public decimal RetailPrice
        {
            get { return retailPrice; }
            set { retailPrice = value; Notify(); }
        }

        private decimal tax;

        public decimal Tax
        {
            get { return tax; }
            set { tax = value / 100; Notify(); }
        }

        private string category;

        public string Category
        {
            get { return category; }
            set { 
                category = value;
                SubmitForm_Command.RaiseCanExecuteChanged();
                Notify();
            }
        }

        private string tag1;

        public string Tag1
        {
            get { return tag1; }
            set { 
                tag1 = value;
                SubmitForm_Command.RaiseCanExecuteChanged();
                Notify();
            }
        }

        private string tag2;

        public string Tag2
        {
            get { return tag2; }
            set { 
                tag2 = value;
                SubmitForm_Command.RaiseCanExecuteChanged();
                Notify();
            }
        }

        private DateTime expiryDate;

        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set { expiryDate = value; }
        }

        public SyncCommand SubmitForm_Command { get; private set; }
        #endregion

        #region=====================================================================CONSTRUCTORS=================================================================================================
        public AddProductVM()
        {

        }

        public AddProductVM(IStartUpData _productData)
        {
            productData = _productData;
            SubmitForm_Command = new SyncCommand(SubmitForm, ValidateAddProduct);
        }
        #endregion

        #region========================================================================METHODS===================================================================================================
        private void SubmitForm()
        {
            var result = MsgBox.Show("Verificati daca datele sunt corecte si confirmati", "Confirmare", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                ProductModel product = new ProductModel
                {
                    Name = name,
                    Barcode = barcode,
                    Unit = unit,
                    RetailPrice = retailPrice,
                    Tax = tax,
                    Category = category,
                    Tag1 = tag1,
                    Tag2 = tag2,
                    ExpiryDate = expiryDate
                };
                var response = productData.AddProduct(product);
                if (response > 0)
                {
                    MsgBox.Show("Produsul a fost adaugat in baza de date!", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MsgBox.Show("Produsul nu a fost adaugat din cauza unei erori. Va rog sa incercati!", " ", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                StartupVM.UIDispatcher[nameof(AddProductVM)].CloseUI();
            }
        }

        private bool ValidateAddProduct()
        {
            return !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(unit) && !string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(tag1);
        }
        #endregion
    }
}

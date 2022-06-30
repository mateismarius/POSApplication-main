using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using RetailManagerUI.Models.Common;
using RetailManagerUI.Models.Models;
using RetailManagerUI.ViewModels.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetailManagerUI.ViewModels
{
    public class AddInvoiceDetailsVM: BaseModel
    {
        private readonly IStartUpData productData;

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; Notify(); AddDetails_Command.RaiseCanExecuteChanged(); OpenAddProductWindow_Command.RaiseCanExecuteChanged(); }
        }

        private float quantity;

        public float Quantity
        {
            get { return quantity; }
            set { quantity = value; Notify(); AddDetails_Command.RaiseCanExecuteChanged(); }
        }

        private decimal purchasePrice;

        public decimal PurchasePrice
        {
            get { return purchasePrice; }
            set { purchasePrice = value; ProductPurchasePrice = purchasePrice + productTax; Notify(); AddDetails_Command.RaiseCanExecuteChanged(); }
        }

        private decimal productTax;

        public decimal ProductTax
        {
            get { return productTax; }
            set { productTax = value; ProductPurchasePrice = purchasePrice + productTax; Notify(); }
        }

        private decimal productPurchasePrice;

        public decimal ProductPurchasePrice
        {
            get { return productPurchasePrice; }
            set { productPurchasePrice = value; Notify(); }
        }


        public AddInvoiceDetailsVM()
        {

        }

        public AddInvoiceDetailsVM(IStartUpData _productData)
        {
            productData = _productData;
            Products_EnterKeyUpAsync_Command = new AsyncCommand(Products_EnterKeyUpAsync);
            Products_DropDownClosingAsync_Command = new AsyncCommand(Products_DropDownClosingAsync);
            OpenAddProductWindow_Command = new SyncCommand(OpenAddProductWindow, ValidateAddProductButton);
            AddDetails_Command = new SyncCommand(AddDetails, ValidateAddButton);
            GetProducts();
        }

        // Add invoice details to observable collection

        #region
        
        public AsyncCommand Products_EnterKeyUpAsync_Command { get; private set; }
        public AsyncCommand Products_DropDownClosingAsync_Command { get; private set; }
        public SyncCommand OpenAddProductWindow_Command { get; private set; }


        private string productsSearchText = "";
        public string ProductsSearchText
        {
            get { return productsSearchText; }
            set { productsSearchText = value; Notify(); }
        }

        
        private ObservableCollection<ComboboxItemDataM> sourceSearchProducts = new ObservableCollection<ComboboxItemDataM>();
        public ObservableCollection<ComboboxItemDataM> SourceSearchProducts
        {
            get { return sourceSearchProducts; }
            set { sourceSearchProducts = value; Notify(); }
        }

        private ComboboxItemDataM productsSearchSelectedItem = new ComboboxItemDataM();
        public ComboboxItemDataM ProductsSearchSelectedItem
        {
            get { return productsSearchSelectedItem; }
            set { productsSearchSelectedItem = value; Notify(); }
        }



        private void GetProducts()
        {
            List<ComboboxItemDataM> _temp = new List<ComboboxItemDataM>();
            var product = productData.GetAllProducts();
            foreach (var item in product)
            {
                _temp.Add(new ComboboxItemDataM { Text = item.Name, Value = item.Barcode, Hover = item.Barcode });
            }
            SourceSearchProducts = new ObservableCollection<ComboboxItemDataM>(_temp);
        }

        private async Task Products_DropDownClosingAsync()
        {
            ProductModel result = await GetProductByParameter<ProductModel>(productsSearchText);
            ProductName = result.Name ;
            ProductsSearchText = "";
        }

        /// <summary>
        /// Handles Search Products Enter key up event
        /// </summary>
        private async Task Products_EnterKeyUpAsync()
        {
            ProductModel result = await GetProductByParameter<ProductModel>(productsSearchText);
            ProductName = result.Name;
            ProductsSearchText = "";
        }

        public SyncCommand AddDetails_Command { get; set; }
        
        private void AddDetails()
        {
            InvoiceDetailsModelUI detailsCollection = new InvoiceDetailsModelUI
            {
                Name = productName,
                Quantity = quantity,
                PurchasePrice = purchasePrice,
                Tax = ProductTax,
                TotalPrice = productPurchasePrice
            };
            AddInvoiceVM.InvoiceDetailsCollection.Add(detailsCollection);
            StartupVM.UIDispatcher[nameof(AddInvoiceDetailsVM)].CloseUI();
        }

        internal async Task<ProductModel> GetProductByParameter<ProductModel>(string searchTerm)
        {
            return await productData.GetProductByParameter<ProductModel>(searchTerm);
        }

        internal bool ValidateAddButton()
        {
            return !string.IsNullOrEmpty(productName) && quantity > 0 && purchasePrice > 0;
        }

        internal void OpenAddProductWindow()
        {
            StartupVM.UIDispatcher[nameof(AddProductVM)].ShowModalUI();
            GetProducts();
        }

        internal bool ValidateAddProductButton()
        {
            return string.IsNullOrEmpty(productName);
        }
        #endregion
    }
}

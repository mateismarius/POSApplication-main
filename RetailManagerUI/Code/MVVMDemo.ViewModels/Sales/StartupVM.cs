
using RetailManagerUI.ViewModels.Common.Interfaces;
using RetailManagerUI.ViewModels.Common.MVVM;
using System;
using System.Collections.Generic;
using RetailManagerUI.ViewModels.Common.Enums;
using DataAccesLibrary.Internal.Models;
using RetailManagerUI.Models.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RetailManagerUI.Models.Common;
using DataAccesLibrary.Internal.Interfaces;
using System.Linq;
using RetailManagerUI.ViewModels.Authentication;
using RetailManagerUI.ViewModels.Sales;

namespace RetailManagerUI.ViewModels
{
    public class StartupVM : BaseModel
    {
        #region=============================================================================PROPERTIES===============================================================================================
        private readonly IStartUpData startupData;
        private readonly ILoggerManager<StartupVM> logger;


        public static Dictionary<string, IUserInterface> UIDispatcher = new Dictionary<string, IUserInterface>();
        
        private string productsSearchText = "";
        public string ProductsSearchText
        {
            get { return productsSearchText; }
            set { productsSearchText = value; Notify(); }
        }

        private ObservableCollection<ProductModelUI> salesCollection;

        public ObservableCollection<ProductModelUI> SalesCollection
        {
            get { return salesCollection; }
            set { salesCollection = value; }
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

        private ObservableCollection<SaleModelUI> productCollection;

        public ObservableCollection<SaleModelUI> ProductCollection
        {
            get { return productCollection; }
            set { productCollection = value; Notify(); }
        }
        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set
            {
                amount = decimal.Round(value, 2, MidpointRounding.AwayFromZero);
                Notify();
            }
        }

        private float quantity;
        public float Quantity
        {
            get { return quantity; }
            set { quantity = value; Notify(); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; Notify(); }
        }

        private string rol;

        public string Rol
        {
            get { return rol; }
            set { rol = value; Notify(); }
        }

        private SaleModelUI selectedSaleItem;

        public SaleModelUI SelectedSaleItem
        {
            get { return selectedSaleItem; }
            set { selectedSaleItem = value; DeleteItem_Command.RaiseCanExecuteChanged(); Notify(); }
        }

        public SyncCommand OpenBarchart_Command { get; private set; }
        public SyncCommand OpenInvoiceWindow_Command { get; private set; }
        public SyncCommand OpenSaleWindow_Command { get; private set; }
        public SyncCommand OpenProductWindow_Command { get; private set; }
        public SyncCommand Window_ContentRendered_Command { get; private set; }
        public SyncCommand GetProducts_Command { get; private set; }
        public SyncCommand AddSale_Command { get; private set; }
        public SyncCommand DeleteItem_Command { get; private set; }
        public SyncCommand OpenRegisterWindow_Command { get; private set; }
        public IAsyncCommand<string> Products_EnterKeyUpAsync_Command { get; private set; }
        public IAsyncCommand Products_DropDownClosingAsync_Command { get; private set; }

        


        #endregion


        #region============================================================================CONSTRUCTORS==============================================================================================
        public StartupVM()
        {

        }

        public StartupVM(IStartUpData _startupData)
        {
            ShowProgressBar();
            startupData = _startupData;
            OpenInvoiceWindow_Command = new SyncCommand(OpenInvoiceWindow);
            OpenProductWindow_Command = new SyncCommand(OpenProductWindow);
            OpenSaleWindow_Command = new SyncCommand(OpenSaleWindow);
            Window_ContentRendered_Command = new SyncCommand(Window_ContentRendered);
            GetProducts_Command = new SyncCommand(GetProducts);
            AddSale_Command = new SyncCommand(AddSale);
            OpenRegisterWindow_Command = new SyncCommand(OpenRegisterWindow);
            DeleteItem_Command = new SyncCommand(DeleteItem, ValidateDeleteButton);
            Products_EnterKeyUpAsync_Command = new AsyncCommand<string>(Products_EnterKeyUpAsync);
            Products_DropDownClosingAsync_Command = new AsyncCommand(Products_DropDownClosingAsync);
            ProductCollection = new ObservableCollection<SaleModelUI>();
            SetUserDataForUI();
            Quantity = 1;
            HideProgressBar();
        }

        #endregion


        #region===============================================================================METHODS======================================================================================================
        /// <summary>
        /// Handles Products DropDownClosing event
        /// </summary>
        internal async Task Products_DropDownClosingAsync()
        {
            var product = await GetProductsAsync(productsSearchText);
            ProductCollection.Add(product);
            Quantity = 1;
            ProductsSearchText = "";
        }

        /// <summary>
        /// Handles Search Products Enter key up event
        /// </summary>
        internal async Task Products_EnterKeyUpAsync(string _searchTerm)
        {
            var product = await GetProductsAsync(productsSearchText);
            ProductCollection.Add(product);
            Quantity = 1;
            ProductsSearchText = "";
        }

        /// <summary>
        /// convert the object from ProductModel to SaleModelUI 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns>SaleModelUI object</returns>
        internal async Task<SaleModelUI> GetProductsAsync(string searchTerm)
        {
            var product = new SaleModelUI();
            ProductModel result = await GetProductByParameter(searchTerm);

            product.Crt = productCollection.Count + 1;
            product.ProductName = result.Name;
            product.Quantity = quantity;
            product.Unit = result.Unit;
            product.Subtotal = result.RetailPrice;
            product.Tax = Convert.ToDecimal(result.Tax) * result.RetailPrice;
            product.Total = product.Subtotal + product.Tax;
            product.SaleUnitValue = Convert.ToDecimal(product.Quantity) * product.Total;
            Amount += product.SaleUnitValue;

            return product;

        }

        /// <summary>
        /// Retrieve from database the searched product 
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns>Product founded</returns>
        internal async Task<ProductModel> GetProductByParameter(string searchTerm)
        {
            return await startupData.GetProductByParameter<ProductModel>(searchTerm);
        }

        /// <summary>
        /// Fill the collection bounded to autocomplete box with all products 
        /// </summary>
        internal void GetProducts()
        {
            List<ComboboxItemDataM> _temp = new List<ComboboxItemDataM>();
            var product = startupData.GetAllProducts();
            foreach (var item in product)
            {
                _temp.Add(new ComboboxItemDataM { Text = item.Name, Value = item.Barcode, Hover = item.Barcode});
            }
            SourceSearchProducts = new ObservableCollection<ComboboxItemDataM>(_temp);
        }
       
        /// <summary>
        /// Open Invoice Window
        /// </summary>
        internal void OpenInvoiceWindow()
        {
            if (UIDispatcher.ContainsKey(nameof(InvoiceVM)))
                UIDispatcher[nameof(InvoiceVM)].ShowModalUI();
            else
                MsgBox.Show("That window has not been registred!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            GetProducts();
        }

        /// <summary>
        /// Open Product Window
        /// </summary>
        internal void OpenProductWindow()
        {
            if (UIDispatcher.ContainsKey(nameof(ProductVM)))
                UIDispatcher[nameof(ProductVM)].ShowModalUI();
            else
                MsgBox.Show("That window has not been registred!", "", MessageBoxButton.OK, MessageBoxImage.Error);
            GetProducts();
        }

        /// <summary>
        /// Open User Window
        /// </summary>
        internal void OpenRegisterWindow()
        {
            if (UIDispatcher.ContainsKey(nameof(RegisterVM)))
                UIDispatcher[nameof(RegisterVM)].ShowModalUI();
            else
                MsgBox.Show("That window has not been registred!", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Get logged user details
        /// </summary>
        /// <returns>Logged user object</returns>
        internal UserModel GetUserDetails()
        {
            return startupData.GetActiveUser();
        }

        /// <summary>
        /// Check the user's role
        /// </summary>
        internal void SetUserDataForUI()
        {
            UserModel user = GetUserDetails();
            if (user != null)
            {
                UserName = user.FirstName.ToUpper();
                switch(user.IsAdmin)
                {
                    case 1:
                        Rol = "ADMINISTRATOR";
                        break;
                    case 0:
                        Rol = "VANZATOR";
                        break;
                }    
            }
        }

        /// <summary>
        /// Open Sale Window
        /// </summary>
        internal void OpenSaleWindow()
        {
            if (UIDispatcher.ContainsKey(nameof(SalesVM)))
                UIDispatcher[nameof(SalesVM)].ShowModalUI();
            else
                MsgBox.Show("That window has not been registred!", "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Actions to performe when window is rendered
        /// </summary>
        internal void Window_ContentRendered()
        {
          
            TitleFontSize = 18;
            GetProducts();
        }

        /// <summary>
        /// Record new sale in database
        /// </summary>
        internal void AddSale()
        {
            ShowProgressBar();
            if (productCollection.Count > 0 && productCollection != null)
            {
                var payConfirmation = MsgBox.Show("Doriti sa incheiati tranzactia si sa platiti?", "Incheiere tranzactie", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (payConfirmation == MessageBoxResult.Yes)
                {
                    var result = MsgBox.Show("Doriti sa imprimati bonul?", "Imprimare bon", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (result == MessageBoxResult.Yes)
                    {
                        string sql = "BEGIN TRY" +
                            " BEGIN TRANSACTION" +
                            " INSERT INTO dbo.Sale (CashierId, Total)" +
                                     $" VALUES ((SELECT Id from [dbo].[User] WHERE IsActive=1), {amount})";

                        foreach (var item in ProductCollection)
                        {
                            sql += " INSERT INTO dbo.SaleDetail (SaleId, ProductId, Quantity, RetailPrice, Tax, Total)" +
                                   $" VALUES ((SELECT TOP 1 Id FROM dbo.Sale ORDER BY Id DESC ), (SELECT Id from dbo.Product WHERE Name='{item.ProductName}'), {quantity}, {item.Subtotal}, {item.Tax}, {item.SaleUnitValue})";
                            sql += $" UPDATE dbo.Stock SET StockOut = Stockout + {item.Quantity} WHERE ProductId = (SELECT Id from dbo.Product WHERE Name='{item.ProductName}')";

                        }
                        sql += "COMMIT" +
                            " END TRY" +
                            " BEGIN CATCH" +
                            " ROLLBACK " +
                            " END CATCH";

                        ServerResponseModel<SalesModel> response = startupData.AddSale(sql);

                        if (response.Error == null && response.Count > 0)
                        {
                            MsgBox.Show("Baza de date a fost acualizata cu succes!", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MsgBox.Show("Cererea nu a fost procesata din cauza unei erori, iar produsul nu a fost adaugat", "Eroare de procesare", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        ProductCollection = new ObservableCollection<SaleModelUI>();
                        Amount = 0;
                    }
                    else
                    {
                        ProductCollection = new ObservableCollection<SaleModelUI>();
                        Amount = 0;
                    }
                }
            }
            else
            {
                MsgBox.Show("Cosul de cumparaturi este gol. Va rog sa scanati cel putin un produs!", "", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            HideProgressBar();
        }

        /// <summary>
        /// Delete item from collection
        /// </summary>
        internal void DeleteItem()
        {
            Amount -= selectedSaleItem.SaleUnitValue;
            ProductCollection.Remove(ProductCollection.Where(i => i.Crt == SelectedSaleItem.Crt).Single());
        }
        /// <summary>
        /// Enable delete button when true
        /// </summary>
        /// <returns>true or false</returns>
        internal bool ValidateDeleteButton()
        {
            return SelectedSaleItem != null;
        }

        #endregion
    }

}










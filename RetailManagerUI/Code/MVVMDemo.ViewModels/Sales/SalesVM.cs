using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using RetailManagerUI.ViewModels.Common.MVVM;
using RetailManagerUI.ViewModels.Common.Enums;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Data;

namespace RetailManagerUI.ViewModels.Sales
{
    public class SalesVM : BaseModel
    {
        #region==============================================================================PROPERTIES========================================================================================
        private readonly ISalesData salesData;

        private SalesReportModel selectedSale;

        private ICollectionView salesCollectionView;

        public ICollectionView SalesCollectionView
        {
            get { return salesCollectionView; }
            set { salesCollectionView = value; Notify(); }
        }

        private string productsSearchText = "";
        public string ProductsSearchText
        {
            get { return productsSearchText; }
            set { productsSearchText = value; Notify(); SalesCollectionView.Filter += FilterSales; SalesCollectionView.Refresh(); }
        }

        private ObservableCollection<SalesReportModel> salesCollection;

        public ObservableCollection<SalesReportModel> SalesCollection
        {
            get { return salesCollection; }
            set { salesCollection = value; Notify(); }
        }


        public SalesReportModel SelectedSale
        {
            get { return selectedSale; }
            set { selectedSale = value; Notify(); DeleteSale_Command.RaiseCanExecuteChanged(); }
        }
        public SyncCommand Window_ContentRendered_Command { get; private set; }
        public SyncCommand DeleteSale_Command { get; private set; }
        public SyncCommand<SalesReportModel> OpenSale_Command { get; private set; }
       
        #endregion


        #region=============================================================================CONSTRUCTORS======================================================================================
        public SalesVM(ISalesData _salesData)
        {
            ShowProgressBar();
            salesData = _salesData;
            Window_ContentRendered_Command = new SyncCommand(Window_ContentRendered);
            DeleteSale_Command = new SyncCommand(DeleteSale, EnableDeleteButton);
            OpenSale_Command = new SyncCommand<SalesReportModel>(OpenSale);
            SalesCollection = new ObservableCollection<SalesReportModel>();
        }

        public SalesVM()
        {

        }
        #endregion


        #region===============================================================================METHODS===================================================================================================
        /// <summary>
        /// Fill the collection bounded to Sales's listview when window rendered
        /// </summary>
        private void Window_ContentRendered()
        {
           GetSales();
        }

        private bool FilterSales(object obj)
        {
            if (obj is SalesReportModel salesReportModel)
            {
                return salesReportModel.Total.ToString().Contains(productsSearchText.ToLower()) || salesReportModel.LastName.ToLower().Contains(productsSearchText.ToLower());
            }
            return false;
        }

        /// <summary>
        /// Delete entire sale from Sale's table, delete all entries with specific SalesId from SalesDetails table and update the stock
        /// </summary>
        public void DeleteSale()
        {
            string sql = "BEGIN TRANSACTION";
            var saleDetails = salesData.GetSaleDetailsById(selectedSale.Id);
            if (saleDetails != null)
            {
                foreach (var item in saleDetails)
                {
                    sql += $" UPDATE [dbo].[Stock] SET Stockout = Stockout - {item.Quantity} WHERE ProductId = (SELECT Id FROM [dbo].[Product] WHERE Name = '{item.ProductName}')";
                }
            }
            sql += $" DELETE FROM [dbo].[SaleDetail] WHERE SaleId = {selectedSale.Id}";
            sql += $" DELETE FROM [dbo].[Sale] WHERE Id = {selectedSale.Id}";
            sql += " COMMIT";
            var response = salesData.DeleteSale(sql).Count;
            if (response > 0)
            {
                MsgBox.Show($"Vanzarea cu numarul {selectedSale.Id} efectuata de {selectedSale.LastName} in data de {selectedSale.SaleDate} a fost stearsa din baza de date", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                GetSales();
            }
            else
                MsgBox.Show("Eroare de procesare! Va rog sa incercati din nou!", " ", MessageBoxButton.OK, MessageBoxImage.Error);

            

        }
        /// <summary>
        /// Enable delete button when true 
        /// </summary>
        /// <returns>true or false</returns>
        private bool EnableDeleteButton()
        {
            return selectedSale != null;
        }
        /// <summary>
        /// Open SaleDetails Window and send selected object's id as a parameter
        /// </summary>
        /// <param name="_sale">selected object from a list</param>
        public void OpenSale(SalesReportModel _sale)
        {
            if (StartupVM.UIDispatcher.ContainsKey(nameof(SaleDetailsVM)))
                StartupVM.UIDispatcher[nameof(SaleDetailsVM)].ShowModalUI(_sale.Id.ToString(), typeof(SaleDetailsVM).Namespace + "." + nameof(SaleDetailsVM) + ", " + Assembly.GetExecutingAssembly().GetName().Name);
            GetSales();
        }

        private void GetSales()
        {
            ObservableCollection<SalesReportModel> temp = new ObservableCollection<SalesReportModel>();
            var salescollection = salesData.GetAllSales().Data;
            foreach (var sale in salescollection)
            {
                temp.Add(sale);
            }
            SalesCollection = temp;
            SalesCollectionView = CollectionViewSource.GetDefaultView(SalesCollection);
        }
        #endregion
    }


}

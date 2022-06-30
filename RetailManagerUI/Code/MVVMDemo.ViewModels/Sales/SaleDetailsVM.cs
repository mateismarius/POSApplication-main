using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using RetailManagerUI.ViewModels.Common.MVVM;
using RetailManagerUI.ViewModels.Common.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagerUI.ViewModels
{
    public class SaleDetailsVM: BaseModel
    {
        #region===========================================================================PROPERTIES==============================================================================================
        private readonly ISalesData salesData;

        private string label;

        public string Label
        {
            get { return label; }
            set { label = value; Notify(); }
        }

        private SaleDetailsModel selectedSale;

        public SaleDetailsModel SelectedSale
        {
            get { return selectedSale; }
            set { selectedSale = value; Notify(); DeleteProduct_Command.RaiseCanExecuteChanged(); }
        }
        private ObservableCollection<SaleDetailsModel> saleDetailsCollection;

        public ObservableCollection<SaleDetailsModel> SaleDetailsCollection
        {
            get { return saleDetailsCollection; }
            set { saleDetailsCollection = value; Notify(); }
        }

        public SyncCommand Window_ContentRendered_Command { get; private set; }
        public SyncCommand DeleteProduct_Command { get; private set; }

        #endregion


        #region==========================================================================CONSTRUCTORS===============================================================================================
        public SaleDetailsVM(ISalesData _salesData)
        {
            salesData = _salesData;
            SaleDetailsCollection = new ObservableCollection<SaleDetailsModel>();
            Window_ContentRendered_Command = new SyncCommand(Window_ContentRendered);
            DeleteProduct_Command = new SyncCommand(DeleteProduct, EnableDeleteButton);
        }

        public SaleDetailsVM()
        {

        }

        #endregion


        #region=============================================================================METHODS==========================================================================================
        /// <summary>
        /// Fill the collection bounded to SalesDetails's listview
        /// </summary>
        public void Window_ContentRendered()
        {
            GetSaleDetails();
        }

        private void GetSaleDetails()
        {
            if (!string.IsNullOrEmpty(this.Id))
            {
                var sale = GetSale(Convert.ToInt32(this.Id)).Data.First();
                ObservableCollection<SaleDetailsModel> temp = new ObservableCollection<SaleDetailsModel>();
                if (sale != null)
                {
                    Label = $"Lista produselor vandute de catre {sale.LastName} cu numarul de inregistrare {sale.Id} in data de {sale.SaleDate}";
                }
                var saleDetails = salesData.GetSaleDetailsById(Convert.ToInt32(this.Id));
                foreach (var item in saleDetails)
                {
                    temp.Add(item);
                }
                SaleDetailsCollection = temp;
            }
        }
        
        /// <summary>
        /// Retrieve the sale from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ServerResponseModel<SalesReportModel> GetSale(int id)
        {
            string sql = $"SELECT Id, LastName = (SELECT LastName FROM [dbo].[User] WHERE Id = [dbo].[Sale].CashierId), SaleDate, Total FROM [dbo].[Sale] WHERE Id={id}";
            return salesData.GetSaleById(sql);
        }

        /// <summary>
        /// Enable Delete button when true
        /// </summary>
        /// <returns>true or false</returns>
        private bool EnableDeleteButton()
        {
            return SelectedSale != null;
        }

        /// <summary>
        /// Delete product from SaleDetails's table and update stock table
        /// </summary>
        private void DeleteProduct()
        {
            string sql = $"BEGIN TRANSACTION " +
                $"UPDATE [dbo].[Stock] SET Stockout = Stockout - {selectedSale.Quantity} WHERE ProductId = (SELECT Id FROM [dbo].[Product] WHERE Name = '{selectedSale.ProductName}') " +
                $"UPDATE [dbo].[Sale] SET Total = Total - ({selectedSale.Quantity} * {selectedSale.Total}) WHERE Id = (SELECT SaleId FROM [dbo].[SaleDetail] WHERE Id = {selectedSale.Id})" +
                $"DELETE FROM [dbo].[SaleDetail] WHERE ProductId = (SELECT Id FROM [dbo].[Product] WHERE Name = '{selectedSale.ProductName}') AND SaleId = {this.Id}" +
                " COMMIT";
            var response = salesData.DeleteSale(sql).Count;
            if (response > 0)
            {
                GetSaleDetails();
                MsgBox.Show("Baza de date a fost actualizata cu succes!", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }
                
            else
                MsgBox.Show("Eroare de procesare. Va rog sa incercati din nou!", " ", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion
    }
}

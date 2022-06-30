using AutoMapper;
using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using RetailManagerUI.Models.Models;
using RetailManagerUI.ViewModels.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RetailManagerUI.ViewModels.Common.Enums;
using System.Text;
using System.Threading.Tasks;
using RetailManagerUI.ViewModels.Common.IoC;
using RetailManagerUI.ViewModels.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace RetailManagerUI.ViewModels
{
    public class InvoiceDetailsVM: BaseModel
    {
        #region=============================================================================PROPERTIES====================================================================================================
        private readonly IInvoiceData invoiceData;
        private readonly ILoggerManager<InvoiceDetailsVM> logger = DependencyInjection.ServiceProvider.GetService<ILoggerManager<InvoiceDetailsVM>>();
       
        private string label;

        public string Label
        {
            get { return label; }
            set { label = value; Notify(); }
        }

        private InvoiceDetailsModelUI selectedProduct;

        public InvoiceDetailsModelUI SelectedProduct
        {
            get { return selectedProduct; }
            set { selectedProduct = value; Notify(); DeleteProduct_Command.RaiseCanExecuteChanged(); }
        }

        public SyncCommand DeleteProduct_Command { get; private set; }

        private ObservableCollection<InvoiceDetailsModelUI>  invoiceDetailsCollection;

        public ObservableCollection<InvoiceDetailsModelUI> InvoiceDetailsCollection 
        {
            get { return invoiceDetailsCollection; }
            set { invoiceDetailsCollection = value; }
        }

        public SyncCommand Window_ContentRendered_Command { get; private set; }
        #endregion

        #region============================================================================CONSTRUCTORS=======================================================================================================
        public InvoiceDetailsVM()
        {

        }

        public InvoiceDetailsVM(IInvoiceData _invoiceData)
        {
            InvoiceDetailsCollection = new ObservableCollection<InvoiceDetailsModelUI>();
            invoiceData = _invoiceData;
            Window_ContentRendered_Command = new SyncCommand(Window_ContentRendered);
            DeleteProduct_Command = new SyncCommand(DeleteProduct, EnableDeleteButton);
            logger.LogWarn("test");
        }
        #endregion

        #region===============================================================================METHODS============================================================================================================
        public void Window_ContentRendered()
        {
            if (!string.IsNullOrEmpty(this.Id))
            {
                var invoice = GetInvoice(Convert.ToInt32(this.Id)).Data.First();
                if (invoice != null)
                {
                    Label = $"Produsele achizitionate de la {invoice.Supplier.ToUpper()} cu factura numarul {invoice.Invoice.ToUpper()}";
                }
                var invoiceDetails = invoiceData.GetInvoiceDetailsById(Convert.ToInt32(this.Id));
                var invoiceDetailsUI = ConfigureAutoMapper().Map<IEnumerable<InvoiceDetailsModelUI>>(invoiceDetails);
                foreach (var item in invoiceDetailsUI)
                {
                    InvoiceDetailsCollection.Add(item);
                }
            }
        }

        public IMapper ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GetInvoiceDetailsModel, InvoiceDetailsModelUI>();
            });

            return config.CreateMapper();
        }

        public ServerResponseModel<InvoiceModel> GetInvoice(int id)
        {
            string sql = $"SELECT * FROM [dbo].[Invoice] WHERE Id={id}";
            return invoiceData.GetInvoiceById(sql);
        }

        private bool EnableDeleteButton()
        {
            return SelectedProduct != null;
        }

        public void DeleteProduct()
        {
            string sql = $"BEGIN TRANSACTION " +
                $"UPDATE [dbo].[Stock] SET Stockin = Stockin - {selectedProduct.Quantity} WHERE ProductId = (SELECT Id FROM [dbo].[Product] WHERE Name = '{selectedProduct.Name}') " +
                $"DELETE FROM [dbo].[InvoiceDetail] WHERE ProductId = (SELECT Id FROM [dbo].[Product] WHERE Name = '{selectedProduct.Name}') AND InvoiceId = {this.Id}" + 
                "COMMIT";
            var response = invoiceData.DeleteInvoice(sql).Count;
            if (response > 0)
                MsgBox.Show("Baza de date a fost actualizat[ cu succes!", " ", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MsgBox.Show("Eroare de procesare. Va rog sa incercati din nou!", " ", MessageBoxButton.OK, MessageBoxImage.Error);

            if (StartupVM.UIDispatcher.ContainsKey(nameof(InvoiceDetailsVM)))
                StartupVM.UIDispatcher[nameof(InvoiceDetailsVM)].CloseUI();
            else
                MsgBox.Show("That window has not been registred!", "", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        #endregion
    }
}

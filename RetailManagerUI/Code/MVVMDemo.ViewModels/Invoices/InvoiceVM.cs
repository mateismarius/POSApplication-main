using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using AutoMapper;
using DataAccesLibrary.DataAccess;
using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using RetailManagerUI.Models.Models;
using RetailManagerUI.ViewModels.Common.Enums;
using RetailManagerUI.ViewModels.Common.Interfaces;
using RetailManagerUI.ViewModels.Common.MVVM;

namespace RetailManagerUI.ViewModels
{
    public class InvoiceVM: BaseModel
    {
        #region==========================================================================PROPERTIES======================================================================================================
        private readonly IInvoiceData invoiceData;
        public SyncCommand Window_ContentRendered_Command { get; private set; }

        public SyncCommand OpenAddInvoiceWindow_Command { get; private set; }
        public SyncCommand DeleteInvoice_Command { get; private set; }
        public ISyncCommand<InvoiceModelUI> OpenInvoice_Command { get; private set; }

        public static ObservableCollection<InvoiceModelUI> Invoices { get; set; }

        private InvoiceModelUI selectedInvoiceItem;

        public InvoiceModelUI SelectedInvoiceItem
        {
            get { return selectedInvoiceItem; }
            set { selectedInvoiceItem = value; Notify(); DeleteInvoice_Command.RaiseCanExecuteChanged(); }
        }

        private string productsSearchText = "";
        public string ProductsSearchText
        {
            get { return productsSearchText; }
            set { productsSearchText = value; Notify(); InvoicesCollection.Filter += FilterProducts; InvoicesCollection.Refresh(); }
        }

        private ICollectionView invoicesCollection;

        public ICollectionView InvoicesCollection
        {
            get { return invoicesCollection; }
            set { invoicesCollection = value; Notify(); }
        }
        #endregion

        #region=========================================================================CONSTRUCTORS=======================================================================================================
        public InvoiceVM()
        {

        }
        public InvoiceVM(IInvoiceData _invoiceData)
        {
            ShowProgressBar();
            invoiceData = _invoiceData;
            OpenInvoice_Command = new SyncCommand<InvoiceModelUI>(OpenInvoice);
            OpenAddInvoiceWindow_Command = new SyncCommand(OpenAddInvoiceWindow);
            DeleteInvoice_Command = new SyncCommand(DeleteInvoice, EnableDeleteButton);
            Invoices = new ObservableCollection<InvoiceModelUI>();
            Window_ContentRendered_Command = new SyncCommand(Window_ContentRendered);
        }
        #endregion

        #region============================================================================METHODS==========================================================================================================
       /// <summary>
       /// Fill the invoices's collection bounded to listview
       /// </summary>
        private void Window_ContentRendered()
        {
            var invoiceCollection = invoiceData.GetAllInvoices();
            var invoice = ConfigureAutoMapper().Map<IEnumerable<InvoiceModelUI>>(invoiceCollection.Data);
            InvoicesCollection = CollectionViewSource.GetDefaultView(Invoices);

            string message = "A aparut o eroare in baza de date!!";
            string message1 = "Nu a fost gasit niciun rezultat";

            if (invoiceCollection.Count > 0)
            {
                foreach (var item in invoice)
                {
                    Invoices.Add(item);
                }
            }
            else if (!string.IsNullOrEmpty(invoiceCollection.Error))
            {
                MsgBox.Show(message, " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MsgBox.Show(message1, " ", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            HideProgressBar();
        }

        /// <summary>
        /// Open AddInvoice Window
        /// </summary>
        private void OpenAddInvoiceWindow()
        {
            StartupVM.UIDispatcher[nameof(AddInvoiceVM)].ShowModalUI();
        }

        /// <summary>
        /// Convert InvoiceModel tio InvoiceModelUI
        /// </summary>
        /// <returns>InvoiceModelUI object</returns>
        public IMapper ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InvoiceModel, InvoiceModelUI>();
            });

            return config.CreateMapper();
        }

        /// <summary>
        /// Delete selected invoice and all entries based on it from a list, update the stock
        /// </summary>
        public void DeleteInvoice()
        {
            string sql = "BEGIN TRANSACTION";
            var invoiceDetails = invoiceData.GetInvoiceDetailsById(selectedInvoiceItem.Id);
            if (invoiceDetails != null)
            {
                foreach (var item in invoiceDetails)
                {
                    sql += $" UPDATE [dbo].[Stock] SET Stockin = Stockin - {item.Quantity} WHERE ProductId = (SELECT Id FROM [dbo].[Product] WHERE Name = '{item.Name}')";
                }
            }
            sql += $" DELETE FROM [dbo].[InvoiceDetail] WHERE InvoiceId = {selectedInvoiceItem.Id}";
            sql += $" DELETE FROM [dbo].[Invoice] WHERE Id = {selectedInvoiceItem.Id}";
            sql += "COMMIT";
            var response = invoiceData.DeleteInvoice(sql).Count;
            if (response > 0)
                MsgBox.Show($"Factura numarul {selectedInvoiceItem.Invoice} eliberata de {selectedInvoiceItem.Supplier} a fost stearsa din baza de date", " ", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MsgBox.Show("Eroare de procesare! Va rog sa incercati din nou!", " ", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Enable delete button when true
        /// </summary>
        /// <returns>true or false</returns>
        private bool EnableDeleteButton()
        {
            return selectedInvoiceItem != null;
        }

        /// <summary>
        /// Open InvoiceDetails window and sent selected invoice as a parameter
        /// </summary>
        /// <param name="_invoice">selected invoice</param>
        public void OpenInvoice(InvoiceModelUI _invoice)
        {
            ShowProgressBar();
            if (StartupVM.UIDispatcher.ContainsKey(nameof(InvoiceDetailsVM)))
                StartupVM.UIDispatcher[nameof(InvoiceDetailsVM)].ShowModalUI(_invoice.Id.ToString(), typeof(InvoiceDetailsVM).Namespace + "." + nameof(InvoiceDetailsVM) + ", " + Assembly.GetExecutingAssembly().GetName().Name);
            HideProgressBar();
        }

        private bool FilterProducts(object obj)
        {
            if (obj is InvoiceModelUI invoiceModel)
            {
                return invoiceModel.Invoice.ToLower().Contains(productsSearchText.ToLower()) || invoiceModel.Supplier.ToLower().Contains(productsSearchText.ToLower());
            }
            return false;
        }
        #endregion
    }
}

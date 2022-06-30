using AutoMapper;
using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using RetailManagerUI.Models.Models;
using RetailManagerUI.ViewModels.Common.Enums;
using RetailManagerUI.ViewModels.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RetailManagerUI.ViewModels
{
    public class AddInvoiceVM: BaseModel
    {
        #region
        private string invoiceNumber;
        public string InvoiceNumber
        {
            get { return invoiceNumber; }
            set 
            {
                invoiceNumber = value;
                Notify();
                AddNewRow_Command.RaiseCanExecuteChanged();
            }
        }

        private string supplier;
        public string Supplier
        {
            get { return supplier; }
            set { supplier = value; Notify(); AddNewRow_Command.RaiseCanExecuteChanged(); }
        }

        private DateTime invoiceDate;
        public DateTime InvoiceDate
        {
            get { return invoiceDate; }
            set { invoiceDate = value; Notify();}
        }

        private decimal subtotal;
        public decimal Subtotal
        {
            get { return subtotal;  }
            set { 
                subtotal = value;
                Notify();
                AddNewRow_Command.RaiseCanExecuteChanged();
                Total = Subtotal + tax; 
            }
        }

        private decimal tax;
        public decimal Tax
        {
            get { return tax; }
            set { 
                tax = value;
                Notify();
                AddNewRow_Command.RaiseCanExecuteChanged();
                Total = Subtotal + tax;
            }
        }

        private decimal total;
        public decimal Total
        {
            get { return total; }
            set { total = subtotal + tax; Notify(); AddNewRow_Command.RaiseCanExecuteChanged();
            }
        }
        #endregion
        private readonly IInvoiceData invoiceData;
        // InvoiceModel properties
        
        // InvoiceDetailsModel properties
        #region

        private string productName;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; Notify(); }
        }

        private float quantity;

        public float Quantity
        {
            get { return quantity; }
            set { quantity = value; Notify(); }
        }

        private decimal purchasePrice;

        public decimal PurchasePrice
        {
            get { return purchasePrice; }
            set { purchasePrice = value; Notify(); }
        }

        private decimal productTax;

        public decimal ProductTax
        {
            get { return productTax; }
            set { productTax = value; Notify(); }
        }

        private decimal productPurchasePrice;

        public decimal ProductPurchasePrice
        {
            get { return productPurchasePrice; }
            set { productPurchasePrice = value; Notify(); }
        }

        #endregion
        public static ObservableCollection<InvoiceDetailsModelUI> InvoiceDetailsCollection { get; set; }

        public SyncCommand Window_ContentRendered_Command { get; private set; }

        public AddInvoiceVM()
        {

        }


        public AddInvoiceVM(IInvoiceData _invoiceData)
        {
            invoiceData = _invoiceData;
            SubmitForm_Command = new SyncCommand(SubmitForm);
            AddNewRow_Command = new SyncCommand(AddNewRow, ValidateInvoice);
            Window_ContentRendered_Command = new SyncCommand(Window_ContentRendered);
            InvoiceDetailsCollection = new ObservableCollection<InvoiceDetailsModelUI>();
            ValidateInput();
        }

        public SyncCommand AddNewRow_Command { get; private set; }

        private void AddNewRow()
        {
            StartupVM.UIDispatcher[nameof(AddInvoiceDetailsVM)].ShowModalUI();
        }

        public SyncCommand SubmitForm_Command { get; private set; }
        private void SubmitForm()
        {
            var result = MsgBox.Show("Verificati daca datele sunt corecte si confirmati", "Confirmare", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                string sql = "BEGIN TRY BEGIN TRANSACTION INSERT INTO dbo.Invoice (Invoice, Supplier, DateIn, Subtotal, Tax, Total)" +
                             $" VALUES ('{invoiceNumber}', '{supplier}', '{invoiceDate}', {subtotal},  {tax}, {total})";

                foreach (var item in InvoiceDetailsCollection)
                {
                    sql += " INSERT INTO dbo.InvoiceDetail (InvoiceId, ProductId, Quantity, PurchasePrice, Tax, TotalPrice)" +
                           $" VALUES ((SELECT TOP 1 Id FROM dbo.Invoice WHERE Invoice='{invoiceNumber}' AND Supplier = '{supplier}' ORDER BY Id DESC ), (SELECT Id from dbo.Product WHERE Name='{item.Name}'), {item.Quantity}, {item.PurchasePrice}, {item.Tax}, {item.TotalPrice})";
                    sql += $" UPDATE dbo.Stock SET StockIn = Stockin + {item.Quantity} WHERE ProductId = (SELECT Id from dbo.Product WHERE Name='{item.Name}')";
                    
                }
                sql += "COMMIT END TRY BEGIN CATCH ROLLBACK END CATCH";

                ServerResponseModel<InvoiceModel> response = invoiceData.AddInvoice(sql);

                if (response.Count > 0 || response.Error == null)
                {
                    MsgBox.Show("Baza de date a fost acualizata cu succes!", " ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MsgBox.Show("Cererea nu a fost procesata din cauza unei erori, iar produsul nu a fost adaugat", "Eroare de procesare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        private void ValidateInput()
        {
            HelpText += "Butonul de adaugat se activeaza cand toate campurile sunt valide:\n" +
                "\t - Numarul facturii reprezinta numarul facturii furnizorului si este obligatoriu";
        }

        private bool ValidateInvoice()
        {
            return !string.IsNullOrEmpty(invoiceNumber) && !string.IsNullOrEmpty(supplier) && subtotal > 0;
        }

        public void Window_ContentRendered()
        {
            if (!string.IsNullOrEmpty(this.Id))
            {
                var invoiceDetails =  invoiceData.GetInvoiceDetailsById(Convert.ToInt32(this.Id));
                var invoice = ConfigureAutoMapper().Map<IEnumerable<InvoiceDetailsModelUI>>(invoiceDetails);
                foreach (var item in invoice)
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

























        /// <summary>
        /// Creates a new Deteriorated document or updates an existing one
        /// </summary>
        /* internal async Task SaveAsync()
         {
             await Task.Run(async () =>
             {
                 //if (!isDeterioratedDisplay) 
                 //{
                 //    // get the document number
                 //    string _sql = "START TRANSACTION;\n\n" +
                 //                  "SELECT CONCAT('PVDET', CAST(CONCAT(DATE_FORMAT(CURDATE(), '%y'), IFNULL(MAX(REPLACE(numar_proces, CONCAT('PVDET', DATE_FORMAT(CURDATE(), '%y')), '')), CONCAT('00000'))) AS UNSIGNED) + 1) " +
                 //                  "INTO @record_id " +
                 //                  "FROM Deteriorate " +
                 //                  "WHERE numar_proces LIKE CONCAT('PVDET', DATE_FORMAT(CURDATE(), '%y'), '%');\n\n";
                 //    // insert the document products and subtract their quantity from products stock and local warehouses
                 //    foreach (DeterioratedProductsM _item in SourceSelectedProducts)
                 //    {
                 //        _sql += "INSERT INTO _produse_deteriorate (numar_proces, numar_articol, nume_produs, cantitate, discount, unitate_masura, pret_unitar, pret, tva, pret_tva, motiv, warehouse_id) " +
                 //                   "VALUES (@record_id, '" + _item.ArticleNumber + "', '" + _item.Name + "', " + _item.WholeQuantity.ToString("F2", CultureInfo.InvariantCulture) + ", " + _item.VATPercent.ToString("F2", CultureInfo.InvariantCulture) + ", '" + _item.MeasureUnit + "', " + _item.UnitaryPriceWithoutVAT.ToString("F2", CultureInfo.InvariantCulture) + ", " +
                 //                _item.TotalPriceWithoutVAT.ToString("F2", CultureInfo.InvariantCulture) + ", " + _item.VATValue.ToString("F2", CultureInfo.InvariantCulture) + ", " + _item.TotalPriceWithVAT.ToString("F2", CultureInfo.InvariantCulture) + ", '" + _item.SelectedReason.Text + "', '" + _item.WarehouseSelectedItem.WarehouseId + "');\n\n";

                 //        _sql += "UPDATE Produse SET stoc_initial = stoc_initial - " + _item.WholeQuantity.ToString("F2", CultureInfo.InvariantCulture) +
                 //               ", net_total = net_total - (" + _item.WholeQuantity.ToString("F2", CultureInfo.InvariantCulture) + " * IFNULL(net_contents, 1)) WHERE numar_articol = '" + _item.ArticleNumber + "';\n\n";

                 //        _sql += "UPDATE _produse_gestiuni_locale AS pgl LEFT JOIN Produse AS p ON p.numar_articol = pgl.numar_articol " +
                 //                   "SET pgl.cantitate = pgl.cantitate - " + _item.WholeQuantity.ToString("F2", CultureInfo.InvariantCulture) + ", pgl.net_total = pgl.net_total - (" + _item.WholeQuantity.ToString("F2", CultureInfo.InvariantCulture) + " * IFNULL(p.net_contents, 1)) " +
                 //                   "WHERE pgl.numar_articol = '" + _item.ArticleNumber + "' AND pgl.warehouse_id = '" + _item.WarehouseSelectedItem.WarehouseId + "';\n\n" +

                 //                   "INSERT INTO _produse_gestiuni_locale (warehouse_id, numar_articol, cantitate, cantitate_comandata, net_total) " +
                 //                   "SELECT '" + _item.WarehouseSelectedItem.WarehouseId + "', '" +
                 //                               _item.ArticleNumber + "', -" + _item.WholeQuantity.ToString("F2", CultureInfo.InvariantCulture) +
                 //                           ", 0, -" + _item.WholeQuantity.ToString("F2", CultureInfo.InvariantCulture) + " * (SELECT p.net_contents FROM Produse AS p WHERE p.numar_articol = '" + _item.ArticleNumber + "') " +
                 //                   "FROM dual " +
                 //                   "WHERE NOT EXISTS (" +
                 //                   "SELECT pgl.cantitate " +
                 //                   "FROM _produse_gestiuni_locale AS pgl " +
                 //                   "WHERE pgl.numar_articol = '" + _item.ArticleNumber + "' AND pgl.warehouse_id = '" + _item.WarehouseSelectedItem.WarehouseId + "');\n\n";
                 //    }
                 //    // insert the document itself and commit changes
                 //    _sql += "INSERT INTO Deteriorate (numar_proces, data, editat_de) VALUES " +
                 //               "(@record_id, '" +
                 //               StartupVM.currentServerDate.ToString("yyMMdd") + "', '" +
                 //               Crypto.Decrypt((string)StartupVM.regKey.GetValue("username")) + "');\n\n";
                 //    _sql += "SELECT @record_id;\n\n";
                 //    _sql += "COMMIT;";
                 //    // return the generated document id
                 //    Id = (await Database.Query<GenericResponse>("QUERY", _sql))?.Data[0].StatusData;
                 //    // print the document
                 //    await PrintPDFAsync();
                 //    StartupVM.MsgBox.Show("Procesul verbal de deteriorare a fost creat!", "Adaugare Proces Verbal Deteriorare", MessageBoxButton.OK, MessageBoxImage.Information);
                 //}
                 //else
                 //    dispatcher.Dispatch((SendOrPostCallback)delegate { CloseView(); }, true);
             });*/

    }
}

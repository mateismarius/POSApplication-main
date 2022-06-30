using DataAccesLibrary.Internal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.Interfaces
{
    public interface IInvoiceData
    {
        ServerResponseModel<InvoiceModel> AddInvoice(string sql);
        ServerResponseModel<InvoiceModel> GetAllInvoices();
        IEnumerable<GetInvoiceDetailsModel> GetInvoiceDetailsById(int invoiceId);
        ServerResponseModel<AddInvoiceDetailModel> DeleteInvoice(string sql);
        ServerResponseModel<InvoiceModel> GetInvoiceById(string sql);
    }
}

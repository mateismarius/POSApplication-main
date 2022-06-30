using DataAccesLibrary.Internal.DataAccess;
using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.DataAccess
{
    public class InvoiceData : IInvoiceData
    {
        private readonly ISqlDataAccess sqlDataAccess;

        public InvoiceData(ISqlDataAccess _sqlDataAccess)
        {
            sqlDataAccess = _sqlDataAccess;
        }

        public ServerResponseModel<InvoiceModel> AddInvoice(string sql)
        {
            return sqlDataAccess.ExecuteRawSql<InvoiceModel>(sql, "retailApp");
        }

        public ServerResponseModel<InvoiceModel> GetAllInvoices()
        {
            ServerResponseModel<InvoiceModel> serverResponse = new ServerResponseModel<InvoiceModel>();
            IEnumerable<InvoiceModel> invoiceModel;
            try
            {
                invoiceModel = sqlDataAccess.GetAllData<InvoiceModel>("dbo.spGetAllInvoices", "retailApp");
                serverResponse.Data = invoiceModel;
                serverResponse.Count = invoiceModel.Count();
            }
            catch (Exception ex)
            {
                serverResponse.Error = "Eroare la conexiunea cu baza de date " + ex.Message;
            }

            return serverResponse;
        }


        public IEnumerable<GetInvoiceDetailsModel> GetInvoiceDetailsById(int _invoiceId)
        {
            var p = new
            {
                invoiceId = _invoiceId,
            };
            return sqlDataAccess.LoadData<GetInvoiceDetailsModel, dynamic>("dbo.spGetInvoiceDetails", p, "retailApp");
        }

        public ServerResponseModel<AddInvoiceDetailModel> DeleteInvoice(string sql)
        {
           return sqlDataAccess.ExecuteRawSql<AddInvoiceDetailModel>(sql, "retailApp");
        } 

        public ServerResponseModel<InvoiceModel> GetInvoiceById(string sql)
        {
            return sqlDataAccess.QueryRawSql<InvoiceModel>(sql, "retailApp");
        }
    }

}

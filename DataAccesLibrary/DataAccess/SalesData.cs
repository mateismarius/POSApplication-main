using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.DataAccess
{
    public class SalesData: ISalesData
    {
        private readonly ISqlDataAccess sqlDataAccess;
        public SalesData(ISqlDataAccess _sqlDataAccess)
        {
            sqlDataAccess = _sqlDataAccess;
        }

        public ServerResponseModel<SalesReportModel> GetAllSales()
        {
            ServerResponseModel<SalesReportModel> serverResponse = new ServerResponseModel<SalesReportModel>();
            IEnumerable<SalesReportModel> invoiceModel;
            try
            {
                invoiceModel = sqlDataAccess.GetAllData<SalesReportModel>("dbo.spGetAllSales", "retailApp");
                serverResponse.Data = invoiceModel;
                serverResponse.Count = invoiceModel.Count();
            }
            catch (Exception ex)
            {
                serverResponse.Error = "Eroare la conexiunea cu baza de date! " + ex.Message;
            }

            return serverResponse;
        }

        public ServerResponseModel<SaleDetailsModel> DeleteSaleDetails(string sql)
        {
            return sqlDataAccess.ExecuteRawSql<SaleDetailsModel>(sql, "retailApp");
        }

        public ServerResponseModel<SalesReportModel> DeleteSale(string sql)
        {
            return sqlDataAccess.ExecuteRawSql<SalesReportModel>(sql, "retailApp");
        }

        public ServerResponseModel<SalesReportModel> GetSaleById(string sql)
        {
            return sqlDataAccess.QueryRawSql<SalesReportModel>(sql, "retailApp");
        }

        public IEnumerable<SaleDetailsModel> GetSaleDetailsById(int _saleId)
        {
            var p = new
            {
                saleId = _saleId,
            };
            return sqlDataAccess.LoadData<SaleDetailsModel, dynamic>("dbo.spGetSaleDetails", p, "retailApp");
        }
    }
}

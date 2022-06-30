using DataAccesLibrary.Internal.Models;
using System.Collections.Generic;

namespace DataAccesLibrary.Internal.Interfaces
{
    public interface ISalesData
    {
        ServerResponseModel<SalesReportModel> GetAllSales();
        ServerResponseModel<SaleDetailsModel> DeleteSaleDetails(string sql);
        ServerResponseModel<SalesReportModel> DeleteSale(string sql);
        ServerResponseModel<SalesReportModel> GetSaleById(string sql);
        IEnumerable<SaleDetailsModel> GetSaleDetailsById(int _saleId);
    }
}

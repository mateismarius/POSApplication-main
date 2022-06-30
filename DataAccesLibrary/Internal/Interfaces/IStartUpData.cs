using DataAccesLibrary.Internal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.Interfaces
{
    public interface IStartUpData
    {
        int AddProduct(ProductModel product);
        IEnumerable<ProductModel> GetAllProducts();
        UserModel GetActiveUser();
        Task<ProductModel> GetProductByParameter<ProductModel>(string text);
        void UpdateProduct<ProductModel>(ProductModel product);
        ServerResponseModel<SalesModel> AddSale(string sql);
        IEnumerable<ProductReportModel> GetProductReport();
        IEnumerable<StockModel> GetStocks();
    }
}

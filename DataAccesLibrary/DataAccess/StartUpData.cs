using DataAccesLibrary.Internal.DataAccess;
using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.DataAccess
{   

    public class StartUpData : IStartUpData
    {
        private readonly ISqlDataAccess sqlDataAccess;

        public StartUpData(ISqlDataAccess _sqlDataAccess)
        {
            sqlDataAccess = _sqlDataAccess;
        }

        public int AddProduct(ProductModel product)
        {
           return sqlDataAccess.SaveData("dbo.spAddProduct", product, "retailApp");
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            return sqlDataAccess.GetAllData<ProductModel>("dbo.spProductsAll", "retailApp");
        }
        public void UpdateProduct<ProductModel>(ProductModel product)
        {

            sqlDataAccess.UpdateData<ProductModel>("dbo.spUpdateProduct", product, "retailApp");
        }

        public async Task<ProductModel> GetProductByParameter<ProductModel>(string text)
        {
            string query = $"SELECT Name, Unit, RetailPrice, Tax from dbo.Product where Barcode='{text}' or Name='{text}'";

            return await sqlDataAccess.LoadDataByParameterAsync<ProductModel>(query, text, "retailApp");
        }

        public UserModel GetActiveUser()
        {
            string query = "SELECT Id, FirstName, LastName, Password, Barcode, IsAdmin from [dbo].[User] where IsActive=1";

            return sqlDataAccess.LoadUserByParameterAsync(query, "retailApp");
        }

        public ServerResponseModel<SalesModel> AddSale(string sql)
        {
            return sqlDataAccess.ExecuteRawSql<SalesModel>(sql, "retailApp");
        }

        public IEnumerable<ProductReportModel> GetProductReport()
        {
            return sqlDataAccess.GetAllData<ProductReportModel>("dbo.spGetProductReport", "retailApp");
        }

        public IEnumerable<StockModel> GetStocks()
        {
            return sqlDataAccess.GetAllData<StockModel>("dbo.spGetStocks", "retailApp");
        }
    }
}

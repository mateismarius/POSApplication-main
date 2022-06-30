using DataAccesLibrary.Internal.Interfaces;
using DataAccesLibrary.Internal.Models;
using RetailManagerUI.Models.Common;
using RetailManagerUI.ViewModels.Common.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RetailManagerUI.ViewModels
{
    public class ProductVM : BaseModel
    {
        #region==========================================================================PROPERTIES====================================================================================================
        private readonly IStartUpData productData;

        private ICollectionView productsCollection;

        public ICollectionView ProductsCollection
        {
            get { return productsCollection; }
            set { productsCollection = value; Notify(); }
        }


        private ObservableCollection<ProductReportModel> products;
        public ObservableCollection<ProductReportModel> Products
        {
            get { return products; }
            set { products = value; Notify(); }
        }

        

        private string productsSearchText = "";
        public string ProductsSearchText
        {
            get { return productsSearchText; }
            set { productsSearchText = value; Notify(); ProductsCollection.Filter += FilterProducts; ProductsCollection.Refresh(); }
        }


        private int stock;

        public int Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        public AsyncCommand Window_ContentRendered_Command { get; private set; }
        public AsyncCommand OpenAddProductWindow_Command { get; private set; }
        #endregion

        #region=========================================================================CONSTRUCTORS===================================================================================================
        public ProductVM()
        {

        }
        public ProductVM(IStartUpData _productData)
        {
            
            productData = _productData;
            ShowProgressBar();
            OpenAddProductWindow_Command = new AsyncCommand(OpenAddProductWindow);
            Products = new ObservableCollection<ProductReportModel>();
            Window_ContentRendered_Command = new AsyncCommand(Window_ContentRendered);
        }

        private bool FilterProducts(object obj)
        {
            if(obj is ProductReportModel productReportModel)
            {
                return productReportModel.Barcode.ToLower().Contains(productsSearchText.ToLower()) || productReportModel.Name.ToLower().Contains(productsSearchText.ToLower());
            }
            return false;
        }
        #endregion

        #region============================================================================METHODS=====================================================================================================
        /// <summary>
        /// Fill the product's collection bounded to listview
        /// </summary>
        private async Task Window_ContentRendered()
        {
            await GetProductsReport();
            ProductsCollection = CollectionViewSource.GetDefaultView(Products);
            HideProgressBar();
        }

        private async Task GetProductsReport()
        {
            List<ProductReportModel> temp = new List<ProductReportModel>();
            await Task.Run(() =>
            {
                var productCollection = productData.GetProductReport();
                foreach (var product in productCollection)
                {
                    product.Stock = product.StockIn - product.Stockout;
                    temp.Add(product);
                }
            });

            Products = new ObservableCollection<ProductReportModel>(temp);

        }

        /// <summary>
        /// Open AddProduct window
        /// </summary>
        private async Task OpenAddProductWindow()
        {
            StartupVM.UIDispatcher[nameof(AddProductVM)].ShowModalUI();
            // refresh products list
            await GetProductsReport();

        }
        #endregion
    }
}

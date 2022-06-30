using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.Models
{
    public class ProductReportModel
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Unit { get; set; }
        public decimal RetailPrice { get; set; }
        public double Tax { get; set; }
        public string Category { get; set; }
        public string Tag1 { get; set; }
        public string Tag2 { get; set; }
        public DateTime ExpiryDate { get; set; }
        public float StockIn { get; set; }
        public float Stockout { get; set; }
        public float Stock { get; set; }

    }
}

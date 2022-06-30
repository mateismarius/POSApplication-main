using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.Models
{
    public class InsertInvoiceModel
    {
        public string Invoice { get; set; }
        public string Supplier { get; set; }
        public DateTime DateIn { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public string ProductName { get; set; }
        public float Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal ProductTax { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

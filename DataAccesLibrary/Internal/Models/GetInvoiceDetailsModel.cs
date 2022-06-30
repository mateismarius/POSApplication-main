using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.Models
{
    public class GetInvoiceDetailsModel
    {
        public string Invoice { get; set; }
        public string Supplier { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

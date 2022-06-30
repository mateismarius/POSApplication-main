using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.Models
{
    public class SaleDetailsModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public float Quantity { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }
}

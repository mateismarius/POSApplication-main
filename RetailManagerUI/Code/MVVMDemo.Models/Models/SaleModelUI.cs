using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagerUI.Models.Models
{
    public class SaleModelUI
    {
        public int Crt { get; set; }
        public string ProductName { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public decimal SaleUnitValue { get; set; }
    }
}

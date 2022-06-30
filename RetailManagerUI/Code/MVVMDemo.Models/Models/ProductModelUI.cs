using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagerUI.Models.Models
{
    public class ProductModelUI
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
        public int Purchase { get; set; }
        public int Sales { get; set; }
        public int Stock { get; set; }
    }
}

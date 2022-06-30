using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailManagerUI.Models.Models
{
    public class InvoiceModelUI // DTO - data transfer object (POCO - Plain Old CLR (C#) Object)
    {
        public int Id { get; set; }
        public string Invoice { get; set; }
        public string Supplier { get; set; }
        public DateTime DateIn { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        
    }
}

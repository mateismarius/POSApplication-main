using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.Models
{
    public class SalesReportModel
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Total { get; set; }
    }
}

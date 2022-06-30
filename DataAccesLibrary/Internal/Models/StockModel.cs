using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.Models
{
    public class StockModel
    {
        public int ProductId { get; set; }
        public float Stockin { get; set; }
        public float Stockout { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLibrary.Internal.Models
{
    public class ServerResponseModel<T>
    {
        #region ================================================================ PROPERTIES =================================================================================
        public IEnumerable<T> Data { get; set; }
        public int Count { get; set; }
        public string Error { get; set; }
        #endregion
    }
}

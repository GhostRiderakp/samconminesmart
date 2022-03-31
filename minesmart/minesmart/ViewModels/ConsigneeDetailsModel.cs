using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace minesmart.ViewModels
{
    public class ConsigneeDetailsModel
    {
        public string SsoId { get; set; }
        public string UserId { get; set; }

        public string ProductId { get; set; }
        
        public DataTable consigneeDetails { get; set; }
        public string MineralUsedFor { get; set; }
        public string Status { get; set; }

        public string PostUrl { get; set; }
        public string Result { get; set; }

        public string Success { get; set; }

        public DataTable consigneeAddressDetails { get; set; }

        public DataTable LeaseMineralList { get; set; }
        public DataTable leaselistdt { get; set; }
        public string ErrorCode { get; set; }

        public string ConsigneeId { get; set; }

        public string Message { get; set; }
        public int Action { get; set; }

        public string Types { get; set; }

        public string Authtoken { get; set; }

        public long ClientId { get; set; }

    }
}

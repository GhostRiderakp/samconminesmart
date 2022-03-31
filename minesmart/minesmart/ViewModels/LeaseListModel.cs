using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesmart.ViewModels
{
    public class LeaseListModel
    {
        public string SsoId { get; set; }
        public string UserId { get; set; }
        public string Mineralname { get; set; }
        public long LeaseId { get; set; }
        public string Address { get; set; }
        public string LeaseNo { get; set; }
        public string Status { get; set; }
        public string PostUrl { get; set; }
        public string Result { get; set; }
        public string ErrorCode { get; set; }
        public DataTable leaselistdt { get; set; }
        public string Message { get; set; }
        public int Action { get; set; }
        public string Types { get; set; }
        public string Authtoken { get; set; }
       
    }
    public class dealerdtl
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

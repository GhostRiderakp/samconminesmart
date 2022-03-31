using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesmart.ViewModels
{
    public class DealerModel
    {
        public string DInfoId { get; set; }
        public string SSOID { get; set; }
        public string UserId { get; set; }
        public string DealerEnrolId { get; set; }
        public string DealerName { get; set; }
        public string FirmName { get; set; }
        public string GstinNo { get; set; }
        public string PanNo { get; set; }
        public string PrimaryAddress { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DistrictNameE { get; set; }
        public string TehsilNameE { get; set; }
        public string VillageNameE { get; set; }
        public string PinCode { get; set; }
        public string Authtoken { get; set; }
        public DataTable DealerInfolist { get; set; }

        public string Types { get; set; }

        public string PostUrl { get; set; }

    }
}

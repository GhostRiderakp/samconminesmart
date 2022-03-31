using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace minesmart.ViewModels
{
    public class AllTP
    {
        public class JSONClass
        {
            public string Status { get; set; }

            [JsonProperty("DealerList")]
            public DealerLists[] DealerList { get; set; }

            public string TransitPassNumber { get; set; }
            public string MessageDiscription { get; set; }

        }

        public partial class DealerLists
        {
            [JsonProperty("dealerId")]
            public int dealerId { get; set; }

            [JsonProperty("dealerName")]
            public string dealerName { get; set; }

            [JsonProperty("firmName")]
            public string firmName { get; set; }

            [JsonProperty("locationList")]
            public locationLists[] locationList { get; set; }

            [JsonProperty("consigneeList")]
            public consigneeLists[] consigneeList { get; set; }




        }


        public partial class locationLists
        {
            [JsonProperty("stockLocId")]
            public int stockLocId { get; set; }

            [JsonProperty("stockLocName")]
            public string stockLocName { get; set; }

            [JsonProperty("mineralList")]
            public mineralLists[] mineralList { get; set; }




        }

        public partial class mineralLists
        {
            [JsonProperty("mineralId")]
            public int mineralId { get; set; }

            [JsonProperty("mineralName")]
            public string mineralName { get; set; }


        }

        public partial class consigneeLists
        {
            [JsonProperty("consigneeId")]
            public int consigneeId { get; set; }

            [JsonProperty("consigneeName")]
            public string consigneeName { get; set; }
            
            [JsonProperty("addressList")]
            public addressLists[] addressList { get; set; }


        }
        public partial class addressLists
        {
            [JsonProperty("addressId")]
            public int addressId { get; set; }

            [JsonProperty("fullAddress")]
            public string fullAddress { get; set; }

            [JsonProperty("distance")]
            public string distance { get; set; }

        }

        public class googlecred
        {
            public string sub { get; set; }
            public string name { get; set; }
            public string given_name { get; set; }
            public string family_name { get; set; }
            public string picture { get; set; }

            public string email { get; set; }
            public string email_verified { get; set; }
            public string locale { get; set; }
        }
    }
}

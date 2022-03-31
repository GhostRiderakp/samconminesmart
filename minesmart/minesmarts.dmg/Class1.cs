using System;
using System.Collections.Generic;

namespace minesmart.DGMS
{
    public class Class1
    {
    }
    public class RawwanResult
    {
        public string Royaltyamount { get; set; }

        public string status { get; set; }
        public string eRawannaNo { get; set; }

        public string availBal { get; set; }

        public string rawannaCount { get; set; }

        public string rawannaDate { get; set; }
        public string TransitPassNumber { get; set; }

        public string MessageDiscription { get; set; }
        public IList<dealerdtl> DealerList { get; set; }

    }
    
    public class dealerdtl
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}

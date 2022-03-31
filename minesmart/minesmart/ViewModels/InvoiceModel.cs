using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace minesmart.ViewModels
{
    public class InvoiceModel
    {
        public int InvoiceId { get; set; }
        public long UserId { get; set; }
        public string SsoId { get; set; }
        public long CompanyId { get; set; }
        public long ClientId { get; set; }

        public string Authtoken { get; set; }
        public string weightbridgeNo { get; set; }

        public string weightbridge { get; set; }
        public string InvoiceNumber { get; set; }


        public string FirmName { get; set; }

        public string FirmAddress { get; set; }

        public string InvoiceDate { get; set; }


        public string ErawannaNumber { get; set; }


        public string Dispatchthrough { get; set; }

        public string Destination { get; set; }

        public string VehicleNumber { get; set; }

        public string BuyerName { get; set; }

        public string BuyerGSTNo { get; set; }

        public string TaxslabId { get; set; }

        public string TaxslabName { get; set; }
        public string TaxslabPer { get; set; }
        public string IGSTAmt { get; set; }
        public string CGSTAmt { get; set; }
        public string SGSTAmt { get; set; }

        public string SubtotalAmt { get; set; }

        public string FinaltotalAmt { get; set; }

        public DataTable InvoiceDetail { get; set; }


    }
}

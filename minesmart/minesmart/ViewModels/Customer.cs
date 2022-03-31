using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace minesmart.ViewModels
{
    public class Customer
    {
        public long CUSTOMERID { get; set; }
        public long ADDRESSID { get; set; }
        public long SALESID { get; set; }
        public long COMPANYID { get; set; }
        public long CLEINTID { get; set; }

        public string WEIGHTBRIDGENO { get; set; }

        public string SSOID { get; set; }

        public string POSTURL { get; set; }

        public string NAME { get; set; }
        public string PHONE_NUMBER { get; set; }
        public string EMAIL_ADDRESS { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string LOCATION { get; set; }

        public string SaleDate { get; set; }

        public string SHIPPINGADDRESS1 { get; set; }
        public string SHIPPINGADDRESS2 { get; set; }
        public string SHIPPINGLOCATION { get; set; }
        public string STATE { get; set; }
        public string CITY { get; set; }
        public string CREATED_USERID { get; set; }
        public string ISACTIVE { get; set; }
        public int STATUS { get; set; }
        public string status { get; set; }

        public string NoofItem { get; set; }
        public string TotalQty { get; set; }
        public string GrossAmount { get; set; }
        public string NetPayable { get; set; }

        public string TotalAmount { get; set; }

        public string OrderNote { get; set; }
        public string SGSTAmount { get; set; }
        public string CGSTAmount { get; set; }
        public string SGSTTotalAmount { get; set; }
        public string CGSTTotalAmount { get; set; }

        public decimal MRPAMOUNT { get; set; }

        public string Salesno { get; set; }
        public int USERID { get; set; }
        public int ITEMID { get; set; }
        public string ITEMNAME { get; set; }
        public string QUANTITY { get; set; }

        public string BackColor { get; set; }

        public long RemainQuantity { get; set; }

        public int SalesDetailsid { get; set; }
    }
}
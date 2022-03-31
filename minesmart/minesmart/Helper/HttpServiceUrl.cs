using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesmart.Helper
{
    public class HttpServiceUrl
    {
        public static string DMGravannaStatusURL = "https://mines.rajasthan.gov.in/DMG2/Public/eRawannaStatus/";

        //public static string PrintWebsiteInvoiceUrl = "http://minesmart.in/Print/Printinvoice?invoId=";
        //public static string PrintWeightSlipWebsiteUrl = "http://minesmart.in/Print/Printweightslip?wsId=";
        //public static string PrintTempWeightSlipWebsiteUrl = "http://minesmart.in/Print/Printtempweightslip?twsId=";
        //public static string PrintCTPWebsiteUrl = "http://minesmart.in/Print/PrintConfirmTransitPass?tpId=";
        //public static string PrintCWebsiteUrl = "http://minesmart.in/Print/PrintConfirmRawaanaInvoice?crId=";
        //public static string localURL = "https://mines.rajasthan.gov.in/services/v1.4/";
        //public static string TrasitlocalURL = "https://mines.rajasthan.gov.in/services/v1.4/";


        public static string PrintWebsiteInvoiceUrl = "https://localhost:44353/Print/Printinvoice?invoId=";
        public static string PrintWeightSlipWebsiteUrl = "https://localhost:44353/Print/Printweightslip?wsId=";
        public static string PrintTempWeightSlipWebsiteUrl = "https://localhost:44353/Print/Printtempweightslip?twsId=";
        public static string PrintCTPWebsiteUrl = "https://localhost:44353/Print/PrintConfirmTransitPass?tpId=";
        public static string PrintCWebsiteUrl = "https://localhost:44353/Print/PrintConfirmRawaanaInvoice?crId=";

        public static string localURL = "http://103.203.138.51/services/v1.4/";
        public static string TrasitlocalURL = "http://103.203.138.51/services/v1.4/";

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERawannaDesk.ViewModels
{
    public class WebAPIModel
    {
        public string Authtoken { get; set; }
        public string PostUrl { get; set; }
        public string MacAddress { get; set; }

        public long CompanyId { get; set; }

        public string Ssoid { get; set; }

        public long userid { get; set; }

        public string weightbridge { get; set; }

        public string accesskey { get; set; }

        public string Password { get; set; }
        public DateTime ExpiryDate { get; set; }
        public WebAPIModelResponse FinalResponse { get; set; }


    }
    
    [Serializable]
    public class WebAPIModelResponse
    {
        public static string Authtoken { get; set; }
        public static string Status { get; set; }
        public static string Message { get; set; }
        public static string ErrorCode { get; set; }
        public static string SsoId { get; set; }
        public static string Password { get; set; }
        public static string Weightbridgeno { get; set; }
        public static string InvoiceId { get; set; }
        public static string InvoiceNo { get; set; }
        public static string Accesskey { get; set; }
        public static string ActiveKey { get; set; }
        public static string success { get; set; }
        public static string Userid { get; set; }
        public static string CameraRearUrl { get; set; }
        public static string CameraFrontUrl { get; set; }
        public static string CameraIPaddress { get; set; }
        public static string Camerausername { get; set; }
        public static string CameraPassword { get; set; }
        public static long CompanyId { get; set; }
        public static long ClientId { get; set; }
        public static int Countresult { get; set; }
        public static string Provider { get; set; }
        public static string CombPortName { get; set; }
        public static string baudrate { get; set; }
        public static string bitRate { get; set; }
        public static string databit { get; set; }
        public static string Isreversed { get; set; }
        public static string Stopbits { get; set; }
        public static string Paritys { get; set; }
        public static string leaseNo { get; set; }
        public static string leaseID { get; set; }
        public static string mineralName { get; set; }
        public static string mineralNameuserfor { get; set; }
        public static string consigneeName { get; set; }
        public static string transportMode { get; set; }
        public static string vehicleNo { get; set; }
        public static string approxTime { get; set; }
        public static string Vehicleweight { get; set; }
        public static string driverMobNo { get; set; }
        public static string driverName { get; set; }
        public static string approxWeight { get; set; }
        public static string statuss { get; set; }
        public static string eRawannaNo { get; set; }
        public static string approxRoyalty { get; set; }
        public static string ravannaDate { get; set; }
        public static string approxTotalAmount { get; set; }
        public static string approxNMET { get; set; }
        public static string location { get; set; }
        public static string TransactionType { get; set; }
        public static string approxDMFT { get; set; }
        public static string vehicleName { get; set; }
        public static string TareWeight { get; set; }
        public static string PostUrl { get; set; }
        public static string FCameraurl1 { get; set; }
        public static string FCameraurl2 { get; set; }
        public static string LanguageSetting { get; set; }
        public static string Parity { get; set; }
        public static string DataBits { get; set; }

        public static string StopBits { get; set; }
        public static string ReaderCode { get; set; }

        public static string SytemType { get; set; }


        public static string stockLocId { get; set; }
        public static string stockLocName { get; set; }

        public static string address { get; set; }
        public static string districtNameE { get; set; }

        public static string tehsilNameE { get; set; }

        public static string villageNameE { get; set; }

        public static string pincode { get; set; }

        public static string FirmName { get; set; }

        public static string FirmAddress { get; set; }

        public static string FirmGSTNumber { get; set; }
    }

}

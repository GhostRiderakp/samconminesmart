
using ERawannaDesk.DGMS;
using ERawannaDesk.Properties;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERawannaDesk.Helper
{
    public class Cls_PostMines
    {
        public static string applicationPath = Application.StartupPath;
        public static string LogFolderPath = applicationPath + "\\RawannaLog";
        //private static SerialPortManager _spManager = new SerialPortManager();
        private static Dictionary<string, string> _valueObject;
        private static HttpClient client = new HttpClient();
        public static string appVersion = "15";
        public static string IN_AWSURL = "https://9bazujuivi.execute-api.ap-south-1.amazonaws.com/prod/erawanna";
        public static string AWSURL = "https://l84ber8h7f.execute-api.us-east-2.amazonaws.com/prod/licenceValidate";
        public static string AWSAction = "dmgpass";
        //public static string getRegEmail() => Settings.Default["RegEmail"].ToString();

        //static readonly string SSL_DealerInfo = HttpService.localURL + "eravanna/dealerInfo";
        static readonly string SSL_DealerStockLocationInfo = HttpService.localURL + "SSL/stockLocation";
        static readonly string SSL_MineralList = HttpService.localURL + "eravanna/mineralDetail/";
        static readonly string SSL_GetConsigneeDetails = HttpService.localURL + "eravanna/consigneeDetail/";
        static readonly string SSL_GetconsigneeAddressDetails = HttpService.localURL + "eravanna/consigneeAddress/";
        static readonly string SSL_SearchConsignee = HttpService.localURL + "eravanna/searchConsignee/";
        static readonly string SSL_MineralUsedFor = HttpService.localURL + "eravanna/mineralUsedFor/";
        static readonly string SSL_GenerateConfirmedTP = HttpService.localURL + "eravanna/generateConfirmTransitPass/";
        static readonly string SSL_leaseDetail = HttpService.localURL + "eravanna/leaseDetail";
        static readonly string SSL_dealerStockDetail = HttpService.localURL + "eravanna/dealerStockLoaction/";
        static readonly string SSL_dealerInfo = HttpService.localURL + "eravanna/leaseDetail";
        static readonly string SSL_SearchRawannaDetail = HttpService.localURL + "eravanna/rawannaDetail";
        private static string SSL_GenerateRawanna = HttpService.localURL + "eravanna/generateRawanna";

        static readonly string SSL_VehicleInfo = HttpService.localURL + "eravanna/vehicleDetail";


        private static string GetMinerals = HttpService.serverURL + "eravanna/mineralDetail";
        private static string rawannaDetail = HttpService.serverURL + "eravanna/rawannaDetail";
        private static string generateconfirmrawanna = HttpService.localURL + "eravanna/generateConfirmRawanna";

        #region TP
        static readonly string SSL_TPallInfo = HttpService.TrasitlocalURL + "tp/allTpDetail";
        static readonly string SSL_TPdealerInfo = HttpService.TrasitlocalURL + "tp/dealerInfo";
        static readonly string SSL_TPStockLocation = HttpService.TrasitlocalURL + "tp/stockLocation";
        static readonly string SSL_TPMineralDetail = HttpService.TrasitlocalURL + "tp/mineralDetail";
        static readonly string SSL_TPConsigneeDetail = HttpService.TrasitlocalURL + "tp/consigneeDetail";
        static readonly string SSL_TPSearchDetail = HttpService.localURL + "eravanna/rawannaDetail";
        private static string SSL_TPGenerateConfirmTransitPass = HttpService.TrasitlocalURL + "tp/generateConfirmTransitPass";
        private static string SSL_TPTranistPass = HttpService.localURL + "eravanna/generateRawanna";
        #endregion

        //public static String providerAccessKey = "jESrtAg9DObEjyW/N/MCvm8uUAoJgIAqmd8iyoJW7dhqtZBpSHrarLpy5txr8XqnzJGmDQfBwlTK1fLhs+Trj4XxpTJLHjSFw0rg3ljAykezosRj4bOZngHa6MEwO0HZofV5ahOPX1vVl1oBVFkU19fM2XtxMStawYBlhTMr0rhyA35zUfRlQAFXYtrJEmk4S57syMoGBllFJQzjO4mRFx05gUkhAcOPmBCWd7AA0dvgF4p/+ZFKzI3k0MpOfN4wnBCQwk80tlkg517Qk7u4c/cbB+rZoDegkrNHuIEHlbehSC2jpncuLR7LvnMaCrUgV+TOTBkovXWSmJBv2BKL/Q==@@@@AQAB";
        //public static String providerID = "00015.7.1";

        public static async Task<DataTable> getconsigneeDetail(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            string andPost = await ParseAndPost(SSL_GetConsigneeDetails, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            return Tabulate(andPost);
        }
        public static async Task<DataTable> getvehicleDetail(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(SSL_VehicleInfo, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DMG Responce Parsing Error");
                return null;
            }
        }
        #region Transit Pass
        public static async Task<DataTable> getvehicleDetailTP(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(SSL_VehicleInfo, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DMG Responce Parsing Error");
                return null;
            }
        }
        public static async Task<DataTable> getstockLocation(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPostTP(SSL_TPStockLocation, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DMG Responce Parsing Error");
                return null;
            }
        }
        public static async Task<DataTable> TPdealerDetails(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            string andPost = string.Empty;
            DataTable dtJsonresp = new DataTable();
            try
            {
                dtJsonresp.Columns.Add("ID", typeof(Int32));
                dtJsonresp.Columns.Add("Name", typeof(string));
                dtJsonresp.Columns.Add("FirmName", typeof(string));
                dtJsonresp.Columns.Add("Type", typeof(string));
                andPost = await ParseAndPost(SSL_TPallInfo, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var Items = jss.Deserialize<ERawannaDesk.ViewModels.AllTP.JSONClass>(andPost);
                DataRow dr = dtJsonresp.NewRow();
                foreach (var str in Items.DealerList)
                {
                    dr = dtJsonresp.NewRow();
                    dr["ID"] = str.dealerId;
                    dr["Name"] = str.dealerName;
                    dr["FirmName"] = str.firmName;
                    dr["Type"] = "dealer";
                    dtJsonresp.Rows.Add(dr);

                }
                foreach (var str in Items.DealerList.Select(x => x.consigneeList))
                {
                    dr = dtJsonresp.NewRow();
                    dr["ID"] = str.Select(x => x.consigneeId).FirstOrDefault();
                    dr["Name"] = str.Select(x => x.consigneeName).FirstOrDefault();
                    dr["Type"] = "consignee";
                    dtJsonresp.Rows.Add(dr);

                }
                foreach (var str in Items.DealerList.Select(x => x.locationList))
                {
                    dr = dtJsonresp.NewRow();
                    dr["ID"] = str.Select(x => x.stockLocId).FirstOrDefault();
                    dr["Name"] = str.Select(x => x.stockLocName).FirstOrDefault();
                    dr["Type"] = "location";
                    dtJsonresp.Rows.Add(dr);

                }
                foreach (var str in Items.DealerList.Select(x => x.locationList).FirstOrDefault().Select(c => c.mineralList))
                {
                    dr = dtJsonresp.NewRow();
                    dr["ID"] = str.Select(x => x.mineralId).FirstOrDefault();
                    dr["Name"] = str.Select(x => x.mineralName).FirstOrDefault();
                    dr["Type"] = "mineralList";
                    dtJsonresp.Rows.Add(dr);

                }
                foreach (var str in Items.DealerList.Select(x => x.consigneeList).FirstOrDefault().Select(x => x.addressList))
                {
                    dr = dtJsonresp.NewRow();
                    dr["ID"] = str.Select(x => x.addressId).FirstOrDefault();
                    dr["Name"] = str.Select(x => x.fullAddress).FirstOrDefault();
                    dr["Type"] = "addressList";
                    dtJsonresp.Rows.Add(dr);

                }
            }
            catch (Exception ex)
            {

            }
            //List<Dictionary<string, string>> deserializedProduct = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(andPost);


            //var rootOfList = JsonConvert.DeserializeObject<RootObject<List<ERawannaDesk.ViewModels.AllTP.JSONClass>>>(andPost);
            //DataSet dataSet = JObject.Parse(andPost)["Status"].ToObject<DataSet>();
            //DataTable dt = JObject.Parse(andPost)["DealerList"].ToObject<DataTable>();
            //DataTable dt = (DataTable)JsonConvert.DeserializeObject(Convert.ToString(andPost.Replace("[", "").Replace("]", "")), (typeof(DataTable)));

            return dtJsonresp;
        }
        public static DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            try
            {

                string[] jsonStringArray = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
                List<string> ColumnsName = new List<string>();
                foreach (string jSA in jsonStringArray)
                {
                    string[] jsonStringData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                    foreach (string ColumnsNameData in jsonStringData)
                    {
                        try
                        {
                            int idx = ColumnsNameData.IndexOf(":");
                            string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                            if (!ColumnsName.Contains(ColumnsNameString))
                            {
                                ColumnsName.Add(ColumnsNameString);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                        }
                    }
                    break;
                }
                foreach (string AddColumnName in ColumnsName)
                {
                    dt.Columns.Add(AddColumnName);
                }
                foreach (string jSA in jsonStringArray)
                {
                    string[] RowData = Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                    DataRow nr = dt.NewRow();
                    foreach (string rowData in RowData)
                    {
                        try
                        {
                            int idx = rowData.IndexOf(":");
                            string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                            string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                            nr[RowColumns] = RowDataString;
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                    dt.Rows.Add(nr);
                }
            }
            catch (Exception ex)
            {

            }

            return dt;
        }

        public static async Task<DataTable> getTPMineralDetail(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            string andPost = await ParseAndPostTP(SSL_TPMineralDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            return Tabulate(andPost);
        }
        public static async Task<DataTable> getTPConsigneeDetails(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            string andPost = await ParseAndPostTP(SSL_TPConsigneeDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            return Tabulate(andPost);
        }
        public static async Task<DataTable> TPgenerateTransitPass(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            string andPost = await ParseAndPost(SSL_TPTranistPass, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
            if (!jObject[0].ToString().Contains("300") && !jObject[0].ToString().Contains("301"))
            {
                return JsonStringToDataTable(andPost);
            }
            else
            {
                MessageBox.Show(andPost, "DMG Responce Parsing Error");
                return null;
            }
        }

        #endregion
        public static async Task<DataTable> getconsigneeAddressDetail(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(SSL_GetconsigneeAddressDetails, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DMG Responce Parsing Error");
                return null;
            }
        }
        public static async Task<DataTable> getMineralDetail(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(SSL_MineralList, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DMG Responce Parsing Error");
                return null;
            }
        }
        public static async Task<DataTable> generateconfirmrawannadetail(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(generateconfirmrawanna, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
                if (!jObject[0].ToString().Contains("301"))
                {
                    MessageBox.Show(andPost, "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                    int i = 0;
                    List<ERawannaDesk.ViewModels.RawwanResult> classDetails = new List<ERawannaDesk.ViewModels.RawwanResult>();
                    foreach (var item in jObject)
                    {
                        if (((Newtonsoft.Json.Linq.JProperty)item).Name != "Status")
                        {
                            ERawannaDesk.ViewModels.RawwanResult classDetail = new ERawannaDesk.ViewModels.RawwanResult();
                            classDetail.status = ((JProperty)item).Value["status"].ToString().Replace("\"", "");
                            classDetail.eRawannaNo = ((JProperty)item).Value["eRawannaNo"].ToString().Replace("\"", "");
                            classDetail.availBal = ((JProperty)item).Value["availBal"].ToString().Replace("\"", "");
                            classDetail.Royaltyamount = ((JProperty)item).Value["rawannaCount"].ToString().Replace("\"", "");
                            classDetail.rawannaCount = ((JProperty)item).Value["rawannaCount"].ToString().Replace("\"", "");
                            classDetail.rawannaDate = ((JProperty)item).Value["rawannaDate"].ToString().Replace("\"", "");
                            classDetails.Add(classDetail);
                        }

                    }
                    return CreateDataTable(classDetails);

                }
                else
                {
                    MessageBox.Show(andPost, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DMG Responce Parsing Error");
                return null;
            }
        }
        public static async Task<DataTable> LeaseListdetail(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(SSL_leaseDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DMG Responce Parsing Error");
                return null;
            }
        }

        public static async Task<DataTable> dealerStockLoactionDetail(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(SSL_dealerStockDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DMG Responce Parsing Error");
                return null;
            }
        }
        public static async Task<DataTable> generateRawannaDetail(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(SSL_GenerateRawanna, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
                if (!jObject[0].ToString().Contains("300") && !jObject[0].ToString().Contains("301"))
                {
                    return JsonStringToDataTable(andPost);
                }
                else
                {
                    MessageBox.Show(andPost, "DMG Responce Parsing Error");
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DMG Responce Parsing Error");
                return null;
            }
        }

        public static async Task<DataTable> leaseListMineralDetail(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(SSL_leaseDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "DMG Responce Parsing Error");
                return null;
            }
        }

        public static async Task<DataTable> getsearchunconfirmrawanna(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            DataTable dtJsonresp = new DataTable();
            dtJsonresp.Columns.Add("consigneeName", typeof(string));
            dtJsonresp.Columns.Add("approxTotalAmount", typeof(string));
            dtJsonresp.Columns.Add("approxNMET", typeof(string));
            dtJsonresp.Columns.Add("ravannaDate", typeof(string));
            dtJsonresp.Columns.Add("status", typeof(string));
            dtJsonresp.Columns.Add("location", typeof(string));
            dtJsonresp.Columns.Add("TransactionType", typeof(string));
            dtJsonresp.Columns.Add("leaseID", typeof(string));
            dtJsonresp.Columns.Add("driverMobNo", typeof(string));
            dtJsonresp.Columns.Add("approxWeight", typeof(string));
            //dtJsonresp.Columns.Add("CollectionAgainst", typeof(string));
            dtJsonresp.Columns.Add("approxDMFT", typeof(string));
            dtJsonresp.Columns.Add("leaseNo", typeof(string));
            dtJsonresp.Columns.Add("vehicleName", typeof(string));
            dtJsonresp.Columns.Add("eRawannaNo", typeof(string));
            dtJsonresp.Columns.Add("approxRSMET", typeof(string));
            dtJsonresp.Columns.Add("transportMode", typeof(string));
            dtJsonresp.Columns.Add("approxRoyalty", typeof(string));
            dtJsonresp.Columns.Add("driverName", typeof(string));
            dtJsonresp.Columns.Add("vehicleNo", typeof(string));
            dtJsonresp.Columns.Add("approxTime", typeof(string));
            dtJsonresp.Columns.Add("mineralName", typeof(string));
            dtJsonresp.Columns.Add("VEHICLEWEIGHT", typeof(string));
            string andPost = await ParseAndPost(SSL_SearchRawannaDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
            if (!jObject[0].ToString().Contains("301"))
            {


                var jo = JObject.Parse(andPost);
                DataRow dr = dtJsonresp.NewRow();
                dr = dtJsonresp.NewRow();
                dr["consigneeName"] = jo["rawannaDetail"]["consigneeName"].ToString();
                dr["approxTotalAmount"] = jo["rawannaDetail"]["approxTotalAmount(in Rs.)"].ToString();
                dr["approxNMET"] = jo["rawannaDetail"]["approxNMET(in Rs.)"].ToString();
                dr["ravannaDate"] = jo["rawannaDetail"]["ravannaDate"].ToString();
                dr["status"] = jo["rawannaDetail"]["status"].ToString();
                dr["location"] = jo["rawannaDetail"]["location"].ToString();
                dr["TransactionType"] = jo["rawannaDetail"]["TransactionType"].ToString();
                dr["leaseID"] = jo["rawannaDetail"]["leaseID"].ToString();
                dr["driverMobNo"] = jo["rawannaDetail"]["driverMobNo"].ToString();
                dr["approxWeight"] = jo["rawannaDetail"]["approxWeight(in MT)"].ToString();
                //dr["CollectionAgainst"] = jo["rawannaDetail"]["CollectionAgainst"].ToString();
                dr["eRawannaNo"] = jo["rawannaDetail"]["eRawannaNo"].ToString();
                dr["approxRSMET"] = jo["rawannaDetail"]["approxRSMET(in Rs.)"].ToString();
                dr["transportMode"] = jo["rawannaDetail"]["transportMode"].ToString();
                dr["approxRoyalty"] = jo["rawannaDetail"]["approxRoyalty(in Rs.)"].ToString();
                dr["driverName"] = jo["rawannaDetail"]["driverName"].ToString();
                dr["vehicleNo"] = jo["rawannaDetail"]["vehicleNo"].ToString();
                dr["approxTime"] = jo["rawannaDetail"]["approxTime"].ToString();
                dr["mineralName"] = jo["rawannaDetail"]["mineralName"].ToString();
                dr["VEHICLEWEIGHT"] = jo["rawannaDetail"]["VEHICLEWEIGHT"].ToString();
                dtJsonresp.Rows.Add(dr);
                return dtJsonresp;
            }
            else
            {
                MessageBox.Show(andPost, "DMG Responce Parsing Error");
                return null;
            }

        }

        public static async Task<DataTable> getsearchTransitPass(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            DataTable dtJsonresp = new DataTable();
            dtJsonresp.Columns.Add("consigneeName", typeof(string));
            dtJsonresp.Columns.Add("ravannaDate", typeof(string));
            dtJsonresp.Columns.Add("status", typeof(string));
            dtJsonresp.Columns.Add("location", typeof(string));
            dtJsonresp.Columns.Add("approxRoyalty", typeof(string));
            dtJsonresp.Columns.Add("approxTotalAmount", typeof(string));
            dtJsonresp.Columns.Add("approxNMET", typeof(string));
            dtJsonresp.Columns.Add("TransactionType", typeof(string));
            dtJsonresp.Columns.Add("approxWeight", typeof(string));
            dtJsonresp.Columns.Add("leaseID", typeof(string));
            dtJsonresp.Columns.Add("driverMobNo", typeof(string));
            dtJsonresp.Columns.Add("leaseNo", typeof(string));
            dtJsonresp.Columns.Add("vehicleName", typeof(string));
            dtJsonresp.Columns.Add("eRawannaNo", typeof(string));
            dtJsonresp.Columns.Add("transportMode", typeof(string));
            dtJsonresp.Columns.Add("driverName", typeof(string));
            dtJsonresp.Columns.Add("vehicleNo", typeof(string));
            dtJsonresp.Columns.Add("approxTime", typeof(string));
            dtJsonresp.Columns.Add("approxDMFT", typeof(string));
            dtJsonresp.Columns.Add("mineralName", typeof(string));
            dtJsonresp.Columns.Add("VEHICLEWEIGHT", typeof(string));
            string andPost = await ParseAndPost(SSL_TPSearchDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
            if (!jObject[0].ToString().Contains("301"))
            {


                var jo = JObject.Parse(andPost);
                DataRow dr = dtJsonresp.NewRow();
                dr = dtJsonresp.NewRow();
                dr["consigneeName"] = jo["rawannaDetail"]["consigneeName"].ToString();
                dr["ravannaDate"] = jo["rawannaDetail"]["ravannaDate"].ToString();
                dr["status"] = jo["rawannaDetail"]["status"].ToString();
                dr["location"] = jo["rawannaDetail"]["location"].ToString();
                dr["approxRoyalty"] = jo["rawannaDetail"]["approxRoyalty"].ToString();
                dr["approxTotalAmount"] = jo["rawannaDetail"]["approxTotalAmount"].ToString();
                dr["approxWeight"] = jo["rawannaDetail"]["approxWeight"].ToString();
                dr["approxNMET"] = jo["rawannaDetail"]["approxNMET"].ToString();
                dr["TransactionType"] = jo["rawannaDetail"]["TransactionType"].ToString();
                dr["approxWeight"] = jo["rawannaDetail"]["approxWeight"].ToString();
                dr["leaseID"] = jo["rawannaDetail"]["leaseID"].ToString();
                dr["driverMobNo"] = jo["rawannaDetail"]["driverMobNo"].ToString();
                dr["leaseNo"] = jo["rawannaDetail"]["leaseNo"].ToString();
                dr["vehicleName"] = jo["rawannaDetail"]["vehicleName"].ToString();
                dr["eRawannaNo"] = jo["rawannaDetail"]["eRawannaNo"].ToString();
                dr["transportMode"] = jo["rawannaDetail"]["transportMode"].ToString();
                dr["driverName"] = jo["rawannaDetail"]["driverName"].ToString();
                dr["vehicleNo"] = jo["rawannaDetail"]["vehicleNo"].ToString();
                dr["approxTime"] = jo["rawannaDetail"]["approxTime"].ToString();
                dr["approxDMFT"] = jo["rawannaDetail"]["approxDMFT"].ToString();
                dr["mineralName"] = jo["rawannaDetail"]["mineralName"].ToString();
                dr["VEHICLEWEIGHT"] = jo["rawannaDetail"]["VEHICLEWEIGHT"].ToString();
                dtJsonresp.Rows.Add(dr);
                return dtJsonresp;
            }
            else
            {
                MessageBox.Show(andPost, "DMG Responce Parsing Error");
                return null;
            }

        }

        public static async Task<DataTable> generateTPassdetail(Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            DataTable dtJsonresp = new DataTable();
            string andPost = await ParseAndPostTP(SSL_TPGenerateConfirmTransitPass, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
            dtJsonresp.Columns.Add("TransitPassNumber", typeof(string));
            dtJsonresp.Columns.Add("MessageDiscription", typeof(string));
            if (!jObject[0].ToString().Contains("301"))
            {
                MessageBox.Show(andPost, "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                DataRow dr = dtJsonresp.NewRow();
                dr = dtJsonresp.NewRow();
                dr["TransitPassNumber"] = jObject[1].ToString().Replace("Data", "").Replace("{", "").Replace("}", "").Split(':')[2];
                dr["MessageDiscription"] = jObject[2].ToString().Replace("MessageDiscription", "").Replace("{", "").Replace("}", "").Split(':')[1];
                dtJsonresp.Rows.Add(dr);
                return dtJsonresp;

            }
            else
            {
                MessageBox.Show(andPost, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        public static async Task<string> ParseAndPost(string url, Dictionary<string, string> dit, string accessKey, string weighBridgeNo, bool HTMLDECOCE = false, bool dataWrapperNeeded = true)
        {
            JObject myObject = new JObject();
            JArray array123 = new JArray();
            _valueObject = dit;
            string frontValue;

            var contentData = DMGService.ConvertJson(dit);

            string str = await encryptAndPost(url, contentData, accessKey, HTMLDECOCE, weighBridgeNo);
            return str;
        }
        public static async Task<string> ParseAndPostTP(string url, Dictionary<string, string> dit, string accessKey, string weighBridgeNo, bool HTMLDECOCE = false, bool dataWrapperNeeded = true)
        {
            JObject myObject = new JObject();
            JArray array123 = new JArray();
            _valueObject = dit;
            string frontValue;

            var contentData = DMGService.ConvertJsonTP(dit);

            string str = await encryptAndPost(url, contentData, accessKey, HTMLDECOCE, weighBridgeNo);
            return str;
        }
        public static async Task<string> ParseAndPostwithoutdata(string url, Dictionary<string, string> dit, string accessKey, string weighBridgeNo, bool HTMLDECOCE = false, bool dataWrapperNeeded = true)
        {
            JObject myObject = new JObject();
            JArray array123 = new JArray();
            _valueObject = dit;
            //url = "http://103.203.138.51/services/v1.4/tp/allTpDetail";
            var contentData = DMGService.ConvertJsonwithoutdata(dit);
            //contentData= "{\"ssoId\":\"amitkumarmehra\",\"password\":\"mehra1992\"}";
            string str = await encryptAndPost(url, contentData, accessKey, HTMLDECOCE, weighBridgeNo);
            return str;
        }
        public static async Task<string> encryptAndPost(string url, string content_data, string accessKey, bool HTMLDECOCE, string weighBridgeNo)
        {
            JObject result = await DMGService.dmgservicesPost(url, content_data, accessKey, weighBridgeNo);
            if (!(result["ErrorMsg"].ToString() == "true"))
                return !HTMLDECOCE ? result["ShowResult"].ToString() : WebUtility.HtmlDecode(result["ShowResult"].ToString());
            //else
            //int num1 = (int)MessageBox.Show(result["ShowResult"].ToString(), "DMG Responce Parsing Error");
            //MessageBox.Show(result["ShowResult"].ToString().ToString(), "DMG Responce Parsing Error");

            return "";
        }
        public static async Task<string> post(string url, HttpContent content_data)
        {
            string str;
            using (HttpResponseMessage r = await client.PostAsync(new Uri(url), content_data))
            {
                string result = await r.Content.ReadAsStringAsync();
                str = result;
            }
            return str;
        }
        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static DataTable Tabulate(string json)
        {
            if (json == "")
                return new DataTable();
            Newtonsoft.Json.Linq.JObject jobject1 = Newtonsoft.Json.Linq.JObject.Parse(json);
            IEnumerable<JToken> source = jobject1.Descendants().Where<JToken>((Func<JToken, bool>)(d => d is JArray));
            JToken jtoken = source.Count<JToken>() != 0 ? source.First<JToken>() : throw new Exception(jobject1.ToString());
            JArray jarray = new JArray();
            foreach (Newtonsoft.Json.Linq.JObject child in jtoken.Children<Newtonsoft.Json.Linq.JObject>())
            {
                Newtonsoft.Json.Linq.JObject jobject2 = new Newtonsoft.Json.Linq.JObject();
                foreach (JProperty property in child.Properties())
                {
                    if (property.Value is Newtonsoft.Json.Linq.JValue)
                        jobject2.Add(property.Name, property.Value);
                }
                jarray.Add((JToken)jobject2);
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(jarray.ToString());
        }

    }
}

using minesmart.DGMS;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesmart.Helper
{
    public class Cls_PostMines
    {
        //public static string applicationPath = Application.StartupPath;
        //public static string LogFolderPath = applicationPath + "\\RawannaLog";
        //private static SerialPortManager _spManager = new SerialPortManager();
        public static String ErrorFilePath = @"\TpJson.json";
        public static System.IO.StreamWriter objWriter;
        public static string applicationPath = System.Windows.Forms.Application.StartupPath;
        public static string LogFolderPath = applicationPath + ErrorFilePath;
        private static Dictionary<string, string> _valueObject;
        private static HttpClient client = new HttpClient();
        public static string appVersion = "15";
        public static string IN_AWSURL = "https://9bazujuivi.execute-api.ap-south-1.amazonaws.com/prod/erawanna";
        public static string AWSURL = "https://l84ber8h7f.execute-api.us-east-2.amazonaws.com/prod/licenceValidate";
        public static string AWSAction = "dmgpass";

        public static async Task<DataTable> getconsigneeDetail(string LocalUrl, string Url, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            DataTable dtJsonresp = new DataTable();
            //string andPost = await ParseAndPost(LocalUrl, Url, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            dtJsonresp.Columns.Add("Status", typeof(string));
            dtJsonresp.Columns.Add("MessageDiscription", typeof(string));
            string andPost = await ParseAndPost(LocalUrl, Url, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
            if (!jObject[0].ToString().Contains("301"))
            {
                return JsonStringToDataTable(andPost);
            }
            else
            {
                var jo = JObject.Parse(andPost);
                var Message = jo["MessageDiscription"].ToString();
                var Status = jo["Status"].ToString();
                DataRow dr = dtJsonresp.NewRow();
                dr = dtJsonresp.NewRow();
                dr["MessageDiscription"] = Message.ToString();
                dr["Status"] = Status.ToString();
                dtJsonresp.Rows.Add(dr);
                return dtJsonresp;
            }

        }
        public static async Task<DataTable> getvehicleDetail(string LocalUrl, string Url_VehicleInfo, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(LocalUrl, Url_VehicleInfo, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), " DMG Response");
                return null;
            }
        }
        #region Transit Pass
        public static async Task<DataTable> getvehicleDetailTP(string LocalUrl, string Url_VehicleInfo, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(LocalUrl, Url_VehicleInfo, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }
        public static async Task<DataTable> getstockLocation(string LocalUrl, string Url_TPStockLocation, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPostTP(LocalUrl, Url_TPStockLocation, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }
        public static async Task<DataTable> TPdealerDetails(string LocalUrl, string Url_TPallInfo, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {

            string andPost = string.Empty;
            DataTable dtJsonresp = new DataTable();
            try
            {
                dtJsonresp.Columns.Add("ID", typeof(Int32));
                dtJsonresp.Columns.Add("Name", typeof(string));
                dtJsonresp.Columns.Add("OtherId", typeof(string));
                dtJsonresp.Columns.Add("FirmName", typeof(string));
                dtJsonresp.Columns.Add("Type", typeof(string));
                andPost = await ParseAndPost(LocalUrl, Url_TPallInfo, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var Items = jss.Deserialize<AllTP.JSONClass>(andPost);
                if (Items != null)
                {
                    if (Items.DealerList.Count() > 0)
                    {
                        DataRow dr = dtJsonresp.NewRow();
                        foreach (var str in Items.DealerList)
                        {
                            dr = dtJsonresp.NewRow();
                            dr["ID"] = str.dealerId;
                            dr["Name"] = str.dealerName;
                            dr["FirmName"] = str.firmName;
                            dr["Type"] = "dealer";
                            dtJsonresp.Rows.Add(dr);
                            foreach (var str3 in str.locationList)
                            {

                                dr = dtJsonresp.NewRow();
                                dr["ID"] = str3.stockLocId;
                                dr["Name"] = str3.stockLocName;
                                dr["OtherId"] = str.dealerId;
                                dr["Type"] = "stockList";
                                dtJsonresp.Rows.Add(dr);

                                foreach (var str4 in str3.mineralList)
                                {

                                    dr = dtJsonresp.NewRow();
                                    dr["ID"] = str4.mineralId;
                                    dr["Name"] = str4.mineralName;
                                    dr["OtherId"] = str3.stockLocId;
                                    dr["Type"] = "mineralList";
                                    dtJsonresp.Rows.Add(dr);
                                }
                            }
                            foreach (var str1 in str.consigneeList)
                            {
                                dr = dtJsonresp.NewRow();
                                dr["ID"] = str1.consigneeId;
                                dr["Name"] = str1.consigneeName;
                                dr["OtherId"] = str.dealerId;
                                dr["Type"] = "consignee";
                                dtJsonresp.Rows.Add(dr);
                                foreach (var str4 in str1.addressList)
                                {

                                    dr = dtJsonresp.NewRow();
                                    dr["ID"] = str4.addressId;
                                    dr["Name"] = str4.fullAddress;
                                    dr["OtherId"] = str1.consigneeId;
                                    dr["Type"] = "addressList";
                                    dtJsonresp.Rows.Add(dr);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
            //List<Dictionary<string, string>> deserializedProduct = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(andPost);


            //var rootOfList = JsonConvert.DeserializeObject<RootObject<List<minesmart.ViewModels.AllTP.JSONClass>>>(andPost);
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

        public static async Task<DataTable> getTPMineralDetail(string LocalUrl, string Url_TPMineralDetail, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            string andPost = await ParseAndPostTP(LocalUrl, Url_TPMineralDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            return Tabulate(andPost);
        }
        public static async Task<DataTable> getTPConsigneeDetails(string LocalUrl, string Url_TPConsigneeDetail, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            DataTable dtJsonresp = new DataTable();
            dtJsonresp.Columns.Add("MessageDiscription", typeof(string));
            dtJsonresp.Columns.Add("Status", typeof(string));
            string andPost = await ParseAndPostTP(LocalUrl, Url_TPConsigneeDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
            if (!jObject[0].ToString().Contains("301"))
            {
                return Tabulate(andPost);
            }
            else
            {
                var jo = JObject.Parse(andPost);
                var Message = jo["MessageDiscription"].ToString();
                var Status = jo["Status"].ToString();
                DataRow dr = dtJsonresp.NewRow();
                dr = dtJsonresp.NewRow();
                dr["MessageDiscription"] = Message.ToString();
                dr["Status"] = Status.ToString();
                dtJsonresp.Rows.Add(dr);
                return dtJsonresp;
            }
        }
        public static async Task<DataTable> TPgenerateTransitPass(string LocalUrl, string Url_TPTranistPass, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            DataTable dtJsonresp = new DataTable();
            dtJsonresp.Columns.Add("MessageDiscription", typeof(string));
            dtJsonresp.Columns.Add("Status", typeof(string));
            string andPost = await ParseAndPost(LocalUrl, Url_TPTranistPass, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
            if (!jObject[0].ToString().Contains("300") && !jObject[0].ToString().Contains("301"))
            {
                return JsonStringToDataTable(andPost);
            }
            else
            {
                var jo = JObject.Parse(andPost);
                var Message = jo["MessageDiscription"].ToString();
                var Status = jo["Status"].ToString();
                DataRow dr = dtJsonresp.NewRow();
                dr = dtJsonresp.NewRow();
                dr["MessageDiscription"] = Message.ToString();
                dr["Status"] = Status.ToString();
                dtJsonresp.Rows.Add(dr);
                return dtJsonresp;
            }
        }
        public static async Task<DataTable> TPsearchconsigneedtl(string LocalUrl, string Url_TPTranistPass, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            DataTable dtJsonresp = new DataTable();
            dtJsonresp.Columns.Add("MessageDiscription", typeof(string));
            dtJsonresp.Columns.Add("Status", typeof(string));
            string andPost = await ParseAndPostTP(LocalUrl, Url_TPTranistPass, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
            var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
            if (!jObject[0].ToString().Contains("300") && !jObject[0].ToString().Contains("301"))
            {
                return JsonStringToDataTable(andPost);
            }
            else
            {
                var jo = JObject.Parse(andPost);
                var Message = jo["MessageDiscription"].ToString();
                var Status = jo["Status"].ToString();
                DataRow dr = dtJsonresp.NewRow();
                dr = dtJsonresp.NewRow();
                dr["MessageDiscription"] = Message.ToString();
                dr["Status"] = Status.ToString();
                dtJsonresp.Rows.Add(dr);
                return dtJsonresp;
            }
        }

        #endregion
        public static async Task<DataTable> getconsigneeAddressDetail(string LocalUrl, string Url_GetconsigneeAddressDetails, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                DataTable dtJsonresp = new DataTable();
                dtJsonresp.Columns.Add("MessageDiscription", typeof(string));
                dtJsonresp.Columns.Add("Status", typeof(string));
                string andPost = await ParseAndPost(LocalUrl, Url_GetconsigneeAddressDetails, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
                if (!jObject[0].ToString().Contains("300") && !jObject[0].ToString().Contains("301"))
                {
                    return JsonStringToDataTable(andPost);
                }
                else
                {
                    var jo = JObject.Parse(andPost);
                    var Message = jo["MessageDiscription"].ToString();
                    var Status = jo["Status"].ToString();
                    DataRow dr = dtJsonresp.NewRow();
                    dr = dtJsonresp.NewRow();
                    dr["MessageDiscription"] = Message.ToString();
                    dr["Status"] = Status.ToString();
                    dtJsonresp.Rows.Add(dr);
                    return dtJsonresp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }
        public static async Task<DataTable> getMineralDetail(string LocalUrl, string url_MineralList, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(LocalUrl, url_MineralList, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }
        public static async Task<DataTable> generateconfirmrawannadetail(string LocalUrl, string Url_Generateconfirmrawanna, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                DataTable dtJsonresp = new DataTable();
                dtJsonresp.Columns.Add("Status", typeof(string));
                dtJsonresp.Columns.Add("MessageDiscription", typeof(string));
                string andPost = await ParseAndPost(LocalUrl, Url_Generateconfirmrawanna, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
                if (!jObject[0].ToString().Contains("301"))
                {
                    //MessageBox.Show(andPost, "Error", MessageBoxButtons.OK, MessageBoxIcon.None);
                    int i = 0;
                    List<RawwanResult> classDetails = new List<RawwanResult>();
                    foreach (var item in jObject)
                    {
                        if (((Newtonsoft.Json.Linq.JProperty)item).Name != "Status")
                        {
                            RawwanResult classDetail = new RawwanResult();
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
                    var jo = JObject.Parse(andPost);
                    var Message = jo["MessageDiscription"].ToString();
                    var Status = jo["Status"].ToString();
                    DataRow dr = dtJsonresp.NewRow();
                    dr = dtJsonresp.NewRow();
                    dr["MessageDiscription"] = Message.ToString();
                    dr["Status"] = Status.ToString();
                    dtJsonresp.Rows.Add(dr);
                    return dtJsonresp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }
        public static async Task<DataTable> LeaseListdetail(string LocalUrl, string Url_leaseDetail, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(LocalUrl, Url_leaseDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }

        public static async Task<DataTable> dealerStockLoactionDetail(string LocalUrl, string Url_dealerStockDetail, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(LocalUrl, Url_dealerStockDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }
        public static async Task<DataTable> generateRawannaDetail(string LocalUrl, string Url_GenerateRawanna, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                DataTable dtJsonresp = new DataTable();
                dtJsonresp.Columns.Add("Status", typeof(string));
                dtJsonresp.Columns.Add("MessageDiscription", typeof(string));
                string andPost = await ParseAndPost(LocalUrl, Url_GenerateRawanna, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
                if (!jObject[0].ToString().Contains("300") && !jObject[0].ToString().Contains("301"))
                {
                    return JsonStringToDataTable(andPost);
                }
                else
                {
                    var jo = JObject.Parse(andPost);
                    var Message = jo["MessageDiscription"].ToString();
                    var Status = jo["Status"].ToString();
                    DataRow dr = dtJsonresp.NewRow();
                    dr = dtJsonresp.NewRow();
                    dr["MessageDiscription"] = Message.ToString();
                    dr["Status"] = Status.ToString();
                    dtJsonresp.Rows.Add(dr);
                    return dtJsonresp;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }

        public static async Task<DataTable> leaseListMineralDetail(string LocalUrl, string Url_leaseDetail, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                string andPost = await ParseAndPost(LocalUrl, Url_leaseDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                return Tabulate(andPost);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }

        public static async Task<DataTable> getsearchunconfirmrawanna(string LocalUrl, string Url_SearchRawannaDetail, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
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
                string andPost = await ParseAndPost(LocalUrl, Url_SearchRawannaDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
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
                    var jo = JObject.Parse(andPost);
                    var Message = jo["MessageDiscription"].ToString();
                    MessageBox.Show(Message, " DMG Response");
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }

        public static async Task<DataTable> getsearchTransitPass(string LocalUrl, string Url_TPSearchDetail, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                DataTable dtJsonresp1 = new DataTable();
                dtJsonresp1.Columns.Add("Status", typeof(string));
                dtJsonresp1.Columns.Add("MessageDiscription", typeof(string));

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
                string andPost = await ParseAndPost(LocalUrl, Url_TPSearchDetail, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();
                if (!jObject[0].ToString().Contains("301") && !jObject[0].ToString().Contains("300"))
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
                    var jo = JObject.Parse(andPost);
                    var Message = jo["MessageDiscription"].ToString();
                    var Status = jo["Status"].ToString();
                    DataRow dr = dtJsonresp.NewRow();
                    dr = dtJsonresp1.NewRow();
                    dr["MessageDiscription"] = Message.ToString();
                    dr["Status"] = Status.ToString();
                    dtJsonresp1.Rows.Add(dr);
                    return dtJsonresp1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }

        public static async Task<DataTable> generateTPassdetail(string LocalUrl, string Url_TPGenerateConfirmTransitPass, Dictionary<string, string> dit, string accessKey, string weighBridgeNo)
        {
            try
            {
                DataTable dtJsonresp = new DataTable();
                dtJsonresp.Columns.Add("TransitPassNumber", typeof(string));
                dtJsonresp.Columns.Add("MessageDiscription", typeof(string));
                dtJsonresp.Columns.Add("Status", typeof(string));
                string andPost = await ParseAndPostTP(LocalUrl, Url_TPGenerateConfirmTransitPass, dit, accessKey, weighBridgeNo, dataWrapperNeeded: false);
                var jObject = JsonConvert.DeserializeObject<JObject>(andPost).Root.ToList();

                if (!jObject[0].ToString().Contains("301"))
                {
                    // MessageBox.Show(andPost, "Success", MessageBoxButtons.OK, MessageBoxIcon.None);
                    DataRow dr = dtJsonresp.NewRow();
                    dr = dtJsonresp.NewRow();
                    dr["TransitPassNumber"] = jObject[1].ToString().Replace("Data", "").Replace("{", "").Replace("}", "").Split(':')[2];
                    dr["MessageDiscription"] = jObject[2].ToString().Replace("MessageDiscription", "").Replace("{", "").Replace("}", "").Split(':')[1];
                    dtJsonresp.Rows.Add(dr);
                    return dtJsonresp;

                }
                else
                {
                    var jo = JObject.Parse(andPost);
                    var Message = jo["MessageDiscription"].ToString();
                    var Status = jo["Status"].ToString();
                    DataRow dr = dtJsonresp.NewRow();
                    dr = dtJsonresp.NewRow();
                    dr["MessageDiscription"] = Message.ToString();
                    dr["Status"] = Status.ToString();
                    dtJsonresp.Rows.Add(dr);
                    return dtJsonresp;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message.ToString(), " DMG Response");
                return null;
            }
        }
        public static async Task<string> ParseAndPost(string Posturl, string url, Dictionary<string, string> dit, string accessKey, string weighBridgeNo, bool HTMLDECOCE = false, bool dataWrapperNeeded = true)
        {
            JObject myObject = new JObject();
            JArray array123 = new JArray();
            _valueObject = dit;
            string frontValue;

            var contentData = minesmart.DGM.DMGService.ConvertJson(dit);

            string str = await encryptAndPost(Posturl, url, contentData, accessKey, HTMLDECOCE, weighBridgeNo);
            return str;
        }
        public static async Task<string> ParseAndPostTP(string Posturl, string url, Dictionary<string, string> dit, string accessKey, string weighBridgeNo, bool HTMLDECOCE = false, bool dataWrapperNeeded = true)
        {
            JObject myObject = new JObject();
            JArray array123 = new JArray();
            _valueObject = dit;
            string frontValue;

            var contentData = minesmart.DGM.DMGService.ConvertJsonTP(dit);

            string str = await encryptAndPost(Posturl, url, contentData, accessKey, HTMLDECOCE, weighBridgeNo);
            return str;
        }

        public static async Task<string> encryptAndPost(string Posturl, string url, string content_data, string accessKey, bool HTMLDECOCE, string weighBridgeNo)
        {
            JObject result = await minesmart.DGM.DMGService.dmgservicesPost(Posturl, url, content_data, accessKey, weighBridgeNo);
            if (!(result["ErrorMsg"].ToString() == "true"))
                return !HTMLDECOCE ? result["ShowResult"].ToString() : WebUtility.HtmlDecode(result["ShowResult"].ToString());
            //else
            //int num1 = (int)MessageBox.Show(result["ShowResult"].ToString(), " DMG Response");
            //MessageBox.Show(result["ShowResult"].ToString().ToString(), " DMG Response");

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

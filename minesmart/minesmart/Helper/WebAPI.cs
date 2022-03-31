using minesmart.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Nancy.Json;
using System.IO;
using minesmart.DGMS;

namespace ERawaana.Helper
{
    public static class WebAPI
    {
        static string API_URIs = minesmart.Properties.Settings.Default.Connection_String;

        static WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
        static JavaScriptSerializer jss = new JavaScriptSerializer();
        static DataTable dtJsonMessage = new DataTable();
        static string Onlinewebsiteurl = "http://dmg.minesmart.in/";
       //static string Onlinewebsiteurl = "http://localhost:49997/";


        #region Checking User Name and Password Valid
        public static async Task<DataTable> PostCheckUserPasswordExist(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            DataTable dtrecord = new DataTable();

            try
            {
                dtJsonMessage = new DataTable();
                dtJsonMessage.Columns.Add("Status", typeof(string));
                dtJsonMessage.Columns.Add("MessageDiscription", typeof(string));
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        #endregion
        #region Checking  Mach Address Exist
        public static async Task<DataTable> PostCheckApiWorking(string API_URIs, Login loginobj)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                dtJsonMessage = new DataTable();
                dtJsonMessage.Columns.Add("Status", typeof(string));
                dtJsonMessage.Columns.Add("MessageDiscription", typeof(string));
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        #endregion

        #region Checking  Mach Address Exist
        public static async Task<DataTable> PostCheckMachAddressExist(string API_URIs, Login loginobj)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                dtJsonMessage = new DataTable();
                dtJsonMessage.Columns.Add("Status", typeof(string));
                dtJsonMessage.Columns.Add("MessageDiscription", typeof(string));
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        #endregion
        #region Checking User Name Valid
        public static async Task<DataTable> PostCheckUserNameExist(string API_URIs, Login loginobj)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                dtJsonMessage = new DataTable();
                dtJsonMessage.Columns.Add("Status", typeof(string));
                dtJsonMessage.Columns.Add("MessageDiscription", typeof(string));
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.   
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {

                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);


                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        #endregion

        #region Checking Password Valid
        public static async Task<DataTable> PostCheckPasswordExist(string API_URIs, Login loginobj)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();

            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                dtJsonMessage = new DataTable();
                dtJsonMessage.Columns.Add("Status", typeof(string));
                dtJsonMessage.Columns.Add("MessageDiscription", typeof(string));
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();
                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {

                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);

                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        #endregion

        #region Checking User Name and Password Allow to desktop or not
        public static async Task<WebAPIModelResponse> PostAllowdesktop(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.   
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            WebAPIModelResponse.SsoId = json.data[0].SsoId;
                            WebAPIModelResponse.Password = json.data[0].Password;
                            WebAPIModelResponse.Weightbridgeno = json.data[0].WeighBridgeNo;
                            WebAPIModelResponse.Accesskey = json.data[0].Accesskey;
                            WebAPIModelResponse.ActiveKey = json.data[0].ActiveKey;
                            WebAPIModelResponse.Userid = json.data[0].UserId;
                            WebAPIModelResponse.CompanyId = json.data[0].CompanyId;
                            WebAPIModelResponse.ClientId = json.data[0].ClientId;
                            WebAPIModelResponse.Provider = json.data[0].Provider;
                        }
                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }
        #endregion

        #region Checking User supcription plan expired or not
        public static async Task<DataTable> PostCheckUserExpired(string API_URIs, Login loginobj)
        {

            // Initialization.
            DataTable dtrecord = new DataTable();
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                dtJsonMessage = new DataTable();
                dtJsonMessage.Columns.Add("Status", typeof(string));
                dtJsonMessage.Columns.Add("MessageDiscription", typeof(string));
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {

                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);

                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else if (Status == "205")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        #endregion





        public static async Task<WebAPIModelResponse> PostRegInfo(string API_URIs, WebAPIModel requestObj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                //request.PreAuthenticate = true;
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);

                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<WebAPIModelResponse> PostRegSSo(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json.data != null)
                        {
                            WebAPIModelResponse.SsoId = json.data[0].SOOID;
                            WebAPIModelResponse.Password = json.data[0].PASSWORD;
                            WebAPIModelResponse.Weightbridgeno = json.data[0].WEIGHBRIDGENO;
                            WebAPIModelResponse.Accesskey = json.data[0].ACCESSKEY;
                            WebAPIModelResponse.ActiveKey = json.data[0].ACTIVEKEY;
                            WebAPIModelResponse.Userid = json.data[0].USERID;
                            WebAPIModelResponse.CompanyId = json.data[0].COMPANYID;
                        }
                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<WebAPIModelResponse> PostGetRegSSo(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);

                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<WebAPIModelResponse> setRegssoid(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);

                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<ConsigneeDetailsModel> PostconsigneeDetail(string API_URIs, ConsigneeDetailsModel requestObj)
        {
            // Initialization.  

            ConsigneeDetailsModel Objconsigneemodel = new ConsigneeDetailsModel();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        Objconsigneemodel = JsonConvert.DeserializeObject<ConsigneeDetailsModel>(result);

                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        Objconsigneemodel.ErrorCode = "602";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Objconsigneemodel;
        }

        public static async Task<ConsigneeDetailsModel> PostLeaselistMineral(string API_URIs, ConsigneeDetailsModel requestObj)
        {
            // Initialization.  

            ConsigneeDetailsModel Objconsigneemodel = new ConsigneeDetailsModel();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    //response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        Objconsigneemodel = JsonConvert.DeserializeObject<ConsigneeDetailsModel>(result);

                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        Objconsigneemodel.ErrorCode = "602";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Objconsigneemodel;
        }

        public static async Task<ConfirmERawannaModel> PostinsertConfirmRawanna(string API_URIs, ConfirmERawannaModel requestObj)
        {
            // Initialization.  

            ConfirmERawannaModel ObjconfirmerawannaModel = new ConfirmERawannaModel();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        ObjconfirmerawannaModel = JsonConvert.DeserializeObject<ConfirmERawannaModel>(result);

                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        ObjconfirmerawannaModel.ErrorCode = "602";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ObjconfirmerawannaModel;
        }

        public static async Task<ConsigneeDetailsModel> Postconsigneeaddress(string API_URIs, ConsigneeDetailsModel requestObj)
        {
            // Initialization.  

            ConsigneeDetailsModel Objconsigneemodel = new ConsigneeDetailsModel();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        Objconsigneemodel = JsonConvert.DeserializeObject<ConsigneeDetailsModel>(result);

                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        Objconsigneemodel.ErrorCode = "602";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Objconsigneemodel;
        }

        public static async Task<WebAPIModelResponse> PostleaseListMineralDetail(string API_URIs, LeaseListModel leaseListmdl)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(leaseListmdl);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);

                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<WebAPIModelResponse> PostdealerInfoDetail(string API_URIs, ConsigneeDetailsModel Dealermdl)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(Dealermdl);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);

                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<DataTable> PostSettingInfoDetail(string API_URIs, SettingModel _settingModal)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(_settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {

                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);

                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostGetlogProileDetails(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {

                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);

                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }


        public static async Task<WebAPIModelResponse> PostCheckUserDetailsExist(string API_URIs, SettingModel _settingModal)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(_settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            WebAPIModelResponse.SsoId = json.data[0].SsoId;
                            WebAPIModelResponse.Password = json.data[0].Password;
                            WebAPIModelResponse.Weightbridgeno = json.data[0].WeighBridgeNo;
                            WebAPIModelResponse.Accesskey = json.data[0].AccessKey;
                            WebAPIModelResponse.ActiveKey = json.data[0].ActiveKey;
                            WebAPIModelResponse.Userid = json.data[0].UserId;
                            WebAPIModelResponse.CompanyId = json.data[0].CompanyId;
                            WebAPIModelResponse.ClientId = json.data[0].ClientId;
                            WebAPIModelResponse.SsoId = json.data[0].SsoId;
                            WebAPIModelResponse.Provider = json.data[0].Provider;
                        }

                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }
        public static async Task<WebAPIModelResponse> Postsubcribeplan(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            WebAPIModelResponse.SsoId = json.data[0].SsoId;
                            WebAPIModelResponse.Password = json.data[0].Password;
                            WebAPIModelResponse.Weightbridgeno = json.data[0].WeighBridgeNo;
                            WebAPIModelResponse.Accesskey = json.data[0].AccessKey;
                            WebAPIModelResponse.ActiveKey = json.data[0].ActiveKey;
                            WebAPIModelResponse.Userid = json.data[0].UserId;
                            WebAPIModelResponse.CompanyId = json.data[0].CompanyId;
                            WebAPIModelResponse.ClientId = json.data[0].ClientId;
                            WebAPIModelResponse.SsoId = json.data[0].SsoId;
                            WebAPIModelResponse.Provider = json.data[0].Provider;
                        }
                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }
        public static async Task<DataTable> Postsetsubcribeplan(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostSerialPortSetting(string API_URIs, SettingModel settingModalobj)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModalobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                    // Verification  

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        public static async Task<SettingModel> Postweightbridgesetting(string API_URIs, SettingModel _settingModal)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(_settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    client.BaseAddress = new Uri("http://minesmart.in/");

                    // Setting content type.  
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        setresp.Success = json.success;
                        setresp.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            setresp.SsoId = json.data[0].SsoId;
                            setresp.CompanyId = json.data[0].CLientId;
                            setresp.ClientId = json.data[0].CLientId;
                            setresp.RatesBaud = json.data[0].BAUDRATE;
                            setresp.DataBits = json.data[0].DATABITS;
                            setresp.StopBitsNew = json.data[0].StopBitssNew;
                            setresp.StopBits = json.data[0].STOPBITS;
                            setresp.ReaderCode = json.data[0].READERCODE;
                            setresp.SytemType = json.data[0].SYTEMTYPE;
                            setresp.IsReversed = json.data[0].ISREVERSED;
                            setresp.ParityNew = json.data[0].ParityNew;
                            setresp.Parity = json.data[0].PARITY;
                            setresp.DeviceMode = json.data[0].DeviceMake;
                            setresp.DeviceType = json.data[0].DeviceType;
                            setresp.CameraFrontUrl = json.data[0].CameraFrontUrl;
                            setresp.CameraRearUrl = json.data[0].CameraRearUrl;
                            setresp.CameraIPAddress = json.data[0].CameraIPaddress;
                            setresp.CameraUserName = json.data[0].Camerausername;
                            setresp.CameraUserPassword = json.data[0].CameraPassword;
                            setresp.WBEmailId = json.data[0].WBEmailId;
                            setresp.WBBridgeNumber = json.data[0].WBBridgeNumber;
                            setresp.WBAddress = json.data[0].WBAddress;
                            setresp.WBMobileNumber = json.data[0].WBMobileNumber;
                            setresp.WBCompanyName = json.data[0].WBCompanyName;
                            setresp.PortName = json.data[0].PORTNAME;
                        }

                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        setresp = JsonConvert.DeserializeObject<SettingModel>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return setresp;
        }
        public static async Task<SettingModel> PostPortSetting(string API_URIs, SettingModel settingModalobj)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModalobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    client.BaseAddress = new Uri("http://dmg.minesmart.in/");

                    // Setting content type.  
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        setresp.Success = json.success;
                        setresp.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            setresp.SsoId = json.data[0].SsoId;
                            setresp.CompanyId = json.data[0].CompanyId;
                            setresp.PortName = json.data[0].PortName;
                            setresp.RatesBaud = json.data[0].BaudRate;
                            setresp.DataBits = json.data[0].DataBits;
                            setresp.Parity = json.data[0].ParityNew;
                            setresp.StopBits = json.data[0].StopBitssNew;
                            setresp.ReaderCode = json.data[0].ReaderCode;
                            setresp.SytemType = json.data[0].SytemType;
                            setresp.IsReversed = json.data[0].IsReversed;
                            setresp.CameraFrontUrl = json.data[0].CameraFrontUrl;
                            setresp.CameraRearUrl = json.data[0].CameraRearUrl;
                            setresp.CameraIPAddress = json.data[0].CameraIPaddress;
                            setresp.CameraUserName = json.data[0].Camerausername;
                            setresp.CameraUserPassword = json.data[0].CameraPassword;
                            setresp.WBEmailId = json.data[0].WBEmailId;
                            setresp.WBBridgeNumber = json.data[0].WBBridgeNumber;
                            setresp.WBAddress = json.data[0].WBAddress;
                            setresp.WBMobileNumber = json.data[0].WBMobileNumber;
                            setresp.WBCompanyName = json.data[0].WBCompanyName;
                        }
                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return setresp;
        }
        //public static async Task<DataTable> PostPortSetting(string API_URIs, SettingModel settingModalobj)
        //{
        //    // Initialization.  
        //    SettingModel setresp = new SettingModel();
        //    DataTable dtrecord = new DataTable();
        //    try
        //    {
        //        var dataAsString = JsonConvert.SerializeObject(settingModalobj);
        //        var content = new StringContent(dataAsString);
        //        // Posting.  
        //        using (var client = new HttpClient())
        //        {
        //            // Setting Base address.  
        //            client.BaseAddress = new Uri("http://dmg.minesmart.in/");

        //            // Setting content type.  
        //            client.DefaultRequestHeaders.Accept.Clear();
        //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //            // Setting timeout.  
        //            client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

        //            // Initialization.  
        //            HttpResponseMessage response = new HttpResponseMessage();

        //            // HTTP POST  
        //            response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                string result = response.Content.ReadAsStringAsync().Result;
        //                dynamic json = JsonConvert.DeserializeObject(result);
        //                var jo = JObject.Parse(result);
        //                var Status = jo["success"].ToString();
        //                var Message = jo["message"].ToString();
        //                if (Status == "200")
        //                {
        //                    dtrecord = JsonToDataTable(result);
        //                    System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
        //                    newColumn.DefaultValue = "200";
        //                    dtrecord.Columns.Add(newColumn);
        //                }
        //                else if (Status == "210")
        //                {
        //                    DataRow dr = dtJsonMessage.NewRow();
        //                    dr = dtJsonMessage.NewRow();
        //                    dr["MessageDiscription"] = Message.ToString();
        //                    dr["Status"] = Status.ToString();
        //                    dtJsonMessage.Rows.Add(dr);
        //                    return dtJsonMessage;
        //                }
        //                else
        //                {
        //                    DataRow dr = dtJsonMessage.NewRow();
        //                    dr = dtJsonMessage.NewRow();
        //                    dr["MessageDiscription"] = Message.ToString();
        //                    dr["Status"] = Status.ToString();
        //                    dtJsonMessage.Rows.Add(dr);
        //                    return dtJsonMessage;

        //                }

        //            }
        //            else
        //            {
        //                DataRow dr = dtJsonMessage.NewRow();
        //                dr = dtJsonMessage.NewRow();
        //                dr["MessageDiscription"] = "Some Error Occured";
        //                dr["Status"] = "212";
        //                dtJsonMessage.Rows.Add(dr);
        //                return dtJsonMessage;
        //            }
        //            // Verification  

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return dtrecord;
        //}

        public static async Task<DataTable> PostCompanySetting(string API_URIs, SettingModel settingModalobj)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModalobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                    // Verification  

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostCompanydetailsetting(string API_URIs, SettingModel _settingModal)
        {
            // Initialization.  
            SettingModel webAPIresp = new SettingModel();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(_settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostGetsettingcamera(string API_URIs, SettingModel _settingModal)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(_settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }


        public static async Task<SettingModel> PostCameraSetting(string API_URIs, SettingModel _settingModal)
        {
            // Initialization.  
            SettingModel webAPIresp = new SettingModel();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(_settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<SettingModel>(result);
                        dynamic json = JsonConvert.DeserializeObject(result);
                        webAPIresp.Success = json.Status;
                        webAPIresp.Message = json.Status;
                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<SettingModel>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<SettingModel> PostSetWeightSlipSetting(string API_URIs, SettingModel _settingModal)
        {
            // Initialization.  
            SettingModel webAPIresp = new SettingModel();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(_settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    byte[] frontImage = Convert.FromBase64String(_settingModal.CameraFrontUrl);

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();
                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        webAPIresp.Success = json.Status;
                        webAPIresp.Message = json.ErrorCode;

                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<SettingModel>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }
        public static async Task<SettingModel> PostSetTempWeightSlipSetting(string API_URIs, SettingModel _settingModal)
        {
            // Initialization.  
            SettingModel webAPIresp = new SettingModel();

            try
            {
                var dataAsString = JsonConvert.SerializeObject(_settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    byte[] frontImage = Convert.FromBase64String(_settingModal.CameraFrontUrl);
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();
                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        webAPIresp.Success = json.Status;
                        webAPIresp.Message = json.ErrorCode;

                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        webAPIresp = JsonConvert.DeserializeObject<SettingModel>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }


        public static async Task<WebAPIModelResponse> Postchecksssoidexist(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json.data != null)
                        {
                            WebAPIModelResponse.SsoId = json.data[0].SsoId;
                            WebAPIModelResponse.Password = json.data[0].Password;
                            WebAPIModelResponse.Weightbridgeno = json.data[0].WeighBridgeNo;
                            WebAPIModelResponse.Accesskey = json.data[0].Accesskey;
                            WebAPIModelResponse.ActiveKey = json.data[0].ActiveKey;
                            WebAPIModelResponse.Userid = json.data[0].UserId;
                            WebAPIModelResponse.ClientId = json.data[0].ClientId;
                            WebAPIModelResponse.CompanyId = json.data[0].CompanyId;
                            WebAPIModelResponse.Provider = json.data[0].Provider;
                        }
                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<WebAPIModelResponse> Postcheckdealerinfo(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json.data > 0)
                        {
                            WebAPIModelResponse.Countresult = json.data[0].COUNTRESULT;
                        }
                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }




        public static async Task<DataTable> Postcheckalreadyuserlogin(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else if (Status == "206")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<WebAPIModelResponse> PostCheckUserLogout(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            WebAPIModelResponse.SsoId = json.data[0].SsoId;
                            WebAPIModelResponse.Password = json.data[0].Password;
                            WebAPIModelResponse.Weightbridgeno = json.data[0].WeighBridgeNo;
                            WebAPIModelResponse.Accesskey = json.data[0].Accesskey;
                            WebAPIModelResponse.ActiveKey = json.data[0].ActiveKey;
                            WebAPIModelResponse.Userid = json.data[0].UserId;
                            WebAPIModelResponse.CompanyId = json.data[0].CompanyId;
                            WebAPIModelResponse.Provider = json.data[0].Provider;
                        }
                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<WebAPIModelResponse> PostGetCombPortSetting(string API_URIs, SettingModel settobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            WebAPIModelResponse.SsoId = json.data[0].SsoId;
                            WebAPIModelResponse.CombPortName = json.data[0].Password;
                            WebAPIModelResponse.baudrate = json.data[0].BAUDRATE;
                            WebAPIModelResponse.databit = json.data[0].DATABITCOMBO;
                            WebAPIModelResponse.Isreversed = json.data[0].ISREVERSED;
                            WebAPIModelResponse.Stopbits = json.data[0].STOPBITS;
                            WebAPIModelResponse.Paritys = json.data[0].Paritys;
                        }
                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<WebAPIModelResponse> Postunconfirmrawanna(string API_URIs, ConfirmERawannaModel confirmrewbj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(confirmrewbj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            WebAPIModelResponse.SsoId = json.data[0].SSOID;
                            WebAPIModelResponse.CompanyId = json.data[0].CompanyId;
                            WebAPIModelResponse.eRawannaNo = json.data[0].ERawannaNo;
                            WebAPIModelResponse.leaseNo = json.data[0].MLNo;
                            WebAPIModelResponse.leaseID = json.data[0].LeaseId;
                            WebAPIModelResponse.mineralName = json.data[0].MineralName;
                            WebAPIModelResponse.consigneeName = json.data[0].ConsigneeName;
                            WebAPIModelResponse.transportMode = json.data[0].TransportDetails;
                            WebAPIModelResponse.vehicleNo = json.data[0].Vechicle;
                            WebAPIModelResponse.approxTime = json.data[0].ApproximateTime;
                            WebAPIModelResponse.Vehicleweight = json.data[0].Vechicleweight;
                            WebAPIModelResponse.driverMobNo = json.data[0].DriverMobileNo;
                            WebAPIModelResponse.driverName = json.data[0].DriverName;
                            WebAPIModelResponse.approxWeight = json.data[0].Vechicleweight;
                            WebAPIModelResponse.statuss = json.data[0].Rawannastatus;
                            WebAPIModelResponse.approxRoyalty = json.data[0].RoyaltySchedule;
                            WebAPIModelResponse.ravannaDate = json.data[0].RawannaDate;
                            WebAPIModelResponse.approxTotalAmount = json.data[0].ApproxTotalAmount;
                            WebAPIModelResponse.approxNMET = json.data[0].ApproxNMET;
                            WebAPIModelResponse.location = json.data[0].Location;
                            WebAPIModelResponse.TransactionType = json.data[0].TransactionType;
                            WebAPIModelResponse.approxDMFT = json.data[0].ApproxDMFT;
                            WebAPIModelResponse.vehicleName = json.data[0].vehicleName;
                            WebAPIModelResponse.TareWeight = json.data[0].TareWeight;
                        }

                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<WebAPIModelResponse> PostGenerateRawannaDetail(string API_URIs, WebAPIModelResponse webAPIobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(webAPIobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            WebAPIModelResponse.SsoId = json.data[0].SsoId;
                            WebAPIModelResponse.CombPortName = json.data[0].Password;
                            WebAPIModelResponse.baudrate = json.data[0].BAUDRATE;
                            WebAPIModelResponse.databit = json.data[0].DATABITCOMBO;
                            WebAPIModelResponse.Isreversed = json.data[0].ISREVERSED;
                            WebAPIModelResponse.Stopbits = json.data[0].STOPBITS;
                            WebAPIModelResponse.Paritys = json.data[0].Paritys;
                        }
                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }

        public static async Task<DataTable> PostOrderDetail(string API_URIs, SettingModel settingmdl)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingmdl);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostGetLeaseDetail(string API_URIs, ConsigneeDetailsModel consigneeDetails)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(consigneeDetails);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        public static async Task<DataTable> PostInvoiceNumberdtl(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        public static async Task<DataTable> PostGetMineraltl(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostGetProductItemPricedtl(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<ConfirmERawannaModel> PostinsertConfirmRawannaInvoice(string API_URIs, ConfirmERawannaModel requestObj)
        {
            // Initialization.  
            var dataAsString = JsonConvert.SerializeObject(requestObj);
            var content = new StringContent(dataAsString);
            ConfirmERawannaModel ObjconfirmerawannaModel = new ConfirmERawannaModel();
            try
            {
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result; //await client.PostAsJsonAsync(API_URIs, requestObj).ConfigureAwait(false);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        ObjconfirmerawannaModel.success = json.success;
                        ObjconfirmerawannaModel.MessageDiscription = json.message;
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        ObjconfirmerawannaModel.ErrorCode = "602";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ObjconfirmerawannaModel;
        }
        public static async Task<SettingModel> PostGetCamerasetting(string API_URIs, SettingModel requestObj)
        {
            // Initialization.  

            SettingModel objsettingmdl = new SettingModel();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;// await client.PostAsJsonAsync(API_URIs, requestObj).ConfigureAwait(false);

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        objsettingmdl.Success = json.success;
                        objsettingmdl.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            objsettingmdl.CameraUserName = json.data[0].Camerausername;
                            objsettingmdl.CameraUserPassword = json.data[0].CameraPassword;
                            objsettingmdl.CameraIPAddress = json.data[0].CameraIPaddress;
                        }
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        objsettingmdl.Success = "602";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return objsettingmdl;
        }

        public static async Task<DataTable> PostgetconsigneeDetail(string API_URIs, ConsigneeDetailsModel consigneeDetails)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(consigneeDetails);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        public static async Task<DataTable> Postgetconsigneenameaddress(string API_URIs, ConsigneeDetailsModel consigneeDetails)
        {
            // Initialization. 
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(consigneeDetails);
                var content = new StringContent(dataAsString);
                // Posting. 
                using (var client = new HttpClient())
                {

                    // Setting timeout. 
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    // Initialization. 
                    HttpResponseMessage response = new HttpResponseMessage();
                    // HTTP POST 
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    // Verification 
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response. 
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtrecord;
        }

        public static async Task<DataTable> PostGetdealerInfoDetail(string API_URIs, ConsigneeDetailsModel consigneeDetails)
        {
            // Initialization. 
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(consigneeDetails);
                var content = new StringContent(dataAsString);
                // Posting. 
                using (var client = new HttpClient())
                {

                    // Setting timeout. 
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    // Initialization. 
                    HttpResponseMessage response = new HttpResponseMessage();
                    // HTTP POST 
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    // Verification 
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response. 
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtrecord;
        }

        public static async Task<DataTable> PostLogoutUser(string API_URIs, SettingModel requestObj)
        {
            // Initialization. 
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting. 
                using (var client = new HttpClient())
                {

                    // Setting timeout. 
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    // Initialization. 
                    HttpResponseMessage response = new HttpResponseMessage();
                    // HTTP POST 
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    // Verification 
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response. 
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtrecord;
        }
        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return await httpClient.PostAsync(url, content);
        }
        public static async Task<DataTable> PostCheckUserDetails(string API_URIs, CheckUser requestObj)
        {
            // await Task.Delay(10);
            // Initialization. 
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting. 
                using (var client = new HttpClient())
                {
                    // Setting timeout. 
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));
                    // Initialization. 
                    HttpResponseMessage response = new HttpResponseMessage();
                    // HTTP POST 
                    //response = client.PostAsync(Onlinewebsiteurl + API_URIs, content).Result;\
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    // Verification 
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response. 
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dtrecord;
        }

        public static async Task<DataTable> PostGetdevicetypedtl(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostGetdevicenamedtl(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        public static async Task<DataTable> PostSaveErrorLogtable(string API_URIs, ErrorModel requestObj)
        {
            // Initialization. 
            DataTable dtrecord = new DataTable();
            ErrorModel ObjErrorModel = new ErrorModel();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting. 
                using (var client = new HttpClient())
                {

                    // Setting timeout. 
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization. 
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST 
                    response = client.PostAsync("http://dmg.minesmart.in/" + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification 
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response. 
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response. 
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        public static async Task<DataTable> PostSaveErrorLogtableCustom(string API_URIs, ErrorModel requestObj)
        {
            // Initialization. 
            DataTable dtrecord = new DataTable();
            ErrorModel ObjErrorModel = new ErrorModel();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting. 
                using (var client = new HttpClient())
                {
                    // Setting timeout. 
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization. 
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST 
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification 
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response. 
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response. 
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostSaveErrorLogtabledt(string API_URIs, ErrorModel requestObj)
        {
            // Initialization. 
            DataTable dtrecord = new DataTable();
            ErrorModel ObjErrorModel = new ErrorModel();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting. 
                using (var client = new HttpClient())
                {
                    // Setting timeout. 
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization. 
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST 
                    response = client.PostAsync("http://dmg.minesmart.in/" + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification 
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response. 
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response. 
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        #region Checking User Name and Password Valid
        public static async Task<DataTable> PostCheckUserEmailIdExist(string API_URIs, Login loginobj)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }




                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        #endregion

        public static async Task<ConfirmERawannaModel> PostinsertTransitPass(string API_URIs, ConfirmERawannaModel requestObj)
        {
            // Initialization.  

            ConfirmERawannaModel ObjconfirmerawannaModel = new ConfirmERawannaModel();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        ObjconfirmerawannaModel = JsonConvert.DeserializeObject<ConfirmERawannaModel>(result);

                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        ObjconfirmerawannaModel.ErrorCode = "602";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ObjconfirmerawannaModel;
        }

        #region Checking User Name and Password Valid
        public static async Task<WebAPIModelResponse> PostGetcompanydetails(string API_URIs, SettingModel settingobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            WebAPIModelResponse.FirmName = json.data[0].Company;
                            WebAPIModelResponse.FirmAddress = json.data[0].Address;
                            WebAPIModelResponse.FirmGSTNumber = json.data[0].GstNo;
                            WebAPIModelResponse.ClientId = json.data[0].CLientId;
                        }
                        // Releasing.  
                        //response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }
        #endregion

        #region Checking Tax Slab
        public static async Task<DataTable> PostGettaxslab(string API_URIs, SettingModel settingobj)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        #endregion

        #region Get Invoice Number
        public static async Task<WebAPIModelResponse> PostGetInvoiceNumber(string API_URIs, SettingModel settingobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        WebAPIModelResponse.Message = json.message;
                        if (json["data"].Count > 0)
                        {
                            WebAPIModelResponse.InvoiceNo = json.data[0].INVOICENO;
                        }
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return webAPIresp;
        }
        #endregion

        #region Checking Tax Slab
        public static async Task<DataTable> PostGetInvoiceConsigneeName(string API_URIs, SettingModel settingobj)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }  //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        #endregion

        #region InvoiceData
        public static async Task<DataTable> PostSetInvoicedata(string API_URIs, InvoiceModel invmdl)
        {
            // Initialization.  

            DataTable dtresp = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(invmdl);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtresp = JsonToDataTable(result);

                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtresp = JsonToDataTable(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtresp;
        }
        #endregion

        #region InvoiceData
        public static async Task<DataTable> PostGetTicketNumber(string API_URIs, SettingModel Stmdl)
        {
            // Initialization.  

            DataTable dtresp = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(Stmdl);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtresp = JsonToDataTable(result);

                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtresp = JsonToDataTable(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtresp;
        }
        #endregion

        public static async Task<DataTable> PostLanguageSetting(string API_URIs, Login loginobj)
        {
            // Initialization.  
            WebAPIModelResponse webAPIresp = new WebAPIModelResponse();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        public static async Task<DataTable> PostGetLanguagesetting(string API_URIs, SettingModel _settingModal)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(_settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);

                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostCheckLanguageSetting(string API_URIs, SettingModel _settingModal)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(_settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);

                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }


        public static async Task<ConsigneeDetailsModel> Postbinddealerdetail(string API_URIs, ConsigneeDetailsModel requestObj)
        {
            // Initialization.  

            ConsigneeDetailsModel Objconsigneemodel = new ConsigneeDetailsModel();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(requestObj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        Objconsigneemodel = JsonConvert.DeserializeObject<ConsigneeDetailsModel>(result);

                        // Releasing.  
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        Objconsigneemodel.ErrorCode = "602";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Objconsigneemodel;
        }

        public static async Task<DataTable> PostGetLatestWeightSlip(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostGetLastVechicledtl(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {

                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dtrecord = JsonToDataTable(result);
                    }
                    else
                    {
                        // Reading Response.  
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        WebAPIModelResponse.success = json.success;
                        //webAPIresp = JsonConvert.DeserializeObject<WebAPIModelResponse>(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostGetConfirmrawanna(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostGetConfirmTPass(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostGetWeightSlipDetails(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        public static async Task<DataTable> PostSetProductName(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostGetProductItemdtl(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }


        public static async Task<DataTable> PostgetInvoiceConsigneeNameGST(string API_URIs, SettingModel settingModal)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModal);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static DataTable JsonToDataTable(string jsonString)
        {
            var jsonLinq = JObject.Parse(jsonString);

            // Find the first array using Linq  
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();
            foreach (JObject row in srcArray.Children<JObject>())
            {
                var cleanRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types  
                    if (column.Value is JValue)
                    {
                        cleanRow.Add(column.Name, column.Value);
                    }
                }
                trgArray.Add(cleanRow);
            }

            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }

        public static async Task<DataTable> PostDashboardDetails(string API_URIs, SettingModel settingModalobj)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModalobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST   /Api/ErawaanaAPI/GetDashboardDetails/ GetDashboardDetails
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                    // Verification  

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }


        public static async Task<DataTable> PostApplicationVersion(string API_URIs, SettingModel settingModalobj)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();
            DataTable dtrecord = new DataTable();

            var dataAsString = JsonConvert.SerializeObject(settingModalobj);
            var content = new StringContent(dataAsString);
            // Posting.  
            using (var client = new HttpClient())
            {
                // Setting Base address.  

                // Setting timeout.  
                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                // Initialization.  
                HttpResponseMessage response = new HttpResponseMessage();

                // HTTP POST   /Api/ErawaanaAPI/GetDashboardDetails/ GetDashboardDetails
                response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    dynamic json = JsonConvert.DeserializeObject(result);
                    var jo = JObject.Parse(result);
                    var Status = jo["success"].ToString();
                    var Message = jo["message"].ToString();
                    if (Status == "200")
                    {
                        dtrecord = JsonToDataTable(result);
                        System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                        newColumn.DefaultValue = "200";
                        dtrecord.Columns.Add(newColumn);
                    }
                    else if (Status == "210")
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = Message.ToString();
                        dr["Status"] = Status.ToString();
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = Message.ToString();
                        dr["Status"] = Status.ToString();
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;

                    }

                }
                else
                {
                    DataRow dr = dtJsonMessage.NewRow();
                    dr = dtJsonMessage.NewRow();
                    dr["MessageDiscription"] = "Some Error Occured";
                    dr["Status"] = "212";
                    dtJsonMessage.Rows.Add(dr);
                    return dtJsonMessage;
                }
                // Verification  

            }


            return dtrecord;
        }
        public static async Task<DataTable> Postsystemtypesetting(string API_URIs, SettingModel settingModalobj)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModalobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                    // Verification  

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> Postsystemmodelsetting(string API_URIs, SettingModel settingModalobj)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModalobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                    // Verification  

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }

        public static async Task<DataTable> PostGetsystemmodelallsetting(string API_URIs, SettingModel settingModalobj)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModalobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                    // Verification  

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        public static async Task<DataTable> PostWeightslipsetting(string API_URIs, SettingModel settingModalobj)
        {
            // Initialization.  
            SettingModel setresp = new SettingModel();
            DataTable dtrecord = new DataTable();
            try
            {
                var dataAsString = JsonConvert.SerializeObject(settingModalobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting Base address.  

                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync(Onlinewebsiteurl + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                    // Verification  

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }


        #region Checking SQL SERVER CONNECTIION
        public static async Task<DataTable> PostCheckSqlconnectionExist(string API_URIs, Login loginobj)
        {
            // Initialization.  
            DataTable dtrecord = new DataTable();
            try
            {
                dtJsonMessage = new DataTable();
                dtJsonMessage.Columns.Add("Status", typeof(string));
                dtJsonMessage.Columns.Add("MessageDiscription", typeof(string));
                var dataAsString = JsonConvert.SerializeObject(loginobj);
                var content = new StringContent(dataAsString);
                // Posting.  
                using (var client = new HttpClient())
                {
                    // Setting timeout.  
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.  
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP POST  
                    response = client.PostAsync("http://dmg.minesmart.in/" + API_URIs, new StringContent(dataAsString, Encoding.UTF8, "application/json")).Result;

                    // Verification  
                    if (response.IsSuccessStatusCode)
                    {
                        string result = response.Content.ReadAsStringAsync().Result;
                        dynamic json = JsonConvert.DeserializeObject(result);
                        var jo = JObject.Parse(result);
                        var Status = jo["success"].ToString();
                        var Message = jo["message"].ToString();
                        if (Status == "200")
                        {
                            dtrecord = JsonToDataTable(result);
                            System.Data.DataColumn newColumn = new System.Data.DataColumn("Status", typeof(System.String));
                            newColumn.DefaultValue = "200";
                            dtrecord.Columns.Add(newColumn);
                        }
                        else if (Status == "210")
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;
                        }
                        else
                        {
                            DataRow dr = dtJsonMessage.NewRow();
                            dr = dtJsonMessage.NewRow();
                            dr["MessageDiscription"] = Message.ToString();
                            dr["Status"] = Status.ToString();
                            dtJsonMessage.Rows.Add(dr);
                            return dtJsonMessage;

                        }

                    }
                    else
                    {
                        DataRow dr = dtJsonMessage.NewRow();
                        dr = dtJsonMessage.NewRow();
                        dr["MessageDiscription"] = "Some Error Occured";
                        dr["Status"] = "212";
                        dtJsonMessage.Rows.Add(dr);
                        return dtJsonMessage;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dtrecord;
        }
        #endregion

    }


}


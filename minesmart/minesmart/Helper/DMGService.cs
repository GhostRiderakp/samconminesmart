using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.VisualBasic;
using System.Net;
using ERawannaDesk.ViewModels;
using System.Security.Cryptography;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Reflection;

namespace ERawannaDesk.Helper
{
    public  class DMGService
    {
        public static string MachineName, PortName;
        public static string ProviderID = Convert.ToString(WebAPIModelResponse.Provider);// "00011.4.5";
                                                                                         // public static string ProductionURL = "http://mines.rajasthan.gov.in/services/v1.4/";
        public static string ProductionURL = "http://103.203.138.51/services/v1.4";

        /// <summary>
        /// Change dynamic key 16 July
        /// </summary>
        public static string ProviderAccessKey = Convert.ToString(WebAPIModelResponse.ActiveKey);//"scll4byJdTh41+JGZiKsQmPsdACSl28exl2HX9nZRshCWKWWS8+gNUTJSrKfRZkUJI0Rl2n9auLjeV86ELeGRnTrdCwPVY5npqmGStuLmxbDlo2Jctc0NPlYzjebF2tJmkpWcEt6A4xgZMLQiUzDmPcCm7xLKS1u9c2HeFobOIAwvfgUcrdroCJoFZpOgeMS6anWdyGjpTXTTNBr4xDANjk+wOWRE2qHv5MRZPdRayw7DxgGdLyeoKxYax9VGd4GvN9aKrlpaY9e4wL8um69comeeBPZvAhl1IxWeToTGx0MYCI3ywYt44x5+nRksoPl/PL1E6oZFvMQ+aliOpqxVw==@@@@AQAB";
        public static string SampleEncryptionKey = Convert.ToString(WebAPIModelResponse.Accesskey);// "qk6y42gsAuJbOMrct0m6dtFqGd";
        //public static String ProviderAccessKey = "jESrtAg9DObEjyW/N/MCvm8uUAoJgIAqmd8iyoJW7dhqtZBpSHrarLpy5txr8XqnzJGmDQfBwlTK1fLhs+Trj4XxpTJLHjSFw0rg3ljAykezosRj4bOZngHa6MEwO0HZofV5ahOPX1vVl1oBVFkU19fM2XtxMStawYBlhTMr0rhyA35zUfRlQAFXYtrJEmk4S57syMoGBllFJQzjO4mRFx05gUkhAcOPmBCWd7AA0dvgF4p/+ZFKzI3k0MpOfN4wnBCQwk80tlkg517Qk7u4c/cbB+rZoDegkrNHuIEHlbehSC2jpncuLR7LvnMaCrUgV+TOTBkovXWSmJBv2BKL/Q==@@@@AQAB";
        //scll4byJdTh41+JGZiKsQmPsdACSl28exl2HX9nZRshCWKWWS8+gNUTJSrKfRZkUJI0Rl2n9auLjeV86ELeGRnTrdCwPVY5npqmGStuLmxbDlo2Jctc0NPlYzjebF2tJmkpWcEt6A4xgZMLQiUzDmPcCm7xLKS1u9c2HeFobOIAwvfgUcrdroCJoFZpOgeMS6anWdyGjpTXTTNBr4xDANjk+wOWRE2qHv5MRZPdRayw7DxgGdLyeoKxYax9VGd4GvN9aKrlpaY9e4wL8um69comeeBPZvAhl1IxWeToTGx0MYCI3ywYt44x5+nRksoPl/PL1E6oZFvMQ+aliOpqxVw@@@@AQAB
        public static async Task<JObject> dmgservicesPost(string url, string response_data, string accessKey, string weighBridgeNo)
        {
            // eravanna / generateConfirmRawanna
            string result = "";
             DataTable dtn = new DataTable();
            JObject returnJOBJECT = new JObject();
            if (url != HttpService.localURL + "eravanna/generateRawanna" && url != HttpService.localURL + "tp/generateConfirmTransitPass" && url != HttpService.localURL + "eravanna/generateConfirmRawanna")
            {
                string encrpttedData = DMGENCODEDECODE.DMGNewEncode(response_data, accessKey, ProviderAccessKey);
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                string requestContent = string.Format("encData={0}&weighbridgeNo={1}&providerId={2}", (object)GetEscapedLongUri(encrpttedData.Trim()), (object)GetEscapedLongUri(weighBridgeNo), (object)GetEscapedLongUri(ProviderID.Trim()));
                request.Content = (HttpContent)new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request);
                result = await response.Content.ReadAsStringAsync();
                //MessageBox.Show(DMGENCODEDECODE.DMGNewDecode(result, SampleEncryptionKey));
                encrpttedData = (string)null;
                client = (HttpClient)null;
                request = (HttpRequestMessage)null;
                requestContent = (string)null;
                response = (HttpResponseMessage)null;
            }
            else
            {
                string encrpttedData = DMGENCODEDECODE.DMGNewEncode(response_data, accessKey, ProviderAccessKey);
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                string requestContent = string.Format("encData={0}&weighbridgeNo={1}&providerId={2}", (object)GetEscapedLongUri(encrpttedData.Trim()), (object)GetEscapedLongUri(weighBridgeNo.Trim()), (object)GetEscapedLongUri(ProviderID.Trim()));
                request.Content = (HttpContent)new StringContent(requestContent, Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = await client.SendAsync(request);
                result = await response.Content.ReadAsStringAsync();
                encrpttedData = (string)null;
                client = (HttpClient)null;
                requestContent = (string)null;
                response = (HttpResponseMessage)null;
            }
            string decrypttedData = "";
            try
            {
                if (result.Contains("Status"))
                {
                    dynamic data = JObject.Parse(result);
                    var fresult = data.Status;
                    var fMessageDiscription = data.MessageDiscription;
                    returnJOBJECT["ErrorMsg"] = (JToken)"true";
                    returnJOBJECT["ShowResult"] = (JToken)fMessageDiscription;
                }
                else if (result.Contains("404"))
                {
                    var fMessageDiscription = "Some Error Occured this Process";
                    returnJOBJECT["ErrorMsg"] = (JToken)"true";
                    returnJOBJECT["ShowResult"] = (JToken)fMessageDiscription;
                }
                else
                {
                    decrypttedData = DMGENCODEDECODE.DMGNewDecode(result, accessKey);
                    JObject o = JObject.Parse(decrypttedData);
                    if (o["Status"].ToString() == "301")
                    {
                        o = (JObject)null;
                        returnJOBJECT["ErrorMsg"] = (JToken)"True";
                        returnJOBJECT["ShowResult"] = (JToken)decrypttedData;
                    }
                    else if (o["Status"].ToString() == "200")
                    {
                        int i = 0;
                        o = (JObject)null;
                        returnJOBJECT["ErrorMsg"] = (JToken)"false";
                        returnJOBJECT["ShowResult"] = (JToken)decrypttedData;

                    }
                    else
                    {
                        //dtn = Tabulate(decrypttedData);
                        o = (JObject)null;
                        returnJOBJECT["ErrorMsg"] = (JToken)"false";
                        returnJOBJECT["ShowResult"] = (JToken)decrypttedData;
                    }
                }
            }
            catch (Exception ex)
            {
                returnJOBJECT["ErrorMsg"] = (JToken)"true";
                returnJOBJECT["ShowResult"] = (JToken)result;
            }

            return returnJOBJECT;
        }
        
        public static string GetEscapedLongUri(string value)
        {
            int length = 2000;
            StringBuilder stringBuilder = new StringBuilder();
            int num = value.Length / length;
            for (int index = 0; index <= num; ++index)
            {
                if (index < num)
                    stringBuilder.Append(Uri.EscapeDataString(value.Substring(length * index, length)));
                else
                    stringBuilder.Append(Uri.EscapeDataString(value.Substring(length * index)));
            }
            return stringBuilder.ToString();
        }
        public static DataTable Tabulate(string json)
        {
            if (json == "")
                return new DataTable();
            JObject jobject1 = JObject.Parse(json);
            IEnumerable<JToken> source = jobject1.Descendants().Where<JToken>((Func<JToken, bool>)(d => d is JArray));
            JToken jtoken = source.Count<JToken>() != 0 ? source.First<JToken>() : throw new Exception(jobject1.ToString());
            JArray jarray = new JArray();
            foreach (JObject child in jtoken.Children<JObject>())
            {
                JObject jobject2 = new JObject();
                foreach (JProperty property in child.Properties())
                {
                    if (property.Value is JValue)
                        jobject2.Add(property.Name, property.Value);
                }
                jarray.Add((JToken)jobject2);
            }
            return JsonConvert.DeserializeObject<DataTable>(jarray.ToString());
        }
        public static string ConvertJson(Dictionary<string, string> datacollection)
        {
            string contentData = string.Empty;
            JObject myObject = new JObject();
            JArray array123 = new JArray();
            foreach (KeyValuePair<string, string> keyValuePair in datacollection)
            {
                KeyValuePair<string, string> entry = keyValuePair;
                myObject[entry.Key] = (JToken)entry.Value.Trim();
                entry = new KeyValuePair<string, string>();
            }
            array123.Add((JToken)myObject);
            JObject returnObject = new JObject((object)new JProperty("Data", (object)array123));
            contentData = returnObject.ToString();
            return contentData;
        }
        public static string ConvertJsonTP(Dictionary<string, string> datacollection)
        {
            string contentData = string.Empty;
            JObject myObject = new JObject();
            JArray array123 = new JArray();
            foreach (KeyValuePair<string, string> keyValuePair in datacollection)
            {
                KeyValuePair<string, string> entry = keyValuePair;
                myObject[entry.Key] = (JToken)entry.Value.Trim();
                // entry = new KeyValuePair<string, string>();
            }
            array123.Add((JToken)myObject);
            //JObject returnObject = new JObject((object)new JProperty("", (object)array123));
            contentData = array123.ToString().Replace("[", "").Replace("]", "").Trim();
            return contentData;
        }
        public static string ConvertJsonwithoutdata(Dictionary<string, string> datacollection)
        {
            string contentData = string.Empty;
            JObject myObject = new JObject();
            JArray array123 = new JArray();
            foreach (KeyValuePair<string, string> keyValuePair in datacollection)
            {
                KeyValuePair<string, string> entry = keyValuePair;
                myObject[entry.Key] = (JToken)entry.Value.Trim();
                entry = new KeyValuePair<string, string>();
            }
            array123.Add((JToken)myObject);
            JObject returnObject = new JObject((object)new JProperty("Data", (object)array123));
            contentData = array123.ToString();
            return contentData;
        }

        public static async Task<string> GenerateConfirmedRawanna(string url, Dictionary<string, string> dictionary, string Encryption_Key, string weighbridgeNo)
        {
            string returnOutput = "";
            try
            {
                returnOutput = GetWebResponse(url, dictionary, weighbridgeNo, Encryption_Key);
            }
            catch (Exception ex)
            {
                returnOutput = ex.Message;
            }
            return returnOutput;
        }

        private static string GetWebResponse(string URL, Dictionary<string, string> dictionary, string weighbridgeNo, string accessKey)
        {
            string jsonDictionary = ConvertToJSON(dictionary);
            string encryptedData = Encrypt(ConvertToJSON(dictionary), accessKey);
            var parameters = new System.Collections.Specialized.NameValueCollection
            {
                { "encData", encryptedData },
                { "weighbridgeNo", weighbridgeNo },
                { "providerId", ProviderID }
            };
            byte[] response;
            using (WebClient webClient = new WebClient())
            {
                response = webClient.UploadValues(URL, "POST", parameters);
            }
            //WebClient wc = new WebClient();
            //byte[] response = wc.UploadValues(URL, "POST", parameters);

            return Decrypt(Encoding.UTF8.GetString(response), accessKey);
        }

        private static string ConvertToJSON(Dictionary<string, string> dictionary)
        {
            JObject jObject = new JObject();
            JArray dataArray = new JArray();

            foreach (KeyValuePair<string, string> entry in dictionary)
            {
                jObject[entry.Key] = entry.Value.Trim();
            }
            dataArray.Add(jObject);
            JObject returnObject = new JObject(new JProperty("Data", dataArray));
            return returnObject.ToString();
        }
        public static string Encrypt(string content_data, string accessKey) => DMGENCODEDECODE.DMGNewEncode(content_data, accessKey, ProviderAccessKey);
        public static string Decrypt(string content_data, string accessKey) => DMGENCODEDECODE.DMGNewDecode(content_data, accessKey);


    }
}

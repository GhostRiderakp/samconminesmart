using ERawaana.Helper;
using minesmart.DGMS;
using minesmart.Helper;
using minesmart.Helper.Command;
using minesmart.ViewModels;
using Nancy.Json;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesmart
{
    public partial class Form1 : Form
    {
        public static String ErrorFilePath = @"\ErrorLog\minesmart.txt";
        public static System.IO.StreamWriter objWriter;
        public static string applicationPath = System.Windows.Forms.Application.StartupPath;
        public static string LogFolderPath = applicationPath + "MineSmart.txt";// ErrorFilePath;
        Login login = new Login();
        Select select = new Select();
        CheckConnection checkcon = new CheckConnection();
        bool result = false;
        static string conn_str = Properties.Settings.Default.Connection_String;
        static string weightbreidge = Properties.Settings.Default.WeightbridgeNo;
        WebAPIModelResponse wb = new WebAPIModelResponse();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        const string clientID = "3898637725-vqq1s7pvqad0u494d7376vesrci0d6i2.apps.googleusercontent.com";
        const string clientSecret = "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";
        const string authorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        const string tokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
        const string userInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";
        static DataTable dtMain = new DataTable();
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);
        public Form1()
        {
            InitializeComponent();
            lblErrormessage.Visible = false;
            radioButton1.Checked = true;
            txtUserId.Focus();
            this.Text = "Minesmart " + GetAssemblyVersion();
            lblversions.Text = GetAssemblyVersion();
            lblversions.ForeColor = System.Drawing.Color.Black;
        }

        public string GetAssemblyVersion()
        {
            var version = string.Empty;
            try
            {
                DataTable dtn = new DataTable();
                SettingModel settingModal = new SettingModel();
                settingModal.PostUrl = "/Api/ErawaanaAPI/ApplicationVersion/";
                settingModal.Authtoken = UniqueSystemId.GetMacAddress();
                dtMain = WebAPI.PostApplicationVersion(settingModal.PostUrl, settingModal).Result;
                if (dtMain != null)
                {
                    if (dtMain != null && dtMain.Rows[0]["Status"].ToString() == "200")
                    {
                        version = dtMain.Rows[0]["ApplicationVersion"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrormessage.Visible = true;
                lblErrormessage.Text = string.Empty;
                lblErrormessage.Text = "Application Version Issued";
            }
            return version;
        }


        public static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var IsCheckInternet = false;
            IsCheckInternet = IsInternetAvailable();
            if (IsCheckInternet)
                checkLoginResult();
            else
            {
                lblErrormessage.Visible = true;
                lblErrormessage.Text = string.Empty;
                lblErrormessage.Text = "Internet May Not be Available. Please Check Network .";
            }
        }
        public static bool IsInternetAvailable()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }
        public void checkLoginResult()
        {
            DataTable dt = new DataTable();
            DataTable dtn = new DataTable();
            DataTable dtapi = new DataTable();
            SettingModel settingModal = new SettingModel();
            DealerModel dealerListmdl = new DealerModel();
            Exception ewx = new Exception();
            if (txtUserId.Text != "" && txtPassword.Text != "")
            {
                try
                {
                    login.SSoUrl = "/Api/ErawaanaAPI/getCheckApiWorking/";
                    dtapi = WebAPI.PostCheckApiWorking(login.SSoUrl, login).Result;
                    if (dtapi != null && dtapi.Rows.Count > 0)
                    {
                        if (dtapi.Rows[0]["Status"].ToString() == "200")
                        {
                            //Check UserName Exist 
                            login.SSoid = txtUserId.Text.Trim();
                            login.Password = txtPassword.Text.Trim();
                            WebAPIModelResponse.LoginUserEmailId = txtUserId.Text.Trim();
                            login.Authtoken = checkcon.GetMacAddress();
                            WebAPIModelResponse.Authtoken = checkcon.GetMacAddress();
                            login.Weightbridge = weightbreidge.ToString() == null ? "00844" : weightbreidge.ToString();
                            login.SSoUrl = "/Api/ErawaanaAPI/CheckUserNameExist/";
                            login.Authtoken = UniqueSystemId.GetMacAddress();
                            dtMain = WebAPI.PostCheckUserNameExist(login.SSoUrl, login).Result;
                            if (dtMain.Rows[0]["Status"].ToString() == "200")
                            {
                                //Check UserName Exist 
                                dtMain = new DataTable();
                                login.SSoid = txtUserId.Text.Trim();
                                login.Password = txtPassword.Text.Trim();
                                login.Authtoken = checkcon.GetMacAddress();
                                WebAPIModelResponse.Authtoken = checkcon.GetMacAddress();
                                login.Weightbridge = weightbreidge.ToString() == null ? "00844" : weightbreidge.ToString();
                                login.SSoUrl = "/Api/ErawaanaAPI/CheckPasswordExist/";
                                dtMain = WebAPI.PostCheckPasswordExist(login.SSoUrl, login).Result;
                                if (dtMain.Rows[0]["Status"].ToString() == "200")
                                {
                                    WebAPIModelResponse.UserProfileid = dtMain.Rows[0]["UserPofileId"].ToString();
                                    WebAPIModelResponse.UserCredtentialId = dtMain.Rows[0]["UserCredtentialId"].ToString();
                                    dtMain = new DataTable();
                                    //Checking User Name and Password Valid
                                    login.SSoid = txtUserId.Text.Trim();
                                    login.Password = txtPassword.Text.Trim();
                                    login.Authtoken = checkcon.GetMacAddress();
                                    WebAPIModelResponse.Authtoken = checkcon.GetMacAddress();
                                    login.Weightbridge = weightbreidge.ToString() == null ? "00844" : weightbreidge.ToString();
                                    login.SSoUrl = "/Api/ErawaanaAPI/CheckUserPasswordExist/";
                                    dtMain = WebAPI.PostCheckUserPasswordExist(login.SSoUrl, login).Result;
                                    if (dtMain.Rows[0]["Status"].ToString() == "210")
                                    {
                                        txtPassword.Text = string.Empty;
                                        txtUserId.Text = string.Empty;
                                        txtUserId.Focus();
                                        ErrorModel errormdl = new ErrorModel();
                                        errormdl.DateLog = DateTime.Now.ToString();
                                        errormdl.Message = "Your Account is not active.Please Contact your Administrator";
                                        minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);
                                        // System.Windows.Forms.MessageBox.Show(dtMain.Rows[0]["MessageDiscription"].ToString(), "Registered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        lblErrormessage.Text = string.Empty;
                                        lblErrormessage.Text = "Your Account is not active.Please Contact your Administrator.";
                                    }
                                    else
                                    {
                                        WebAPIModelResponse.SsoId = dtMain.Rows[0]["SsoId"].ToString();
                                        WebAPIModelResponse.Password = dtMain.Rows[0]["Password"].ToString();
                                        WebAPIModelResponse.Weightbridgeno = dtMain.Rows[0]["WeighBridgeNo"].ToString();
                                        WebAPIModelResponse.Accesskey = dtMain.Rows[0]["AccessKey"].ToString();
                                        WebAPIModelResponse.ActiveKey = dtMain.Rows[0]["ActiveKey"].ToString();
                                        WebAPIModelResponse.Userid = dtMain.Rows[0]["UserId"].ToString();
                                        WebAPIModelResponse.CompanyId = Convert.ToInt64(dtMain.Rows[0]["CompanyId"].ToString());
                                        WebAPIModelResponse.ClientId = Convert.ToInt64(dtMain.Rows[0]["ClientId"].ToString());
                                        WebAPIModelResponse.Provider = dtMain.Rows[0]["Provider"].ToString();
                                        dtMain = new DataTable();
                                        //Checking User supcription plan expired or not
                                        login.SSoid = Convert.ToString(WebAPIModelResponse.SsoId);
                                        login.clientId = Convert.ToInt64(WebAPIModelResponse.ClientId);
                                        login.Weightbridge = Convert.ToString(WebAPIModelResponse.Weightbridgeno);
                                        login.SSoUrl = "/Api/ErawaanaAPI/CheckUserExpired/";
                                        dtMain = WebAPI.PostCheckUserExpired(login.SSoUrl, login).Result;
                                        if (dtMain.Rows[0]["Status"].ToString() == "200")
                                        {
                                            ErrorModel errormdl = new ErrorModel();
                                            errormdl.DateLog = DateTime.Now.ToString();
                                            errormdl.Message = "Your Weight bridge Licence Expired Last days";
                                            minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);

                                            this.Hide();
                                            SubscriptionPlan supcriptionplan = new SubscriptionPlan();
                                            supcriptionplan.Show();
                                        }
                                        else if (dtMain.Rows[0]["Status"].ToString() == "205")
                                        {
                                            ErrorModel errormdl = new ErrorModel();
                                            errormdl.DateLog = DateTime.Now.ToString();
                                            errormdl.Message = "Your Weight bridge Licence Expired Last days";
                                            minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);
                                            lblErrormessage.Visible = true;
                                            lblErrormessage.Text = string.Empty;
                                            lblErrormessage.Text = "Your Weight bridge Licence Expired Last days.";
                                            // System.Windows.Forms.MessageBox.Show("Your Weight bridge Licence Expired Last days", "Licence", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            this.Hide();
                                            SubscriptionPlan supcriptionplan = new SubscriptionPlan();
                                            supcriptionplan.Show();
                                        }
                                        else
                                        {
                                            try
                                            {
                                                dtMain = new DataTable();
                                                settingModal.SsoId = Convert.ToString(WebAPIModelResponse.SsoId);
                                                settingModal.weightbridgeNo = Convert.ToString(WebAPIModelResponse.Weightbridgeno);
                                                settingModal.CompanyId = Convert.ToInt64(WebAPIModelResponse.CompanyId);
                                                settingModal.ClientId = Convert.ToInt64(WebAPIModelResponse.ClientId);
                                                settingModal.UserId = Convert.ToInt64(WebAPIModelResponse.Userid);
                                                settingModal.PostUrl = "/Api/ErawaanaAPI/setgetsettingdetails/";
                                                dtMain = WebAPI.PostSettingInfoDetail(settingModal.PostUrl, settingModal).Result;
                                                if (dtMain.Rows[0]["Status"].ToString() == "200")
                                                {
                                                    WebAPIModelResponse.CameraRearUrl = dtMain.Rows[0]["CameraRearUrl"].ToString();
                                                    WebAPIModelResponse.CameraFrontUrl = dtMain.Rows[0]["CameraFrontUrl"].ToString();
                                                    WebAPIModelResponse.CombPortName = dtMain.Rows[0]["PortName"].ToString();
                                                    login.UserCredid = Convert.ToInt64(WebAPIModelResponse.UserProfileid);
                                                    login.UserProfileid = Convert.ToInt64(WebAPIModelResponse.UserCredtentialId);
                                                    login.Userid = Convert.ToInt64(WebAPIModelResponse.Userid);
                                                    login.SSoid = txtUserId.Text.Trim();
                                                    login.Password = txtPassword.Text.Trim();
                                                    login.Authtoken = checkcon.GetMacAddress();
                                                    WebAPIModelResponse.Authtoken = checkcon.GetMacAddress();
                                                    login.LanguageSetting = WebAPIModelResponse.LanguageSetting;
                                                    login.Weightbridge = weightbreidge.ToString() == null ? "00844" : weightbreidge.ToString();
                                                    login.PostUrl = "/Api/ErawaanaAPI/SetLanguageSetting/";
                                                    dt = WebAPI.PostLanguageSetting(login.PostUrl, login).Result;
                                                    if (dt != null && dt.Rows.Count > 0)
                                                    {
                                                        WebAPIModelResponse.LoginUserEmailId = dt.Rows[0]["EmailID"].ToString();
                                                        WebAPIModelResponse.FirmName = dt.Rows[0]["FullName"].ToString();
                                                    }
                                                    Properties.Settings.Default.UserSSOID = Convert.ToString(txtUserId.Text);
                                                    Properties.Settings.Default.Password = Comman.Encrypt(txtPassword.Text.Trim());
                                                    Properties.Settings.Default.SettingsKey = checkcon.GetMacAddress();
                                                    Properties.Settings.Default.FirstName = WebAPIModelResponse.FirmName;
                                                    Properties.Settings.Default.DefaultLanguage = WebAPIModelResponse.LanguageSetting;
                                                    Properties.Settings.Default.Save();
                                                    List<String> tList = new List<String>();
                                                    string[] portNames = SerialPort.GetPortNames();
                                                    foreach (var portName in portNames)
                                                    {
                                                        tList.Add(portName);
                                                    }
                                                    if (tList.Contains(WebAPIModelResponse.CombPortName))
                                                    {
                                                        this.Hide();
                                                        MineMart childForm = new MineMart();
                                                        childForm.WindowState = FormWindowState.Maximized;
                                                        childForm.Show();
                                                    }
                                                    else
                                                    {
                                                        //System.Windows.Forms.MessageBox.Show("Combport setting Change Please Change Setting first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                        ErrorModel errormdl = new ErrorModel();
                                                        errormdl.DateLog = DateTime.Now.ToString();
                                                        errormdl.Message = "Combport setting Change Please Change Setting first";
                                                        minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);
                                                        this.Hide();
                                                        MineMart fnpg = new MineMart();
                                                        fnpg.Show();

                                                    }
                                                }
                                                else
                                                {
                                                    // System.Windows.Forms.MessageBox.Show("Combport setting Change Please Change Setting first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    dt = new DataTable();
                                                    WebAPIModelResponse.CameraRearUrl = "rtsp://admin:12345@192.168.1.210:554/Streaming/Channels/101";
                                                    WebAPIModelResponse.CameraFrontUrl = "rtsp://admin:12345@192.168.1.210:554/Streaming/Channels/101";
                                                    login.UserCredid = Convert.ToInt64(WebAPIModelResponse.UserProfileid);
                                                    login.UserProfileid = Convert.ToInt64(WebAPIModelResponse.UserCredtentialId);
                                                    login.Userid = Convert.ToInt64(WebAPIModelResponse.Userid);
                                                    login.SSoid = txtUserId.Text.Trim();
                                                    login.Password = txtPassword.Text.Trim();
                                                    login.Authtoken = checkcon.GetMacAddress();
                                                    WebAPIModelResponse.Authtoken = checkcon.GetMacAddress();
                                                    login.LanguageSetting = WebAPIModelResponse.LanguageSetting;
                                                    login.Weightbridge = weightbreidge.ToString() == null ? "00844" : weightbreidge.ToString();
                                                    login.PostUrl = "/Api/ErawaanaAPI/SetLanguageSetting/";
                                                    dt = WebAPI.PostLanguageSetting(login.PostUrl, login).Result;
                                                    if (dt != null && dt.Rows.Count > 0)
                                                    {
                                                        WebAPIModelResponse.LoginUserEmailId = dt.Rows[0]["EmailID"].ToString();
                                                        WebAPIModelResponse.FirmName = dt.Rows[0]["FullName"].ToString();
                                                    }
                                                    WebAPIModelResponse.IsSubmitsetting = "0";
                                                    Properties.Settings.Default.UserSSOID = Convert.ToString(txtUserId.Text);
                                                    Properties.Settings.Default.Password = Comman.Encrypt(txtPassword.Text.Trim());
                                                    Properties.Settings.Default.SettingsKey = checkcon.GetMacAddress();
                                                    Properties.Settings.Default.FirstName = WebAPIModelResponse.FirmName;
                                                    Properties.Settings.Default.DefaultLanguage = WebAPIModelResponse.LanguageSetting;
                                                    Properties.Settings.Default.IsSubmitsetting = "0";
                                                    Properties.Settings.Default.Save();
                                                    this.Hide();
                                                    MineMart fnpg = new MineMart();
                                                    fnpg.WindowState = FormWindowState.Maximized;
                                                    fnpg.Show();

                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                // minesmart.Helper.ErrorLog.WriteSettingLog(ex, "custommessage", "");
                                                System.Windows.Forms.MessageBox.Show(ex.Message);
                                                System.Windows.Forms.MessageBox.Show(ex.StackTrace);
                                                System.Windows.Forms.MessageBox.Show(ex.InnerException.ToString());
                                                //System.Windows.Forms.MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //Log.Logger = new LoggerConfiguration()
                                    //.WriteTo.Async(a =>
                                    //{
                                    //    a.File(LogFolderPath + "logs.log", rollingInterval: RollingInterval.Hour); // <<<<<
                                    //})
                                    //.MinimumLevel.Verbose()
                                    //.CreateLogger();
                                    txtPassword.Text = string.Empty;
                                    txtUserId.Text = string.Empty;
                                    txtUserId.Focus();
                                    lblErrormessage.Visible = true;
                                    lblErrormessage.Text = string.Empty;
                                    lblErrormessage.Text = "User Name & Password does't Exist.";

                                }
                            }
                            else if (dtMain.Rows[0]["Status"].ToString() == "210")
                            {
                                lblErrormessage.Visible = true;
                                lblErrormessage.Text = string.Empty;
                                lblErrormessage.Text = "user name Exist in our system.";
                            }
                            else
                            {

                                txtPassword.Text = string.Empty;
                                txtUserId.Text = string.Empty;
                                txtUserId.Focus();
                                lblErrormessage.Visible = true;
                                lblErrormessage.Text = string.Empty;
                                lblErrormessage.Text = "user name Exist in our system.";

                            }
                        }
                        else
                        {
                            DialogResult d = System.Windows.Forms.MessageBox.Show("Some Change in our software Please Contact your Sale Team dial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (d == DialogResult.OK)
                            {
                                System.Windows.Forms.Application.Exit();
                                System.Windows.Forms.Application.ExitThread();
                                Process currentProcess = Process.GetCurrentProcess();
                                Process[] processItems = Process.GetProcessesByName(currentProcess.ProcessName);
                                Form1 lg = new Form1();
                                lg.Show();
                            }
                            else
                            {
                                System.Windows.Forms.Application.Exit();
                                System.Windows.Forms.Application.ExitThread();
                                Process currentProcess = Process.GetCurrentProcess();
                                Process[] processItems = Process.GetProcessesByName(currentProcess.ProcessName);
                                Form1 lg = new Form1();
                                lg.Show();
                            }

                        }
                    }
                    else
                    {
                        System.Windows.Forms.Application.Exit();
                        System.Windows.Forms.Application.ExitThread();
                        Process currentProcess = Process.GetCurrentProcess();
                        Process[] processItems = Process.GetProcessesByName(currentProcess.ProcessName);
                    }
                }
                catch (Exception ex)
                {
                    lblErrormessage.Visible = true;
                    lblErrormessage.Text = string.Empty;
                    lblErrormessage.Text = "Our Server is down !.Please contact support for further Information..!!";
                }
            }
            else
            {
                lblErrormessage.Visible = true;
                lblErrormessage.Text = string.Empty;
                lblErrormessage.Text = "Please Enter User Name and Password";
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtUserId.Text = "";
        }



        private async void pictureBox_Click(object sender, EventArgs e)
        {
            // Generates state and PKCE values.
            string state = randomDataBase64url(32);
            string code_verifier = randomDataBase64url(32);
            string code_challenge = base64urlencodeNoPadding(sha256(code_verifier));
            const string code_challenge_method = "S256";

            // Creates a redirect URI using an available port on the loopback address.
            string redirectURI = string.Format("{0}:{1}/", "https://localhost:44353/Home/Login", GetRandomUnusedPort());

            // Creates an HttpListener to listen for requests on that redirect URI.
            var http = new HttpListener();
            http.Prefixes.Add(redirectURI);

            http.Start();

            // Creates the OAuth 2.0 authorization request.
            string authorizationRequest = string.Format("{0}?response_type=code&scope=openid%20profile%20email&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}",
                authorizationEndpoint,
                System.Uri.EscapeDataString(redirectURI),
                clientID,
                state,
                code_challenge,
                code_challenge_method);
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = authorizationRequest,
                UseShellExecute = true
            };
            Process.Start(psi);
            //Process.Start("IExplore.exe", authorizationRequest);


            // Opens request in the browser.
            //System.Diagnostics.Process.Start(authorizationRequest);

            // Waits for the OAuth authorization response.
            var context = await http.GetContextAsync();

            // Brings this app back to the foreground.
            this.Activate();

            // Sends an HTTP response to the browser.
            var response = context.Response;
            string responseString = string.Format("<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>");
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                http.Stop();

            });

            // Checks for errors.
            if (context.Request.QueryString.Get("error") != null)
            {
                return;
            }
            if (context.Request.QueryString.Get("code") == null
                || context.Request.QueryString.Get("state") == null)
            {

                return;
            }

            // extracts the code
            var code = context.Request.QueryString.Get("code");
            var incoming_state = context.Request.QueryString.Get("state");

            // Compares the receieved state to the expected value, to ensure that
            // this app made the request which resulted in authorization.
            if (incoming_state != state)
            {

                return;
            }

            // Starts the code exchange at the Token Endpoint.
            performCodeExchange(code, code_verifier, redirectURI);
        }

        async void performCodeExchange(string code, string code_verifier, string redirectURI)
        {


            // builds the  request
            string tokenRequestURI = "https://www.googleapis.com/oauth2/v4/token";
            string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&client_secret={4}&scope=&grant_type=authorization_code",
                code,
                System.Uri.EscapeDataString(redirectURI),
                clientID,
                code_verifier,
                clientSecret
                );

            // sends the request
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create(tokenRequestURI);
            tokenRequest.Method = "POST";
            tokenRequest.ContentType = "application/x-www-form-urlencoded";
            tokenRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            byte[] _byteVersion = Encoding.ASCII.GetBytes(tokenRequestBody);
            tokenRequest.ContentLength = _byteVersion.Length;
            Stream stream = tokenRequest.GetRequestStream();
            await stream.WriteAsync(_byteVersion, 0, _byteVersion.Length);
            stream.Close();

            try
            {
                // gets the response
                WebResponse tokenResponse = await tokenRequest.GetResponseAsync();
                using (StreamReader reader = new StreamReader(tokenResponse.GetResponseStream()))
                {
                    // reads response body
                    string responseText = await reader.ReadToEndAsync();


                    // converts to dictionary
                    Dictionary<string, string> tokenEndpointDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);

                    string access_token = tokenEndpointDecoded["access_token"];
                    userinfoCall(access_token);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    if (response != null)
                    {

                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            // reads response body
                            string responseText = await reader.ReadToEndAsync();
                        }
                    }

                }
            }
        }
        async void userinfoCall(string access_token)
        {

            // builds the  request
            string userinfoRequestURI = "https://www.googleapis.com/oauth2/v3/userinfo";

            // sends the request
            HttpWebRequest userinfoRequest = (HttpWebRequest)WebRequest.Create(userinfoRequestURI);
            userinfoRequest.Method = "GET";
            userinfoRequest.Headers.Add(string.Format("Authorization: Bearer {0}", access_token));
            userinfoRequest.ContentType = "application/x-www-form-urlencoded";
            userinfoRequest.Accept = "Accept=text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

            // gets the response
            WebResponse userinfoResponse = await userinfoRequest.GetResponseAsync();
            using (StreamReader userinfoResponseReader = new StreamReader(userinfoResponse.GetResponseStream()))
            {
                var CheckEmailstr = string.Empty;
                SettingModel settingModal = new SettingModel();
                DataTable dt = new DataTable();
                DataTable dtCheckEmail = new DataTable();

                dtCheckEmail.Columns.Add("email", typeof(string));
                DataRow dr;
                // reads response body
                string userinfoResponseText = await userinfoResponseReader.ReadToEndAsync();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                var Items = jss.Deserialize<minesmart.ViewModels.AllTP.googlecred>(userinfoResponseText);
                dr = dtCheckEmail.NewRow();
                dr["email"] = Items.email;
                dtCheckEmail.Rows.Add(dr);

                if (dtCheckEmail.Rows.Count > 0)
                {
                    dtMain = new DataTable();
                    CheckEmailstr = Convert.ToString(dtCheckEmail.Rows[0]["email"].ToString());
                    login.SSoid = CheckEmailstr;
                    login.SSoUrl = "/Api/ErawaanaAPI/CheckUserEmailIdExist/";
                    dtMain = WebAPI.PostCheckUserEmailIdExist(login.SSoUrl, login).Result;
                    if (dtMain.Rows[0]["Status"].ToString() == "210")
                    {
                        System.Windows.Forms.MessageBox.Show(dtMain.Rows[0]["MessageDiscription"].ToString(), "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {

                        WebAPIModelResponse.SsoId = dtMain.Rows[0]["SsoId"].ToString();
                        WebAPIModelResponse.Password = dtMain.Rows[0]["Password"].ToString();
                        WebAPIModelResponse.Weightbridgeno = dtMain.Rows[0]["WeighBridgeNo"].ToString();
                        WebAPIModelResponse.Accesskey = dtMain.Rows[0]["AccessKey"].ToString();
                        WebAPIModelResponse.ActiveKey = dtMain.Rows[0]["ActiveKey"].ToString();
                        WebAPIModelResponse.Userid = dtMain.Rows[0]["UserId"].ToString();
                        WebAPIModelResponse.CompanyId = Convert.ToInt64(dtMain.Rows[0]["CompanyId"].ToString());
                        WebAPIModelResponse.ClientId = Convert.ToInt64(dtMain.Rows[0]["ClientId"].ToString());
                        WebAPIModelResponse.Provider = dtMain.Rows[0]["Provider"].ToString();

                        login.SSoid = Convert.ToString(WebAPIModelResponse.SsoId);
                        login.Weightbridge = Convert.ToString(WebAPIModelResponse.Weightbridgeno);
                        login.SSoUrl = "/Api/ErawaanaAPI/CheckUserExpired/";
                        dt = WebAPI.PostCheckUserExpired(login.SSoUrl, login).Result;
                        dtMain = WebAPI.PostCheckUserExpired(login.SSoUrl, login).Result;
                        if (dtMain.Rows[0]["Status"].ToString() == "200")
                        {
                            ErrorModel errormdl = new ErrorModel();
                            errormdl.DateLog = DateTime.Now.ToString();
                            errormdl.Message = "Your Weight bridge Licence Expired Last days";
                            minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);
                            this.Hide();
                            SubscriptionPlan supcriptionplan = new SubscriptionPlan();
                            supcriptionplan.Show();
                        }
                        else if (dtMain.Rows[0]["Status"].ToString() == "205")
                        {
                            ErrorModel errormdl = new ErrorModel();
                            errormdl.DateLog = DateTime.Now.ToString();
                            errormdl.Message = "Your Weight bridge Licence Expired Last days";
                            minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);
                            System.Windows.Forms.MessageBox.Show("Your Weight bridge Licence Expired Last days", "Licence", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Hide();
                            SubscriptionPlan supcriptionplan = new SubscriptionPlan();
                            supcriptionplan.Show();
                        }
                        else
                        {
                            dtMain = new DataTable();
                            settingModal.SsoId = Convert.ToString(WebAPIModelResponse.SsoId);
                            settingModal.weightbridgeNo = Convert.ToString(WebAPIModelResponse.Weightbridgeno);
                            settingModal.CompanyId = Convert.ToInt64(WebAPIModelResponse.CompanyId);
                            settingModal.ClientId = Convert.ToInt64(WebAPIModelResponse.ClientId);
                            settingModal.UserId = Convert.ToInt64(WebAPIModelResponse.Userid);
                            settingModal.PostUrl = "/Api/ErawaanaAPI/setgetsettingdetails/";
                            dtMain = WebAPI.PostSettingInfoDetail(settingModal.PostUrl, settingModal).Result;
                            if (dtMain.Rows[0]["Status"].ToString() == "200")
                            {
                                WebAPIModelResponse.CameraRearUrl = dtMain.Rows[0]["CameraRearUrl"].ToString();
                                WebAPIModelResponse.CameraFrontUrl = dtMain.Rows[0]["CameraFrontUrl"].ToString();
                                WebAPIModelResponse.CombPortName = dtMain.Rows[0]["PortName"].ToString();

                                login.SSoid = CheckEmailstr;
                                login.Authtoken = checkcon.GetMacAddress();
                                login.PostUrl = "/Api/ErawaanaAPI/GetlogProileDetails/";
                                dt = WebAPI.PostGetlogProileDetails(login.PostUrl, login).Result;
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    WebAPIModelResponse.LoginUserEmailId = dt.Rows[0]["EmailID"].ToString();
                                    WebAPIModelResponse.FirmName = dt.Rows[0]["FullName"].ToString();
                                }
                                Properties.Settings.Default.UserSSOID = Convert.ToString(dt.Rows[0]["UserName"].ToString());
                                Properties.Settings.Default.Password = Comman.Encrypt(dt.Rows[0]["UserPassword"].ToString());
                                Properties.Settings.Default.SettingsKey = checkcon.GetMacAddress();
                                Properties.Settings.Default.FirstName = dt.Rows[0]["UserName"].ToString();
                                Properties.Settings.Default.Save();
                                this.Hide();
                                MineMart fnpg = new MineMart();
                                fnpg.WindowState = FormWindowState.Maximized;
                                fnpg.Show();
                            }
                            else
                            {
                                WebAPIModelResponse.CameraRearUrl = "rtsp://admin:12345@192.168.1.210:554/Streaming/Channels/101";
                                WebAPIModelResponse.CameraFrontUrl = "rtsp://admin:12345@192.168.1.210:554/Streaming/Channels/101";
                                login.SSoid = CheckEmailstr;
                                login.Authtoken = checkcon.GetMacAddress();
                                login.PostUrl = "/Api/ErawaanaAPI/GetlogProileDetails/";
                                dt = WebAPI.PostGetlogProileDetails(login.PostUrl, login).Result;
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    WebAPIModelResponse.LoginUserEmailId = dt.Rows[0]["EmailID"].ToString();
                                    WebAPIModelResponse.FirmName = dt.Rows[0]["FullName"].ToString();
                                }
                                Properties.Settings.Default.UserSSOID = Convert.ToString(dt.Rows[0]["UserName"].ToString());
                                Properties.Settings.Default.Password = Comman.Encrypt(dt.Rows[0]["UserPassword"].ToString());
                                Properties.Settings.Default.SettingsKey = checkcon.GetMacAddress();
                                Properties.Settings.Default.FirstName = dt.Rows[0]["UserName"].ToString();
                                Properties.Settings.Default.Save();
                                List<String> tList = new List<String>();
                                string[] portNames = SerialPort.GetPortNames();
                                foreach (var portName in portNames)
                                {
                                    tList.Add(portName);
                                }
                                if (tList.Contains(WebAPIModelResponse.CombPortName))
                                {
                                    this.Hide();
                                    MineMart childForm = new MineMart();
                                    childForm.WindowState = FormWindowState.Maximized;
                                    childForm.Show();
                                }
                                else
                                {
                                    ErrorModel errormdl = new ErrorModel();
                                    errormdl.DateLog = DateTime.Now.ToString();
                                    errormdl.Message = "Combport setting Change Please Change Setting first";
                                    minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);
                                    System.Windows.Forms.MessageBox.Show("Combport setting Change Please Change Setting first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    this.Hide();
                                    MineMart fnpg = new MineMart();
                                    fnpg.WindowState = FormWindowState.Maximized;
                                    fnpg.Show();

                                }
                            }
                        }
                    }

                }
            }
        }




        /// <summary>
        /// Returns URI-safe data with a given input length.
        /// </summary>
        /// <param name="length">Input length (nb. output will be longer)</param>
        /// <returns></returns>
        public static string randomDataBase64url(uint length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[length];
            rng.GetBytes(bytes);
            return base64urlencodeNoPadding(bytes);
        }

        /// <summary>
        /// Returns the SHA256 hash of the input string.
        /// </summary>
        /// <param name="inputStirng"></param>
        /// <returns></returns>
        public static byte[] sha256(string inputStirng)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(inputStirng);
            SHA256Managed sha256 = new SHA256Managed();
            return sha256.ComputeHash(bytes);
        }

        /// <summary>
        /// Base64url no-padding encodes the given input buffer.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string base64urlencodeNoPadding(byte[] buffer)
        {
            string base64 = Convert.ToBase64String(buffer);

            // Converts base64 to base64url.
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            // Strips padding.
            base64 = base64.Replace("=", "");

            return base64;
        }



        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Return))
            {
                checkLoginResult();
            }
        }

        private bool IsEnterSeen = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                WebAPIModelResponse.LanguageSetting = Convert.ToString("0");
                //UpdateLanguageMenus();
                if (WebAPIModelResponse.LanguageSetting != null && WebAPIModelResponse.LanguageSetting != "")
                {
                    if (WebAPIModelResponse.LanguageSetting == "0")
                    {
                        WebAPIModelResponse.LanguageSetting = "0";
                        ResXResourceReader rsxr = new ResXResourceReader(AppDomain.CurrentDomain.BaseDirectory + @"\\Resource\\ResEnglish.resx");
                        foreach (DictionaryEntry d in rsxr)
                        {
                            if (d.Key.ToString() == "label2.Text")
                                label2.Text = d.Value.ToString();
                            else if (d.Key.ToString() == "label3.Text")
                                label3.Text = d.Value.ToString();
                            else if (d.Key.ToString() == "btnLogin.Text")
                                btnLogin.Text = d.Value.ToString();

                        }
                    }


                }
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                WebAPIModelResponse.LanguageSetting = Convert.ToString("1");
                //UpdateLanguageMenus();
                if (WebAPIModelResponse.LanguageSetting != null && WebAPIModelResponse.LanguageSetting != "")
                {
                    if (WebAPIModelResponse.LanguageSetting == "1")
                    {
                        ResXResourceReader rsxr = new ResXResourceReader(AppDomain.CurrentDomain.BaseDirectory + @"\\Resource\\ResHindi.resx");
                        foreach (DictionaryEntry d in rsxr)
                        {
                            if (d.Key.ToString() == "label2.Text")
                                label2.Text = d.Value.ToString();
                            else if (d.Key.ToString() == "label3.Text")
                                label3.Text = d.Value.ToString();
                            else if (d.Key.ToString() == "btnLogin.Text")
                                btnLogin.Text = d.Value.ToString();

                        }
                    }
                }
            }
        }
    }
}

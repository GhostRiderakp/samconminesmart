using DeviceId;
using ERawaana.Helper;
using minesmart.DGMS;
using minesmart.Helper;
using minesmart.ViewModels;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace minesmart
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static String ErrorFilePath = @"\ErrorLog\minesmart.txt";
        public static System.IO.StreamWriter objWriter;
        public static string applicationPath = System.Windows.Forms.Application.StartupPath;
        public static string LogFolderPath = applicationPath + ErrorFilePath;
        static string weightbreidge = Properties.Settings.Default.WeightbridgeNo;
        public static Boolean openDashboard { get; set; }
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [STAThread]
        static void Main()
        {
            //CultureManager.ApplicationUICulture = CultureInfo.CurrentCulture;
            var IsCheckInternet = false;
            var Datasource = string.Empty;
            var Initialcatalog = string.Empty;
            var UserId = string.Empty;
            var Password = string.Empty;
            IsCheckInternet = IsInternetAvailable();
            try
            {
                if (IsCheckInternet)
                {


                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    Application.ThreadException += ApplicationThreadException;
                    AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
                    bool IsSucess = false;
                    if (Properties.Settings.Default.UserSSOID != string.Empty && Properties.Settings.Default.Password != string.Empty)
                    {
                        Process currentProcess = Process.GetCurrentProcess();
                        Process[] processItems = Process.GetProcessesByName(currentProcess.ProcessName);
                        IsSucess = checkLoginResult();
                        if (IsSucess)
                            Application.Run(new MineMart());
                        else
                            Application.Run(new Form1());
                    }
                    else
                        Application.Run(new Form1());

                }
                else
                {
                    DialogResult d = System.Windows.Forms.MessageBox.Show("Internet May Not be Available", "Internet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    if (d == DialogResult.OK)
                    {

                        System.Windows.Forms.Application.Exit();
                        System.Windows.Forms.Application.ExitThread();
                        Process currentProcess = Process.GetCurrentProcess();
                        Process[] processItems = Process.GetProcessesByName(currentProcess.ProcessName);
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                DataTable dtJsonMessage = new DataTable();
                dtJsonMessage = new DataTable();
                dtJsonMessage.Columns.Add("Status", typeof(string));
                dtJsonMessage.Columns.Add("Message", typeof(string));
                dtJsonMessage.Columns.Add("MessageDiscription", typeof(string));
                DataRow dr = dtJsonMessage.NewRow();
                dr = dtJsonMessage.NewRow();
                dr["Message"] = ex.Message;
                //dr["MessageDiscription"] = ex.InnerException.ToString();
                dr["Status"] = "212";
                dtJsonMessage.Rows.Add(dr);
                //minesmart.Helper.ErrorLog.WriteSettingLog(ex, "custommessage", "");
                System.Windows.Forms.MessageBox.Show("Our Server is down !.Please contact support for further Information..!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }


        public static Process PriorProcess()
        {
            try
            {
                Process curr = Process.GetCurrentProcess();
                Process[] procs = Process.GetProcessesByName(curr.ProcessName);
                foreach (Process p in procs)
                {
                    if ((p.Id != curr.Id) &&
                        (p.MainModule.FileName == curr.MainModule.FileName))
                        return p;
                }
            }
            catch (Exception ex)
            {
                DataTable dtJsonMessage = new DataTable();
                dtJsonMessage = new DataTable();
                dtJsonMessage.Columns.Add("Status", typeof(string));
                dtJsonMessage.Columns.Add("Message", typeof(string));
                dtJsonMessage.Columns.Add("MessageDiscription", typeof(string));
                DataRow dr = dtJsonMessage.NewRow();
                dr = dtJsonMessage.NewRow();
                dr["Message"] = ex.Message;
                //dr["MessageDiscription"] = ex.InnerException.ToString();
                dr["Status"] = "212";
                dtJsonMessage.Rows.Add(dr);
                //minesmart.Helper.ErrorLog.WriteSettingLog(ex, "custommessage", "");
                System.Windows.Forms.MessageBox.Show(dtJsonMessage.Rows[0]["Message"].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return null;
        }
        public static bool IsInternetAvailable()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }


        public static bool checkLoginResult()
        {
            DataTable dt = new DataTable();
            DataTable dtn = new DataTable();
            DataTable dtapi = new DataTable();
            SettingModel settingModal = new SettingModel();
            DealerModel dealerListmdl = new DealerModel();
            Login login = new Login();
            CheckConnection checkcon = new CheckConnection();
            WebAPIModelResponse wb = new WebAPIModelResponse();
            Exception ewx = new Exception();
            var FResult = false;
            if (Properties.Settings.Default.UserSSOID != "" && Properties.Settings.Default.Password != "")
            {
                try
                {
                    login.SSoUrl = "/Api/ErawaanaAPI/getCheckApiWorking/";
                    dtapi = WebAPI.PostCheckApiWorking(login.SSoUrl, login).Result;
                    if (dtapi != null && dtapi.Rows.Count > 0)
                    {
                        if (dtapi.Rows[0]["Status"].ToString() == "200")
                        {

                            if (Properties.Settings.Default.SettingsKey != "")
                            {
                                WebAPIModelResponse.Authtoken = Properties.Settings.Default.SettingsKey;
                                login.Authtoken = Properties.Settings.Default.SettingsKey;
                                login.SSoUrl = "/Api/ErawaanaAPI/CheckMachAddressExist/";
                                dtn = WebAPI.PostCheckMachAddressExist(login.SSoUrl, login).Result;
                                if (dtn != null && dtn.Rows.Count > 0)
                                {
                                    if (dtn.Rows[0]["Status"].ToString() == "200")
                                    {
                                        if (dtn.Rows[0]["UniqueSystem"].ToString() == "1")
                                        {
                                            WebAPIModelResponse.UserProfileid = dtn.Rows[0]["UserPofileId"].ToString();
                                            WebAPIModelResponse.UserCredtentialId = dtn.Rows[0]["UserCredtentialId"].ToString();
                                            //Check UserName Exist 
                                            login.SSoid = dtn.Rows[0]["UserName"].ToString();
                                            login.Password = dtn.Rows[0]["UserPassword"].ToString();
                                            WebAPIModelResponse.LoginUserEmailId = dtn.Rows[0]["EmailID"].ToString();
                                            //WebAPIModelResponse.FirmName = dtn.Rows[0]["UserName"].ToString();
                                            login.Authtoken = Properties.Settings.Default.SettingsKey;
                                            WebAPIModelResponse.Authtoken = Properties.Settings.Default.SettingsKey;
                                            login.Weightbridge = weightbreidge.ToString() == null ? "00844" : weightbreidge.ToString();
                                            login.SSoUrl = "/Api/ErawaanaAPI/CheckUserNameExist/";
                                            dtn = WebAPI.PostCheckUserNameExist(login.SSoUrl, login).Result;
                                            if (dtn.Rows[0]["Status"].ToString() == "200")
                                            {
                                                //Check User Name Password Exist
                                                //Check UserName Exist 

                                                login.SSoid = dtn.Rows[0]["UserName"].ToString();
                                                login.Password = Comman.Decrypt(dtn.Rows[0]["UserPassword"].ToString());
                                                login.Authtoken = checkcon.GetMacAddress();
                                                WebAPIModelResponse.Authtoken = checkcon.GetMacAddress();
                                                login.Weightbridge = weightbreidge.ToString() == null ? "00844" : weightbreidge.ToString();
                                                login.SSoUrl = "/Api/ErawaanaAPI/CheckPasswordExist/";
                                                dtn = WebAPI.PostCheckPasswordExist(login.SSoUrl, login).Result;
                                                if (dtn.Rows[0]["Status"].ToString() == "200")
                                                {

                                                    //Checking User Name and Password Valid

                                                    login.Password = Comman.Decrypt(dtn.Rows[0]["UserPassword"].ToString());
                                                    login.Authtoken = checkcon.GetMacAddress();
                                                    WebAPIModelResponse.Authtoken = checkcon.GetMacAddress();
                                                    login.Weightbridge = weightbreidge.ToString() == null ? "00844" : weightbreidge.ToString();
                                                    login.SSoUrl = "/Api/ErawaanaAPI/CheckUserPasswordExist/";
                                                    dtn = WebAPI.PostCheckUserPasswordExist(login.SSoUrl, login).Result;
                                                    if (dtn.Rows[0]["Status"].ToString() == "210")
                                                    {

                                                        ErrorModel errormdl = new ErrorModel();
                                                        errormdl.DateLog = DateTime.Now.ToString();
                                                        errormdl.Message = "Your Account is not active.Please Contact your Administrator";
                                                        minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);
                                                        System.Windows.Forms.MessageBox.Show("User Not Register Contact to Adminstrator", "Registered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    }
                                                    else
                                                    {
                                                        WebAPIModelResponse.SsoId = dtn.Rows[0]["SsoId"].ToString();
                                                        WebAPIModelResponse.Password = dtn.Rows[0]["Password"].ToString();
                                                        WebAPIModelResponse.Weightbridgeno = dtn.Rows[0]["WeighBridgeNo"].ToString();
                                                        WebAPIModelResponse.Accesskey = dtn.Rows[0]["AccessKey"].ToString();
                                                        WebAPIModelResponse.ActiveKey = dtn.Rows[0]["ActiveKey"].ToString();
                                                        WebAPIModelResponse.Userid = dtn.Rows[0]["UserId"].ToString();
                                                        WebAPIModelResponse.CompanyId = Convert.ToInt64(dtn.Rows[0]["CompanyId"].ToString());
                                                        WebAPIModelResponse.ClientId = Convert.ToInt64(dtn.Rows[0]["ClientId"].ToString());
                                                        WebAPIModelResponse.Provider = dtn.Rows[0]["Provider"].ToString();

                                                        //Checking User supcription plan expired or not
                                                        login.clientId = Convert.ToInt64(WebAPIModelResponse.ClientId);
                                                        login.Weightbridge = Convert.ToString(WebAPIModelResponse.Weightbridgeno);
                                                        login.SSoUrl = "/Api/ErawaanaAPI/CheckUserExpired/";
                                                        dtn = WebAPI.PostCheckUserExpired(login.SSoUrl, login).Result;
                                                        if (dtn.Rows[0]["Status"].ToString() == "200")
                                                        {
                                                            ErrorModel errormdl = new ErrorModel();
                                                            errormdl.DateLog = DateTime.Now.ToString();
                                                            errormdl.Message = "Your Weight bridge Licence Expired Last days";
                                                            minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);

                                                            SubscriptionPlan supcriptionplan = new SubscriptionPlan();
                                                            supcriptionplan.Show();
                                                        }
                                                        else if (dtn.Rows[0]["Status"].ToString() == "205")
                                                        {
                                                            ErrorModel errormdl = new ErrorModel();
                                                            errormdl.DateLog = DateTime.Now.ToString();
                                                            errormdl.Message = "Your Weight bridge Licence Expired Last days";
                                                            minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);
                                                            System.Windows.Forms.MessageBox.Show("Your Weight bridge Licence Expired Last days", "Licence", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                                            SubscriptionPlan supcriptionplan = new SubscriptionPlan();
                                                            supcriptionplan.Show();
                                                        }
                                                        else
                                                        {
                                                            dt = new DataTable();

                                                            login.Userid = Convert.ToInt64(WebAPIModelResponse.UserCredtentialId);
                                                            login.Authtoken = checkcon.GetMacAddress();
                                                            login.SSoUrl = "/Api/ErawaanaAPI/CheckAlreadyUserLogin/";
                                                            dt = WebAPI.Postcheckalreadyuserlogin(login.SSoUrl, login).Result;
                                                            if (dt.Rows[0]["Status"].ToString() == "200")
                                                            {
                                                                try
                                                                {
                                                                    dt = new DataTable();
                                                                    settingModal.SsoId = Convert.ToString(WebAPIModelResponse.SsoId);
                                                                    settingModal.weightbridgeNo = Convert.ToString(WebAPIModelResponse.Weightbridgeno);
                                                                    settingModal.CompanyId = Convert.ToInt64(WebAPIModelResponse.CompanyId);
                                                                    settingModal.ClientId = Convert.ToInt64(WebAPIModelResponse.ClientId);
                                                                    settingModal.UserId = Convert.ToInt64(WebAPIModelResponse.Userid);
                                                                    settingModal.PostUrl = "/Api/ErawaanaAPI/setgetsettingdetails/";
                                                                    dt = WebAPI.PostSettingInfoDetail(settingModal.PostUrl, settingModal).Result;
                                                                    if (dt.Rows[0]["Status"].ToString() == "200")
                                                                    {
                                                                        WebAPIModelResponse.CameraRearUrl = dt.Rows[0]["CameraRearUrl"].ToString();
                                                                        WebAPIModelResponse.CameraFrontUrl = dt.Rows[0]["CameraFrontUrl"].ToString();
                                                                        WebAPIModelResponse.CombPortName = dt.Rows[0]["PortName"].ToString();
                                                                        dt = new DataTable();
                                                                        login.LanguageSetting = Properties.Settings.Default.DefaultLanguage;
                                                                        login.PostUrl = "/Api/ErawaanaAPI/SetLanguageSetting/";
                                                                        dt = WebAPI.PostLanguageSetting(login.PostUrl, login).Result;
                                                                        if (dt != null && dt.Rows.Count > 0)
                                                                        {
                                                                            WebAPIModelResponse.LoginUserEmailId = dt.Rows[0]["EmailID"].ToString();
                                                                            WebAPIModelResponse.FirmName = dt.Rows[0]["FullName"].ToString();
                                                                        }

                                                                        FResult = true;
                                                                    }
                                                                    else
                                                                    {
                                                                        dt = new DataTable();
                                                                        login.LanguageSetting = Properties.Settings.Default.DefaultLanguage;
                                                                        login.PostUrl = "/Api/ErawaanaAPI/SetLanguageSetting/";
                                                                        dt = WebAPI.PostLanguageSetting(login.PostUrl, login).Result;
                                                                        if (dt != null && dt.Rows.Count > 0)
                                                                        {
                                                                            WebAPIModelResponse.LoginUserEmailId = dt.Rows[0]["EmailID"].ToString();
                                                                            WebAPIModelResponse.FirmName = dt.Rows[0]["FullName"].ToString();
                                                                        }
                                                                        WebAPIModelResponse.IsSubmitsetting = "0";
                                                                        WebAPIModelResponse.CameraRearUrl = "rtsp://admin:12345@192.168.1.210:554/Streaming/Channels/101";
                                                                        WebAPIModelResponse.CameraFrontUrl = "rtsp://admin:12345@192.168.1.210:554/Streaming/Channels/101";
                                                                        Properties.Settings.Default.UserSSOID = Convert.ToString(WebAPIModelResponse.SsoId);
                                                                        Properties.Settings.Default.Password = Comman.Encrypt(WebAPIModelResponse.Password.Trim());
                                                                        Properties.Settings.Default.SettingsKey = checkcon.GetMacAddress();
                                                                        Properties.Settings.Default.FirstName = WebAPIModelResponse.FirmName;
                                                                        Properties.Settings.Default.IsSubmitsetting = "0";
                                                                        Properties.Settings.Default.Save();
                                                                        List<String> tList = new List<String>();
                                                                        string[] portNames = SerialPort.GetPortNames();
                                                                        foreach (var portName in portNames)
                                                                        {
                                                                            tList.Add(portName);
                                                                        }
                                                                        if (tList.Contains(WebAPIModelResponse.CombPortName))
                                                                        {
                                                                        }
                                                                        else
                                                                        {
                                                                            ErrorModel errormdl = new ErrorModel();
                                                                            errormdl.DateLog = DateTime.Now.ToString();
                                                                            errormdl.Message = "Combport setting Change Please Change Setting first";
                                                                            minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);
                                                                            System.Windows.Forms.MessageBox.Show("Combport setting Change Please Change Setting first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                                        }
                                                                        FResult = true;
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
                                                            else
                                                            {
                                                                FResult = false;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    FResult = false;
                                                    //Log.Logger = new LoggerConfiguration()
                                                    //.WriteTo.Async(a =>
                                                    //{
                                                    //    a.File(LogFolderPath + "logs.log", rollingInterval: RollingInterval.Hour); // <<<<<
                                                    //})
                                                    //.MinimumLevel.Verbose()
                                                    //.CreateLogger();
                                                }
                                            }
                                            else
                                            {
                                                FResult = false;
                                                System.Windows.Forms.MessageBox.Show("User Name does't Exist", "User Name");
                                            }
                                        }
                                        else
                                            FResult = false;
                                    }
                                    else
                                        FResult = false;
                                }
                                else
                                    FResult = false;
                            }
                            else

                                FResult = false;
                        }
                        else
                        {
                            DialogResult d = System.Windows.Forms.MessageBox.Show("Some Change in our software Please Contact your Sale Teamdial", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    DataTable dtJsonMessage = new DataTable();
                    dtJsonMessage = new DataTable();
                    dtJsonMessage.Columns.Add("Status", typeof(string));
                    dtJsonMessage.Columns.Add("Message", typeof(string));
                    dtJsonMessage.Columns.Add("MessageDiscription", typeof(string));
                    dtJsonMessage.Columns.Add("FunctionName", typeof(string));
                    dtJsonMessage.Columns.Add("LineNumber", typeof(string));
                    DataRow dr = dtJsonMessage.NewRow();
                    dr = dtJsonMessage.NewRow();
                    dr["Status"] = "201" + checkcon.GetMacAddress();
                    dr["Message"] = ex.Message;
                    dr["MessageDiscription"] = ex.InnerException.ToString();
                    dr["FunctionName"] = "checkLoginResult()";
                    dr["LineNumber"] = ErrorLog.GetLineNumber(ex);
                    dtJsonMessage.Rows.Add(dr);
                    minesmart.Helper.ErrorLog.SaveErrorLogtabledt(dtJsonMessage);
                    System.Windows.Forms.MessageBox.Show("Our Server is down Please Contact Our Sales Team", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please Enter User Name and Password", "Login");
            }
            return FResult;
        }

        #region Application Bug Message

        /// <summary>
        /// Global exceptions in Non User Interface (other thread) handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var message =
                String.Format(
                    "Sorry, something went wrong.\r\n" + "{0}\r\n" + "{1}\r\n" + "Please contact support.",
                    ((Exception)e.ExceptionObject).Message, ((Exception)e.ExceptionObject).StackTrace);
            MessageBox.Show(message, @"Unexpected error");
        }

        /// <summary>
        /// Global exceptions in User Interface handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            var message =
                String.Format(
                    "Sorry, something went wrong.\r\n" + "{0}\r\n" + "{1}\r\n" + "Please contact support.",
                    e.Exception.Message, e.Exception.StackTrace);
            MessageBox.Show(message, @"Unexpected error");
        }

        #endregion
    }
}

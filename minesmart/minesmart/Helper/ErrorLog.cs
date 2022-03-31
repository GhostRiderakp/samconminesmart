using ERawaana.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesmart.Helper
{
    public class ErrorLog
    {
        static string sDay = DateTime.Now.Day.ToString();
        static string smonth = DateTime.Now.Month.ToString();
        static string syear = DateTime.Now.Year.ToString();
        public static string applicationPath = Application.StartupPath;
        public static string LogFolderPath = applicationPath + "\\ErrorLog";
        public static string LogSettingPath = applicationPath + "Setting";
        public static string WriteLog(string msg, string type)
        {
            string filepath = string.Empty;
            string sLogFormat = "------------------------------------------" + Environment.NewLine;
            if (type == log_error_type["customerror"])
                sLogFormat += "Custom Error- ";
            if (type == log_error_type["exception"])
                sLogFormat += "Exception- ";
            if (type == log_error_type["custommessage"])
                sLogFormat += "Custom Message- ";

            sLogFormat += DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(DateTime.Now.ToString("h:mm:ss tt")) + " ";

            StackFrame callStack = new StackFrame(1, true);
            string callerdetails = "File:" + callStack.GetFileName() + ", Line: " + callStack.GetFileLineNumber();
            sLogFormat += callerdetails + " ";

            sLogFormat += Environment.NewLine;
            try
            {
                string logFileDirectory = LogFolderPath;
                // logFileDirectory = logFileDirectory + "\\GenerateRawannaLog";

                if (!Directory.Exists(logFileDirectory))
                {
                    Directory.CreateDirectory(logFileDirectory);
                }
                string logFileName = logFileDirectory + "\\Setting.log";
                filepath = logFileName;
                StreamWriter sw;
                if (!File.Exists(logFileName))
                {
                    sw = new StreamWriter(logFileName, true);
                }
                else
                {
                    sw = File.AppendText(logFileName);
                }
                sw.WriteLine(sLogFormat + msg);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }
            return filepath;
        }
        public static Dictionary<string, string> log_error_type = new Dictionary<string, string>()
        {
            {"custommessage","Custom-Message"},
            {"customerror","Custom-Error"},
            {"exception","Exception"}
        };

        public static string WriteSettingLog(Exception except, string type, string Cmsg)
        {
            string filepath = string.Empty;
            StreamWriter sw;

            string sLogFormat = "------------------------------------------" + Environment.NewLine;
            if (type == log_error_type["custommessage"])
                sLogFormat += "Solution Details:-";
            sLogFormat += DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(DateTime.Now.ToString("h:mm:ss tt")) + " ";
            StackFrame callStack = new StackFrame(1, true);
            string callerdetails = "File:" + callStack.GetFileName() + ", Line: " + callStack.GetFileLineNumber();
            sLogFormat += callerdetails + " ";
            sLogFormat += Environment.NewLine;


            try
            {
                string logFileDirectory = LogSettingPath;
                if (Directory.Exists(logFileDirectory))
                {
                    foreach (string file in Directory.GetFiles(logFileDirectory))
                    {
                        File.Delete(file);
                    }
                    Thread.Sleep(1);
                    Directory.Delete(logFileDirectory);
                }
                Thread.Sleep(1);
                if (!Directory.Exists(logFileDirectory))
                {
                    Directory.CreateDirectory(logFileDirectory);
                }

                string logFileName = logFileDirectory + "\\ErrorText.txt";
                filepath = logFileName;

                if (!File.Exists(logFileName))
                {
                    sw = new StreamWriter(logFileName, true);
                }
                else
                {
                    sw = File.AppendText(logFileName);
                }
                sw.WriteLine("===========================================");
                sw.WriteLine(sLogFormat + except.Message.ToString());
                sw.WriteLine("===========================================");
                sw.WriteLine(Cmsg);
                sw.Flush();
                sw.Close();
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }
            return filepath;
        }
        public void SendErrorReport(Exception exc)
        {
            try
            {
                string filepath = string.Empty;
                string sLogFormat = "------------------------------------------" + Environment.NewLine;

                sLogFormat += Environment.NewLine;
                sLogFormat += DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(DateTime.Now.ToString("h:mm:ss tt")) + " ";

                StackFrame callStack = new StackFrame(1, true);
                string callerdetails = "File:" + callStack.GetFileName() + ", Line: " + callStack.GetFileLineNumber();
                sLogFormat += callerdetails + " ";

                sLogFormat += Environment.NewLine;
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("nilusilu3@gmail.com");
                //msg.To.Add(textBox1.Text);//
                msg.Subject = "Error Message";
                msg.Body = sLogFormat + Environment.NewLine + exc;

                SmtpClient smt = new SmtpClient();
                smt.Host = "smtp.gmail.com";
                System.Net.NetworkCredential ntcd = new NetworkCredential();
                ntcd.UserName = "nilusilu3@gmail.com";
                ntcd.Password = "";
                smt.Credentials = ntcd;
                smt.EnableSsl = true;
                smt.Port = 587;
                smt.Send(msg);

                MessageBox.Show("Your Mail is sended");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public static void SaveErrorLogtable(minesmart.ViewModels.ErrorModel error)
        {
            try
            {
                StackFrame callStack = new StackFrame(1, true);
                string callerdetails = "File:" + callStack.GetFileName() + ", Line: " + callStack.GetFileLineNumber();
                error.FileName = callStack.GetFileName();
                error.LineNo = Convert.ToString(callStack.GetFileLineNumber());
                error.PostUrl = "/Api/ErawaanaAPI/setSaveErrorLogtable/";
                var responseDetails = WebAPI.PostSaveErrorLogtable(error.PostUrl, error).Result;
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }

        }
        public static void SaveErrorLogtableCustom(minesmart.ViewModels.ErrorModel error)
        {
            try
            {
                StackFrame callStack = new StackFrame(1, true);
                string callerdetails = "File:" + callStack.GetFileName() + ", Line: " + callStack.GetFileLineNumber();
                error.FileName = callerdetails;
                error.LineNo = Convert.ToString(callStack.GetFileLineNumber());
                error.PostUrl = "/Api/ErawaanaAPI/setSaveErrorLogtableCustome/";
                var responseDetails = WebAPI.PostSaveErrorLogtableCustom(error.PostUrl, error).Result;
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }

        }

        public static void SaveErrorLogtabledt(DataTable dtJsonMessage)
        {
            try
            {
                minesmart.ViewModels.ErrorModel error = new ViewModels.ErrorModel();
                StackFrame callStack = new StackFrame(1, true);
                string callerdetails = "File:" + callStack.GetFileName() + ", Line: " + callStack.GetFileLineNumber();
                error.ErrorCode = dtJsonMessage.Rows[0]["Status"].ToString();
                error.Message = dtJsonMessage.Rows[0]["Message"].ToString();
                error.Error = dtJsonMessage.Rows[0]["MessageDiscription"].ToString();
                error.PageName = dtJsonMessage.Rows[0]["FunctionName"].ToString();
                error.LineNo = dtJsonMessage.Rows[0]["LineNumber"].ToString();
                error.Sendtype = "dt";
                error.PostUrl = "/Api/ErawaanaAPI/setSaveErrorLogtabledt/";
                var responseDetails = WebAPI.PostSaveErrorLogtabledt(error.PostUrl, error).Result;
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }

        }

        public static int GetLineNumber(Exception ex)
        {
            var lineNumber = 0;
            const string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            if (index != -1)
            {
                var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                {
                }
            }
            return lineNumber;
        }
        public static void LogErrorToEventViewer()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("********************" + " Error Log 1 - " + DateTime.Now + "*********************");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("Exception Type : " + "Hi 1");
            sb.Append(Environment.NewLine);
            sb.Append("Error Message : " + "Hi MEssage 1");
            sb.Append(Environment.NewLine);
            sb.Append("Error Source : " + "Hi Source 1");
            sb.Append(Environment.NewLine);

            sb.Append("Error Trace : " + "Hi Source 1");

            var innerEx = "hi";
            //Exception innerEx = ex.InnerException;

            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("Exception Type : " + "Hi Source 1");
            sb.Append(Environment.NewLine);
            sb.Append("Error Message : " + "Hi Source 1");
            sb.Append(Environment.NewLine);
            sb.Append("Error Source : " + "Hi Source 1");
            sb.Append(Environment.NewLine);
            //if (ex.StackTrace != null)
            //{
            //    sb.Append("Error Trace : " + "Hi Source");
            //}
            // innerEx = innerEx.InnerException;

            if (EventLog.SourceExists("MinesmartTesting"))
            {
                EventLog eventlog = new EventLog("MinesLog");
                eventlog.Source = "MinesmartTesting";
                eventlog.WriteEntry(sb.ToString(), EventLogEntryType.Error);
            }
            else
            {
                EventLog.CreateEventSource("MinesmartTesting", "MinesLog");
                EventLog eventlog = new EventLog("MinesLog");
                eventlog.Source = "MinesmartTesting";
                eventlog.WriteEntry(sb.ToString(), EventLogEntryType.Error);
            }
        }

    }
}

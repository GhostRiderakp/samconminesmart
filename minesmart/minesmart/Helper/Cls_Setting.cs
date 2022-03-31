using ERawaana.Helper;
using ERawannaDesk.DGMS;
using ERawannaDesk.ViewModels;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERawannaDesk.Helper
{
    public class Cls_Setting
    {
        public static String ErrorFilePath = @"\ErrorLog\ERawannaDesk.txt";
        public static System.IO.StreamWriter objWriter;
        public static string applicationPath = Application.StartupPath;
        public static string LogFolderPath = applicationPath + ErrorFilePath;
        static string terminal_data = null;
        static char last_char = (char)2;
        static SerialPort myport;
         SettingModel _settingModal = new SettingModel();
        public static string Port_ready(string machineName, string portName)
        {

            // <summary>
            //  check serial port is available or not 
            // </summary>

            bool IsComPortPhysical = false;

            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                if (portName.Trim() == queryObj["PortName"].ToString())
                {
                    IsComPortPhysical = true;
                    break;
                }
            }

            // <summary>
            //  if serialport is available
            // </summary>

            if (IsComPortPhysical)
            {


                /// <summary>
                /// setting for different terminal
                /// </summary>

                switch (machineName)
                {
                    case "1":
                        myport = new SerialPort(portName, 1200, Parity.None, 8);
                        break;
                    case "2":
                        myport = new SerialPort(portName, 2400, Parity.None, 8);
                        break;
                    case "3":
                        myport = new SerialPort(portName, 1200, Parity.Odd, 7);
                        break;
                    case "4":
                        myport = new SerialPort(portName, 1200, Parity.None, 8);
                        last_char = (char)2;
                        break;
                    case "5":
                        myport = new SerialPort(portName, 9600, Parity.None, 8);
                        last_char = (char)2;
                        break;
                    case "6":
                        myport = new SerialPort(portName, 9600, Parity.None, 8);
                        break;
                    case "7":
                        myport = new SerialPort(portName, 300, Parity.Odd, 7);
                        break;
                    case "8":
                        myport = new SerialPort(portName, 300, Parity.None, 7);
                        break;
                    case "9":
                        myport = new SerialPort(portName, 19200, Parity.None, 8);
                        break;

                }
                try
                {
                    if (myport.IsOpen)
                    {
                        myport.Close();
                    }
                    myport.Open();
                }
                catch (Exception ex)
                {
                    return "Serial port problem";
                }

                myport.DataReceived += new SerialDataReceivedEventHandler(myport_DataReceived);

                return "OK";
            }
            else

                return "Selected Port is not a Physical Port, Please select a physical port.";
        }

        static void myport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if ((DMGService.MachineName.Trim() == "4") || (DMGService.MachineName.Trim() == "5"))
                {
                    terminal_data = myport.ReadTo(last_char.ToString());
                }
                else
                {
                    terminal_data = myport.ReadLine();
                }
            }
            catch (Exception ex)
            {
                terminal_data = "0";
            }

        }

        public static void close_port()
        {
            if (myport != null)
            {
                if ((myport != null) || (myport.IsOpen))
                {
                    myport.Close();
                    myport.Dispose();
                }
            }
        }
        public static string Getweight()
        {
            string wt = refine(terminal_data);

            if (DMGService.MachineName == "5")
                try
                {
                    return wt.Substring(0, 6);
                }
                catch (Exception ex)
                {
                    return "Wait";
                }
            else
                try
                {
                    return wt;
                }
                catch (Exception ex)
                {
                    return "Wait";
                }

        }
        static private string refine(string x)
        {
            // <summary>
            // this function remove the extra data like "KG" or other symbol from terminal data and send only numeric value
            // </summary>
            string bl = "";
            try
            {
                char[] m = x.ToCharArray();
                foreach (char m2 in m)
                {
                    if ((m2 >= '0') && (m2 <= '9') || (m2 == '-') || (m2 == '.'))
                        bl = bl + m2;
                }
                return bl.ToString();

            }
            catch (Exception)
            {
                return "Error";

            }
        }

      

    }
}

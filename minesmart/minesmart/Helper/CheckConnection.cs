using minesmart.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace minesmart.Helper
{
    public class CheckConnection
    {
        static string conn_str = Properties.Settings.Default.Connection_String;
        public bool CheckForInternetConnection(int timeOut = 3000)
        {
            var task = CheckForInternetConnectionTask(timeOut);

            return task.Wait(timeOut) && task.Result;
        }

        public Task<bool> CheckForInternetConnectionTask(int timeOut = 3000)
        {
            return Task.Factory.StartNew
                (() =>
                {
                    try
                    {
                        var client = (HttpWebRequest)WebRequest.Create("http://google.com/");
                        client.Method = "HEAD";
                        client.Timeout = timeOut;

                        using (var response = client.GetResponse())
                        using (response.GetResponseStream())
                        {
                            return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                });
        }

        /// <summary>
        /// Finds the MAC address of the NIC with maximum speed.
        /// </summary>
        /// <returns>The MAC address.</returns>
        public string GetMacAddress()
        {
            const int MIN_MAC_ADDR_LENGTH = 12;
            string macAddress = string.Empty;
            long maxSpeed = -1;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                //log.Debug(
                //    "Found MAC Address: " + nic.GetPhysicalAddress() +
                //    " Type: " + nic.NetworkInterfaceType);

                string tempMac = nic.GetPhysicalAddress().ToString();
                if (nic.Speed > maxSpeed &&
                    !string.IsNullOrEmpty(tempMac) &&
                    tempMac.Length >= MIN_MAC_ADDR_LENGTH)
                {
                    // log.Debug("New Max Speed = " + nic.Speed + ", MAC: " + tempMac);
                    maxSpeed = nic.Speed;
                    macAddress = tempMac;
                }
            }

            return macAddress;
        }

        /// <summary>
        /// Test that the server is connected
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <returns>true if the connection is opened</returns>
        public bool IsServerConnected()
        {
            using (SqlConnection connection = new SqlConnection(conn_str))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
        public string GetIPAddress()
        {
            string IPAddress = string.Empty;
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }
        //public  bool IsApplictionInstalled(string p_name)
        //{
        //    string displayName;
        //    RegistryKey key;

        //    // search in: CurrentUser
        //    key = Registry.CurrentUser.OpenSubKey(@"C:\Program Files\minesmart\minesmart");
        //    foreach (String keyName in key.GetSubKeyNames())
        //    {
        //        RegistryKey subkey = key.OpenSubKey(keyName);
        //        displayName = subkey.GetValue("minesmart") as string;
        //        if (p_name.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
        //        {
        //            return true;
        //        }
        //    }

        //    // search in: LocalMachine_32
        //    key = Registry.LocalMachine.OpenSubKey(@"C:\Program Files\minesmart\minesmart");
        //    foreach (String keyName in key.GetSubKeyNames())
        //    {
        //        RegistryKey subkey = key.OpenSubKey(keyName);
        //        displayName = subkey.GetValue("minesmart") as string;
        //        if (p_name.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
        //        {
        //            return true;
        //        }
        //    }

        //    // search in: LocalMachine_64
        //    key = Registry.LocalMachine.OpenSubKey(@"C:\Program Files\minesmart\minesmart");
        //    foreach (String keyName in key.GetSubKeyNames())
        //    {
        //        RegistryKey subkey = key.OpenSubKey(keyName);
        //        displayName = subkey.GetValue("minesmart") as string;
        //        if (p_name.Equals(displayName, StringComparison.OrdinalIgnoreCase) == true)
        //        {
        //            return true;
        //        }
        //    }

        //    // NOT FOUND
        //    return false;
        //}
    }
}

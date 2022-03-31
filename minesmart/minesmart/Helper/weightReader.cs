using ERawannaDesk.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Management;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

namespace ERawannaDesk.Helper
{
    public class weightReader : IDisposable
    {
        public weightReader()
        {
            // Finding installed serial ports on hardware
            _currentSerialSettings.PortNameCollection = SerialPort.GetPortNames();
            _currentSerialSettings.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_currentSerialSettings_PropertyChanged);

            // If serial ports is found, we select the first found
            if (_currentSerialSettings.PortNameCollection.Length > 0)
                _currentSerialSettings.PortName = _currentSerialSettings.PortNameCollection[0];
        }


        ~weightReader()
        {
            Dispose(false);
        }



        private SettingModel _currentSerialSettings = new SettingModel();
        private SerialPort _serialPort;
        public event EventHandler<SerialDataEventArgs> NewSerialDataRecieved;
        public int _serialPortType;
        #region
        /// <summary>
        /// Gets or sets the current serial port settings
        /// </summary>
        public SettingModel CurrentSerialSettings
        {
            get { return _currentSerialSettings; }
            set { _currentSerialSettings = value; }
        }

        #endregion
        // Call to release serial port
        public void Dispose()
        {
            Dispose(true);
        }
        // Part of basic design pattern for implementing Dispose
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing && _serialPort != null)
                {
                    _serialPort.DataReceived -= new SerialDataReceivedEventHandler(_serialPort_DataReceived1);
                }
                // Releasing serial port (and other unmanaged objects)
                if (_serialPort != null)
                {
                    if (_serialPort.IsOpen)
                        _serialPort.Close();

                    _serialPort.Dispose();
                }
            }
            catch (Exception ex)
            {

                if (_serialPort != null && _serialPort.IsOpen)
                    _serialPort.Close();
                ErrorModel errormdl = new ErrorModel();
                if (ex.Message.ToString().Contains("Status"))
                {
                    var parsed = JObject.Parse(ex.Message.ToString());
                    errormdl.DateLog = DateTime.Now.ToString();
                    //errormdl.Exp = parsed.SelectToken("MessageDiscription"  .Value<string>();
                    errormdl.Message = parsed.SelectToken("MessageDiscription").Value<string>();
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                else
                {
                    errormdl.DateLog = DateTime.Now.ToString();
                    errormdl.Exp = ex;
                    errormdl.Message = ex.Message;
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
            }
        }
        int lengthOfTheData = 0;
        string TotalData;

        void _serialPort_DataReceived1(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                if (_serialPortType == 1)
                {

                    int dataLength = _serialPort.BytesToRead;
                    MessageBox.Show(dataLength.ToString());
                    byte[] data = new byte[dataLength];
                    int nbrDataRead = _serialPort.Read(data, 0, dataLength);
                    if (nbrDataRead == 0)
                        return;
                    string x = Encoding.ASCII.GetString(data);
                    TotalData += x;
                    lengthOfTheData += x.Length;
                    if (lengthOfTheData >= 20)
                    {
                        callRefe(TotalData);
                        lengthOfTheData = 0;
                        TotalData = "";
                    }

                }
                else
                {

                    string data = string.Empty;

                    while (_serialPort.BytesToRead > 0)
                    {
                        try
                        {
                            data = _serialPort.ReadLine();
                        }
                        catch (Exception ex)
                        {
                            break;
                        }
                    }
                    callRefe(data);
                }
            }
            catch (Exception ex)
            {

                if (_serialPort != null && _serialPort.IsOpen)
                    _serialPort.Close();
                ErrorModel errormdl = new ErrorModel();
                if (ex.Message.ToString().Contains("Status"))
                {
                    var parsed = JObject.Parse(ex.Message.ToString());
                    errormdl.DateLog = DateTime.Now.ToString();
                    //errormdl.Exp = parsed.SelectToken("MessageDiscription"  .Value<string>();
                    errormdl.Message = parsed.SelectToken("MessageDiscription").Value<string>();
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                else
                {
                    errormdl.DateLog = DateTime.Now.ToString();
                    errormdl.Exp = ex;
                    errormdl.Message = ex.Message;
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
            }

        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        void callRefe(string data)
        {
            try
            {
                string weighBridgeRegex = WebAPIModelResponse.ReaderCode;//Properties.Settings.Default["weighBridgeReader"].ToString();
                string isReversedDrop = WebAPIModelResponse.Isreversed;//Properties.Settings.Default["isReversedDrop"].ToString();
                Regex regex = new Regex(weighBridgeRegex);

                if (isReversedDrop == "Yes")
                {
                    data = Reverse(data);
                }

                Match match = regex.Match(data);
                if (match.Success)
                {
                    if (NewSerialDataRecieved != null)
                        NewSerialDataRecieved(this, new SerialDataEventArgs(match.Value));
                }
                else
                {
                    //MessageBox.Show("Couldn't get Data, Original data = " + data);
                }
                //if (NewSerialDataRecieved != null)
                //    NewSerialDataRecieved(this, new SerialDataEventArgs(data));
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
                throw er;
                ErrorModel errormdl = new ErrorModel();
                if (er.Message.ToString().Contains("Status"))
                {
                    var parsed = JObject.Parse(er.Message.ToString());
                    errormdl.DateLog = DateTime.Now.ToString();
                    //errormdl.Exp = parsed.SelectToken("MessageDiscription"  .Value<string>();
                    errormdl.Message = parsed.SelectToken("MessageDiscription").Value<string>();
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                else
                {
                    errormdl.DateLog = DateTime.Now.ToString();
                    errormdl.Exp = er;
                    errormdl.Message = er.Message;
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
            }
        }
        /// Closes the serial port
        /// </summary>
        public void StopListening()
        {
            _serialPort.Close();
        }

        void _currentSerialSettings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // if serial port is changed, a new baud query is issued
            if (e.PropertyName.Equals("PortName"))
                UpdateBaudRateCollection();
        }
        /// <summary>
        /// Retrieves the current selected device's COMMPROP structure, and extracts the dwSettableBaud property
        /// </summary>
        private void UpdateBaudRateCollection()
        {
            try
            {
                _serialPort = new SerialPort(_currentSerialSettings.PortName);
                _serialPort.Open();
                object p = _serialPort.BaseStream.GetType().GetField("commProp", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_serialPort.BaseStream);
                Int32 dwSettableBaud = (Int32)p.GetType().GetField("dwSettableBaud", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(p);

                _serialPort.Close();
                //_currentSerialSettings.UpdateBaudRateCollection(dwSettableBaud);
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
                throw er;
                ErrorModel errormdl = new ErrorModel();
                if (er.Message.ToString().Contains("Status"))
                {
                    var parsed = JObject.Parse(er.Message.ToString());
                    errormdl.DateLog = DateTime.Now.ToString();
                    //errormdl.Exp = parsed.SelectToken("MessageDiscription"  .Value<string>();
                    errormdl.Message = parsed.SelectToken("MessageDiscription").Value<string>();
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                else
                {
                    errormdl.DateLog = DateTime.Now.ToString();
                    errormdl.Exp = er;
                    errormdl.Message = er.Message;
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
            }
        }

        public void OpenPort()
        {
            try
            {
                //must keep it open to maintain connection (CTS)
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                    _serialPort.RtsEnable = true;
                }
            }
            catch (Exception ex)
            {
                ErrorModel errormdl = new ErrorModel();
                if (ex.Message.ToString().Contains("Status"))
                {
                    var parsed = JObject.Parse(ex.Message.ToString());
                    errormdl.DateLog = DateTime.Now.ToString();
                    //errormdl.Exp = parsed.SelectToken("MessageDiscription"  .Value<string>();
                    errormdl.Message = parsed.SelectToken("MessageDiscription").Value<string>();
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                else
                {
                    errormdl.DateLog = DateTime.Now.ToString();
                    errormdl.Exp = ex;
                    errormdl.Message = ex.Message;
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                //error handling here
            }
        }

        /// <summary>
        /// Connects to a serial port defined through the current settings
        /// </summary>
        public void StartListening()
        {
            try
            {
                bool IsComPortPhysical = false;
                // ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");
                //foreach (ManagementObject queryObj in searcher.Get())
                //{
                //    //Console.WriteLine("serial port : {0}", queryObj["PortName"]);
                //    //Console.WriteLine("serial port : {0}", queryObj["InstanceName"]);
                //    if (_currentSerialSettings.PortName.Trim() == queryObj["PortName"].ToString())
                //    {
                //        IsComPortPhysical = true;
                //        //objWriter.WriteLine(queryObj["PortName"].ToString());
                //    }
                //}
                string[] ports = SerialPort.GetPortNames();

                Console.WriteLine("The following serial ports were found:");

                // Display each port name to the console.
                foreach (string port in ports)
                {
                    if (_currentSerialSettings.PortName.Trim() == port.ToString())
                    {
                        IsComPortPhysical = true;
                        //objWriter.WriteLine(queryObj["PortName"].ToString());
                    }
                   
                }

                if (!IsComPortPhysical) throw new Exception("Port is not physical port.");

                // Closing serial port if it is open
                if (_serialPort != null && _serialPort.IsOpen)
                    _serialPort.Close();
                // OpenPort();
                // Setting serial port settings
                _serialPort = new SerialPort(
                    WebAPIModelResponse.CombPortName,
                    Convert.ToInt32(WebAPIModelResponse.baudrate),
                   (Parity)Convert.ToInt32(WebAPIModelResponse.Parity),
                    Convert.ToInt32(WebAPIModelResponse.DataBits),
                (StopBits)Convert.ToInt32(WebAPIModelResponse.StopBits));
                _serialPort.Handshake = Handshake.None;
                _serialPort.RtsEnable = true;
                _serialPort.Open();
                // Subscribe to event and open serial port for data
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived1);

                var timer = new System.Timers.Timer();
                timer.Interval = 10000;
                timer.Start();
                timer.Elapsed += (sender, e) =>
                {

                    //MessageBox.Show("Listener Stopped!!");
                    if (_serialPort != null && _serialPort.IsOpen)
                        _serialPort.Close();
                    timer.Stop();
                };
            }
            catch (Exception ex)
            {
                ErrorModel errormdl = new ErrorModel();
                if (ex.Message.ToString().Contains("Status"))
                {
                    var parsed = JObject.Parse(ex.Message.ToString());
                    errormdl.DateLog = DateTime.Now.ToString();
                    //errormdl.Exp = parsed.SelectToken("MessageDiscription"  .Value<string>();
                    errormdl.Message = parsed.SelectToken("MessageDiscription").Value<string>();
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                else
                {
                    errormdl.DateLog = DateTime.Now.ToString();
                    errormdl.Exp = ex;
                    errormdl.Message = ex.Message;
                    ERawannaDesk.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
             

            }

        }

    }

}

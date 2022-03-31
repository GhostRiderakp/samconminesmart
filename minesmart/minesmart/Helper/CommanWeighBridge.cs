using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Linq;
using System.Reflection;

namespace ERawannaDesk.Helper
{
    public class CommanWeighBridge
    {
        public static System.IO.Ports.SerialPort serialPort1 = null;
        public bool IsReverseData;
        private StringBuilder m_ContinousDataStream = new StringBuilder();
        string m_SubDataStream = "";
        private Int64 grossWeight = -1;
        private Int64 confirmGrossWeight = 0;
        private bool retryToOpenPort = false;
        Exception execpt = new Exception();
        bool IsWeightForRawanna = false;
        public delegate void AddDataDelegate(String myString);
        public AddDataDelegate myDelegate;
        static string terminal_data = null;
        static char last_char = (char)2;
        ViewModels.SettingModel _currentSerialSettings = new ViewModels.SettingModel();
        public int _serialPortType;
        public event EventHandler<SerialDataEventArgs> NewSerialDataRecieved;
        public CommanWeighBridge()
        {
            // Finding installed serial ports on hardware
            _currentSerialSettings.PortNameCollection = SerialPort.GetPortNames();
            _currentSerialSettings.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_currentSerialSettings_PropertyChanged);

            // If serial ports is found, we select the first found
            if (_currentSerialSettings.PortNameCollection.Length > 0)
                _currentSerialSettings.PortName = _currentSerialSettings.PortNameCollection[0];
        }
        /// <summary>
        /// HRSoftDirect
        /// </summary>
        /// <returns></returns>
        /// 
        // Int64 finalGrossWeight = startWeightReading();
        //private Int64 startWeightReading()
        //{
        //    try
        //    {
        //        retryToOpenPort = true;
        //        grossWeight = -1;
        //        OpenPort(ConstantMaster.Port, Convert.ToInt32(ConstantMaster.BitRate), ConstantMaster.Parity, Convert.ToInt32(ConstantMaster.DataBit), Convert.ToBoolean(ConstantMaster.IsReverse), Convert.ToBoolean(ConstantMaster.IsHandShake));
        //        int executionCount = 0;
        //        while (grossWeight < 0 && executionCount <= 50 && serialPort1.IsOpen)
        //        {
        //            executionCount++;
        //            System.Threading.Thread.Sleep(300);
        //        }
        //        log.WriteLog("execution count- " + executionCount + " Grossweight " + grossWeight, ConstantMaster.log_error_type["custommessage"]);

        //    }
        //    catch (Exception ex)
        //    {
        //        log.WriteLog("PostData => startWeightReading() " + ex.Message, ConstantMaster.log_error_type["exception"]);
        //    }
        //    return grossWeight;
        //}

        private void OpenPort(string systemPort, Int32 baudRate, string parity, Int32 dataBit, bool isReverse, bool isHanshake)
        {
            try
            {
                IsReverseData = isReverse;
                // log.WriteLog("isReverse-: " + isReverse, ConstantMaster.log_error_type["custommessage"]);

                if (!IsComPortPhysical(systemPort))   //checking define port is physical port or not.
                    ERawannaDesk.Helper.ErrorLog.WriteSettingLog(execpt, "Error Physical Port Not Found ", "");
                else
                {   // com port is physical, proceed to read weight from terminal.
                    CloseSerialPort();
                    serialPort1.PortName = systemPort;
                    serialPort1.DataBits = dataBit;
                    serialPort1.BaudRate = baudRate;
                    serialPort1.StopBits = System.IO.Ports.StopBits.One;
                    switch (parity)
                    {
                        case "None":
                            serialPort1.Parity = System.IO.Ports.Parity.None;
                            break;
                        case "Odd":
                            serialPort1.Parity = System.IO.Ports.Parity.Odd;
                            break;
                        case "Even":
                            serialPort1.Parity = System.IO.Ports.Parity.Even;
                            break;
                        case "Mark":
                            serialPort1.Parity = System.IO.Ports.Parity.Mark;
                            break;
                        case "Space":
                            serialPort1.Parity = System.IO.Ports.Parity.Space;
                            break;
                        default:
                            serialPort1.Parity = System.IO.Ports.Parity.None;
                            break;
                    }

                    if (isHanshake)
                    {
                        serialPort1.Handshake = System.IO.Ports.Handshake.RequestToSend;
                        serialPort1.RtsEnable = true;
                    }

                    serialPort1.Open();
                    System.Threading.Thread.Sleep(500);
                    serialPort1.DataReceived += serialPort1_DataReceived;
                }
            }
            catch (Exception ex)
            {
                ERawannaDesk.Helper.ErrorLog.WriteSettingLog(ex, "PostData => OpenPort() ", "");
                if (!serialPort1.IsOpen && retryToOpenPort == true)  //retry=true; means retry to open port.
                {
                    retryToOpenPort = false;
                    OpenPort(ConstantMaster.Port, Convert.ToInt32(ConstantMaster.BitRate), ConstantMaster.Parity, Convert.ToInt32(ConstantMaster.DataBit), Convert.ToBoolean(ConstantMaster.IsReverse), Convert.ToBoolean(ConstantMaster.IsHandShake));
                }
            }
        }

        private bool IsComPortPhysical(string systemPort)
        {
            bool isComPortPhysical = false;
            try
            {
                var searcher = new System.Management.ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");
                foreach (var o in searcher.Get())
                {
                    var queryObj = (System.Management.ManagementObject)o;
                    if (systemPort.Trim() == queryObj["PortName"].ToString())
                    {
                        isComPortPhysical = true;
                    }
                }
                return isComPortPhysical;
            }
            catch (Exception ex)
            {
                ERawannaDesk.Helper.ErrorLog.WriteSettingLog(ex, "PostData => IsComPortPhysical() ", "");
            }
            return isComPortPhysical;
        }
        int sampleCount = 0;
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    int SubStreamLength = int.Parse(ConstantMaster.SubStreamLength);
                    int AvgWeightSampling = int.Parse(ConstantMaster.AvgWeightSampling); // sampling for weight.
                    string data = serialPort1.ReadExisting();
                    if (!string.IsNullOrEmpty(data.Trim()))
                    {
                        m_ContinousDataStream.Append(data);
                        m_SubDataStream += m_ContinousDataStream.ToString(); //storing all datastream value into a variable
                        // log.WriteLog("readExisting stream m_ContinousDataStream- " + m_ContinousDataStream.ToString(), ConstantMaster.log_error_type["custommessage"]);
                        m_ContinousDataStream.Clear();

                        if (IsWeightForRawanna)
                        {   // In case of rawanna generation we calculate Avg Weight from continues weight string   
                            ERawannaDesk.Helper.ErrorLog.WriteSettingLog(execpt, "PostData => serialPort1_DataReceived() IsWeightForRawanna- , ", "");
                            sampleCount++;
                            if (m_SubDataStream.Length > SubStreamLength && sampleCount >= AvgWeightSampling)
                            {
                                ERawannaDesk.Helper.ErrorLog.WriteSettingLog(execpt, "PostData => Data stream for rawanna true- ", "");
                                ParseWeightForDisplay(m_SubDataStream);
                                m_SubDataStream = "";
                                sampleCount = 0;
                            }
                        }
                        else
                        {
                            if (m_SubDataStream.Length > SubStreamLength)
                            {
                                ParseWeightForDisplay(m_SubDataStream);
                                m_SubDataStream = "";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ERawannaDesk.Helper.ErrorLog.WriteSettingLog(ex, "PostData => serialPort1_DataReceived() ", "");
            }
        }
        private static string ReverseWeight(string weight)
        {
            if (!string.IsNullOrEmpty(weight))
            {
                char[] chars = weight.ToCharArray();
                Array.Reverse(chars);
                return new string(chars);
            }
            else
                return "0";
        }

        public void CloseSerialPort()
        {
            try
            {
                if (serialPort1 != null && serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    System.Threading.Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                ERawannaDesk.Helper.ErrorLog.WriteSettingLog(ex, "PostData => CloseSerialPort() ", "");
            }
        }
        private void ParseWeightForDisplay(string subDataStream)
        {
            try
            {
                var multipleWeight = System.Text.RegularExpressions.Regex.Replace(subDataStream, "[^0-9]+", "&"); // replacing all the carater rather than digit with & .               
                multipleWeight = multipleWeight.Trim('&'); // trimming & from start and end.

                if (!string.IsNullOrEmpty(multipleWeight))
                {
                    var splitWeight = multipleWeight.Split('&'); //splitting multiple weight by & 
                    Int64 newWeight = 0;
                    // log.WriteLog("ParseWeightForDisplay-After removing garbage multipleWeight string -" + multipleWeight, ConstantMaster.log_error_type["custommessage"]);

                    for (var i = 0; i < splitWeight.Length; i++)
                    // in this for loop we are getting heightest weight from multiple weight.
                    {
                        if (IsReverseData)
                        {
                            if (newWeight < Convert.ToInt64(ReverseWeight(splitWeight[i])))
                                newWeight = Convert.ToInt64(ReverseWeight(splitWeight[i]));
                        }
                        else
                        {
                            if (newWeight < Convert.ToInt64(splitWeight[i]))
                                newWeight = Convert.ToInt64(splitWeight[i]);
                        }
                    }
                    grossWeight = newWeight;
                    // log.WriteLog("ParseWeightForDisplay-grossWeight-" + grossWeight, ConstantMaster.log_error_type["custommessage"]);

                }
            }
            catch (Exception ex)
            {
                ERawannaDesk.Helper.ErrorLog.WriteSettingLog(ex, "PostData => ParseWeightForDisplay() ", "");
            }
        }

        #region
        /// <summary>
        /// DMGEverLastUtility
        /// </summary>        
        public string serial_data(string port, string LoadCell, int BaudRate, int bitRate)
        {
            string weight;
            bool IsComPortPhysical = false;
            try
            {
                //define object for serial port and baud rate and partity set
                System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");
                foreach (System.Management.ManagementObject queryObj in searcher.Get())
                {
                    if (port.Trim() == queryObj["PortName"].ToString())
                    {
                        IsComPortPhysical = true;
                        break;
                    }
                }
                if (IsComPortPhysical)
                {
                    SerialPort myport = new SerialPort(port, BaudRate, Parity.None, bitRate);
                    Thread.Sleep(100);
                    if (myport.IsOpen == false)    //if (IsComPortPhysical)
                    {
                        myport.Open();
                    }
                    else
                    {
                        myport.Close();
                        myport.Dispose();
                        Thread.Sleep(500);
                        myport.Open();
                    }
                    do
                    {
                        Thread.Sleep(50);
                        weight = myport.ReadLine().Trim();

                        weight = new string(weight.Where(x => char.IsDigit(x)).ToArray());
                        break;

                        //if (weight.Substring(LoadCell.Length, (weight.Length - LoadCell.Length)).Length >= 6)
                        //{
                        //    if (weight.StartsWith(LoadCell) && weight.Substring(LoadCell.Length, 6).All(char.IsDigit))
                        //    {
                        //        weight = new string(weight.Where(x => char.IsDigit(x)).ToArray());
                        //        break;
                        //    }
                        //}
                    }
                    while (true);
                    myport.Close();
                    myport.Dispose();
                    return weight;
                }
                else
                {
                    string[] lines = { "The selected COM Port is not physical. Please select a physical COM Port. ------------" + DateTime.Now.ToString() };
                    System.IO.File.AppendAllLines(@"C:\everlast_dmg_software2\dmg_software\ErrorText.txt", lines);
                    return "";
                }
            }
            catch (Exception ex)
            {
                string[] lines = { ex.Message + "rs232 Line No 100 ------------" + DateTime.Now.ToString() };
                System.IO.File.AppendAllLines(@"C:\everlast_dmg_software2\dmg_software\ErrorText.txt", lines);
                return "";
            }
        }
        #endregion

        #region
        /// <summary>
        /// eCareMines
        /// </summary>  
        private static decimal Weight = 0;

        public void InitializePort(string PortName, int BaudRate, int DataBits, StopBits StopBit, Parity Parity)
        {
            try
            {
                if (serialPort1 == null)
                    serialPort1 = new SerialPort();

                if (serialPort1.IsOpen)
                    serialPort1.Close();

                if (IsPortPhysical(PortName))
                {
                    serialPort1.PortName = PortName;
                    serialPort1.BaudRate = BaudRate;
                    serialPort1.DataBits = DataBits;
                    serialPort1.StopBits = StopBit;
                    serialPort1.Parity = Parity;
                    serialPort1.DataReceived += port_DataReceived;

                    serialPort1.Open();
                    Weight = 0;
                }
                else
                {
                    throw new Exception("Port Not physical");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " :  at CL - WeighBridgeReader - Initialize Port");
            }
        }

        public static decimal TotalWeight()
        {
            try
            {
                return Weight;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message + " :  at CL - WeighBridgeReader - Total Weight");
            }
        }

        public static decimal TotalWeightinTon()
        {
            try
            {
                return Math.Round((Weight / 1000), 2);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message + " :  at CL - WeighBridgeReader - Total Weight in Ton");
            }
        }

        private bool IsPortPhysical(string PortName)
        {
            bool isComPortPhysical = false;
            try
            {
                var searcher = new System.Management.ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");
                System.Management.ManagementObjectCollection coll = searcher.Get();
                if (coll != null && coll.Count > 0)
                {
                    foreach (var o in coll)
                    {
                        var queryObj = (System.Management.ManagementObject)o;
                        if (PortName.Trim() == queryObj["PortName"].ToString())
                        {
                            isComPortPhysical = true;
                        }
                    }
                }
                searcher.Dispose();


                return isComPortPhysical;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + " :  at CL - WeighBridgeReader - Is Port Physical");
            }
        }

        public static void ParseWeightStream(string DataStream)
        {
            string text = System.Text.RegularExpressions.Regex.Replace(DataStream, "[^0-9]+", "&");
            text = text.Trim();
            if (!string.IsNullOrEmpty(text))
            {
                string[] array = text.Split('&');

                int TotalCount = array.Length - 1;

                for (int i = TotalCount; i >= 0; i--)
                {
                    if (array[i].Trim() != string.Empty)
                    {
                        Weight = Convert.ToInt32(array[i]);
                        break;
                    }
                }
            }
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!serialPort1.IsOpen) return;
            while (serialPort1.BytesToRead > 0) //<-- repeats until the In-Buffer is empty
            {
                int bytes = serialPort1.BytesToRead;
                byte[] buffer = new byte[bytes];
                serialPort1.Read(buffer, 0, bytes);
                ParseWeightStream(Encoding.UTF8.GetString(buffer).Trim());
            }
        }
        #endregion

        #region
        /// <summary>
        /// JYOTI_00050.26.3
        /// </summary>   
        ///
        private void SerialPortInitialization(string comName)
        {
            serialPort1 = new SerialPort(comName);
            //SerialPort = new SerialPort();

            if (serialPort1.IsOpen == true) serialPort1.Close();

            serialPort1.Encoding = Encoding.ASCII;
            serialPort1.BaudRate = 2400;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Parity = Parity.None;
            serialPort1.ReadBufferSize = 4096;
            serialPort1.NewLine = "\r\n";
            serialPort1.Handshake = Handshake.None;
            serialPort1.ReadTimeout = 500;


            serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);


        }
        /************************************************************************
         * DataReceivedHandler
         * **********************************************************************/
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {

            if (serialPort1.IsOpen == true)
            {
                //SerialPort sp = (SerialPort)sender;
                string indata = serialPort1.ReadExisting();

                // txtSerial.Invoke(this.myDelegate, new Object[] { indata });

            }
        }


        #endregion

        #region MyRegion

        /// PRIME_00015.7.2
        /// <summary>
        /// reads Weight From Com Port
        /// </summary>
        /// <param name="portName">port Name</param>
        /// <param name="BaudRate">Baud Rate</param>
        /// <returns></returns>
        public static string readWeightFromComPort(string portName, Int16 BaudRate, string path, Boolean IsNonPrimeSw, string WSBit, Int16 WSfrom, Int32 WLenght, Boolean IsReverseWeight, Int16 WaitBeforeInitSP)
        {
            //Make execution True
            Boolean IsRecGet = true;
            string str = "";
            string s = "";
            Thread.Sleep(100);
            Boolean IsComPortPhysical = false;

            // Writing weight in file so can be used to show the user
            try
            {
                System.Management.ConnectionOptions _Options = new System.Management.ConnectionOptions();
                System.Management.ManagementPath _Path = new System.Management.ManagementPath(s);

                System.Management.ManagementScope _Scope = new System.Management.ManagementScope(_Path, _Options);
                _Scope.Connect();
                // get all physical com ports
                System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");

                foreach (System.Management.ManagementObject queryObj in searcher.Get())
                {
                    if (portName.Trim() == queryObj["PortName"].ToString().Trim())
                    {
                        IsComPortPhysical = true;
                        break;
                    }
                }
                if (!IsComPortPhysical)
                {
                    return "There is no physical port found with portName: " + portName;
                }
            }
            catch (Exception ex)
            {

                // Writing weight in file so can be used to show the user

                return ex.Message;
            }
            //Wait 10 Sec
            if (IsComPortPhysical)
            {
                System.IO.Ports.SerialPort comport = new System.IO.Ports.SerialPort();
                Thread.Sleep(500);
                comport.PortName = portName;
                comport.BaudRate = Convert.ToInt16(BaudRate);
                comport.DiscardNull = true;
                comport.DtrEnable = true;
                comport.ReceivedBytesThreshold = 1;

                if (comport.IsOpen)
                {
                    comport.Close();
                }
                comport.Open();
                Thread.Sleep(WaitBeforeInitSP);
                DateTime startTime = DateTime.Now;
                Int32 timeSpent = Convert.ToInt32((Microsoft.VisualBasic.DateAndTime.Now - startTime).Seconds);
                while (timeSpent <= 10)
                {
                    if (comport.IsOpen == true)
                    {
                        timeSpent = Convert.ToInt32((Microsoft.VisualBasic.DateAndTime.Now - startTime).Seconds);
                        if (comport.BytesToRead > (byte)0)
                        {
                            int bytes = comport.BytesToRead;

                            //// Create a byte array buffer to hold the incoming data
                            byte[] buffer = new byte[bytes];

                            //// Read the data from the port and store it in our buffer
                            comport.Read(buffer, 0, bytes);
                            //Get Current Byte
                            string Cr = ByteArrayToHexString(buffer);

                            //Append All byte
                            str = Cr;
                            //Success Validate is Prime IT or Indicator
                            //Actual code 14-03-13 
                            if (Cr.Trim().Length >= 24)
                            {
                                Int16 pos = Convert.ToInt16(Cr.IndexOf(WSBit));
                                if (Cr.Trim().Substring(pos, 2) == WSBit)
                                {
                                    switch (IsNonPrimeSw)
                                    {
                                        case true:
                                            Cr = Cr.Substring(pos + 2);
                                            Cr = Cr.Substring(WSfrom, WLenght);
                                            for (Int16 i = 0; i <= Cr.Length - 1; i++)
                                            {
                                                Int16 asc = Convert.ToInt16(Int32.Parse(Cr.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                                                s = s + Convert.ToString(Microsoft.VisualBasic.Strings.Chr(asc));
                                                i += 1;
                                            }
                                            Boolean IsCorrectRec = true;
                                            for (Int16 i = 0; i <= s.Length - 1; i++)
                                            {
                                                if ((Microsoft.VisualBasic.Strings.Asc(s.Substring(i, 1)) < 48) && (Microsoft.VisualBasic.Strings.Asc(s.Substring(i, 1)) > 57))
                                                {
                                                    IsCorrectRec = false;
                                                }
                                            }

                                            if (IsCorrectRec == true)
                                            {
                                                if (s.Length > 0)
                                                {
                                                    str = s.Trim();
                                                    if (IsReverseWeight == true)
                                                    {
                                                        str = Microsoft.VisualBasic.Strings.StrReverse(str);
                                                    }
                                                }
                                                break;
                                            }
                                            s = Convert.ToString(Microsoft.CodeAnalysis.CSharp.Conversion.Val(str));

                                            break;
                                        case false:
                                            str = "";
                                            s = "";

                                            //Encrypt Data here ... date 16/05/11
                                            string L; Int32 M = 0;
                                            L = Convert.ToString(Convert.ToInt32(Cr.Substring(9, 1)) + Convert.ToInt32(Cr.Substring(11, 1)) + Convert.ToInt32(Cr.Substring(13, 1)) + Convert.ToInt32(Cr.Substring(15, 1)));
                                            if (Convert.ToString(L).Length == 1)
                                            {
                                                L = Convert.ToString("0" + Convert.ToInt32(Convert.ToString(L)));
                                            }
                                            M = Convert.ToInt32(Convert.ToInt32(Cr.Substring(21, 1)) + Convert.ToInt32(Cr.Substring(23, 1)));
                                            if (Convert.ToString(Cr.Substring(17, 1)) != Convert.ToString(L).Substring(1, 1))
                                            {
                                                Cr = "80800000000000000";
                                            }
                                            if (Convert.ToString(Cr).Length > 19)
                                            {
                                                if (Convert.ToString(Cr.Substring(19, 1)) != Convert.ToString(L).Substring(0, 1))
                                                {
                                                    Cr = "80800000000000000";
                                                }
                                            }
                                            if (Convert.ToString(Cr).Length > 16)
                                            {
                                                if (Convert.ToString(Cr.Substring(16, 1)) != "6")
                                                {
                                                    Cr = "80800000000000000";
                                                }
                                            }
                                            if (Convert.ToString(Cr).Length > 18)
                                            {
                                                if (Convert.ToString(Cr.Substring(18, 1)) != "5")
                                                {
                                                    Cr = "80800000000000000";
                                                }
                                            }
                                            if (Convert.ToString(Cr).Length > 20)
                                            {
                                                if (Convert.ToString(Cr.Substring(20, 1)) != "4")
                                                {
                                                    Cr = "80800000000000000";
                                                }
                                            }
                                            if (Convert.ToString(Cr).Length > 22)
                                            {
                                                if (Convert.ToString(Cr.Substring(22, 1)) != "2")
                                                {
                                                    Cr = "80800000000000000";
                                                }
                                            }
                                            if (Convert.ToString(Cr).Length > 24)
                                            {
                                                if (Convert.ToString(Cr.Substring(24, 1)) != "7")
                                                {
                                                    Cr = "80800000000000000";
                                                }
                                            }
                                            Int32 LM = Convert.ToInt32(Convert.ToInt32(Microsoft.VisualBasic.Strings.Left(Convert.ToString(L), 1)) + Convert.ToInt32(Microsoft.VisualBasic.Strings.Right(Convert.ToString(L), 1)));
                                            LM = Convert.ToInt32(Convert.ToInt32(LM) + Convert.ToInt32(M));
                                            if (Convert.ToString(Cr).Length > 25)
                                            {
                                                if (Convert.ToString(Microsoft.VisualBasic.Strings.Right(Convert.ToString(LM), 1)) != Convert.ToString(Cr.Substring(25, 1)))
                                                {
                                                    Cr = "80800000000000000";
                                                }
                                            }
                                            for (Int32 i = 4; i <= 14; i++)
                                            {
                                                Int32 asc = Convert.ToInt32(Int32.Parse(Cr.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                                                //string s = Convert.ToString(Int32.Parse(Cr.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                                                if (asc < 0)
                                                {
                                                    if (i == 4)
                                                    {
                                                        str = str + "-0";
                                                    }
                                                    else
                                                    {
                                                        asc = Convert.ToInt32(Convert.ToInt32(asc) + Convert.ToInt32(128));
                                                        s = Convert.ToString(Microsoft.VisualBasic.Strings.Chr(asc));
                                                        str = str + s;
                                                    }
                                                }
                                                else if (asc > 128)
                                                {
                                                    if (i == 4)
                                                    {
                                                        s = Convert.ToString(Microsoft.VisualBasic.Strings.Chr(32));
                                                        str = str + s;
                                                    }
                                                    else
                                                    {
                                                        asc = Convert.ToInt32(Convert.ToInt32(asc) - Convert.ToInt32(128));
                                                        s = Convert.ToString(Microsoft.VisualBasic.Strings.Chr(asc));
                                                        s = s + ".";
                                                        str = str + s;
                                                    }
                                                }
                                                else
                                                {
                                                    if (i == 4)
                                                    {
                                                        if (Cr.Substring(i, 1) == "C")
                                                        {
                                                            s = Convert.ToString(Int32.Parse("3" + Cr.Substring(i + 1, 1), System.Globalization.NumberStyles.HexNumber));
                                                            str = str + Convert.ToString(Microsoft.VisualBasic.Strings.Chr(Convert.ToInt32(s)));
                                                        }
                                                        else
                                                        {
                                                            s = Convert.ToString(Int32.Parse(Cr.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                                                            str = str + Convert.ToString(Microsoft.VisualBasic.Strings.Chr(Convert.ToInt32(s)));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        s = Convert.ToString(Int32.Parse(Cr.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                                                        str = str + Convert.ToString(Microsoft.VisualBasic.Strings.Chr(Convert.ToInt32(s)));
                                                    }
                                                }
                                                i += 1;
                                            }
                                            break;
                                    }
                                }
                                //Failure
                                if (str != "000D")
                                {
                                    //MessageBox.Show("Failed", Application.ProductName);
                                    break;
                                }
                                //Make Loop Continuous..otherwise exit
                                if (IsRecGet == true)
                                {
                                    if (Cr == "")
                                    {
                                        //Start Time Again
                                        IsRecGet = false;
                                        startTime = DateTime.Now;
                                    }
                                }
                            }
                        }
                    }
                }
                comport.Close();
            }

            return str;
        }

        /// <summary>
        /// Byte to string 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                //sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();
        }

        #endregion

        #region 
        ///sharwan_scale_ipcam
        ///

        public static string Port_ready(string machineName, string portName)
        {

            // <summary>
            //  check serial port is available or not 
            // </summary>

            bool IsComPortPhysical = false;
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");
            foreach (System.Management.ManagementObject queryObj in searcher.Get())
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
                        serialPort1 = new SerialPort(portName, 1200, Parity.None, 8);
                        break;
                    case "2":
                        serialPort1 = new SerialPort(portName, 2400, Parity.None, 8);
                        break;
                    case "3":
                        serialPort1 = new SerialPort(portName, 1200, Parity.Odd, 7);
                        break;
                    case "4":
                        serialPort1 = new SerialPort(portName, 1200, Parity.None, 8);
                        last_char = (char)2;
                        break;
                    case "5":
                        serialPort1 = new SerialPort(portName, 9600, Parity.None, 8);
                        last_char = (char)2;
                        break;
                    case "6":
                        serialPort1 = new SerialPort(portName, 9600, Parity.None, 8);
                        break;
                    case "7":
                        serialPort1 = new SerialPort(portName, 300, Parity.Odd, 7);
                        break;
                    case "8":
                        serialPort1 = new SerialPort(portName, 300, Parity.None, 7);
                        break;
                    case "9":
                        serialPort1 = new SerialPort(portName, 19200, Parity.None, 8);
                        break;

                }
                try
                {
                    if (serialPort1.IsOpen)
                    {
                        serialPort1.Close();
                    }
                    serialPort1.Open();
                }
                catch (Exception ex)
                {
                    return "Serial port problem";
                }

                serialPort1.DataReceived += new SerialDataReceivedEventHandler(myport_DataReceived);

                return "OK";
            }
            else

                return "Selected Port is not a Physical Port, Please select a physical port.";
        }

        static void myport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                terminal_data = serialPort1.ReadTo(last_char.ToString());

            }
            catch (Exception ex)
            {
                terminal_data = "0";
            }

        }

        public static string Getweight()
        {
            string wt = refine(terminal_data);

            if (web_service.MachineName == "5")
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


        #endregion

        #region DOSTMarudhara
        #region Event handlers
        #region Properties
        /// <summary>
        /// Gets or sets the current serial port settings
        /// </summary>
        public ViewModels.SettingModel CurrentSerialSettings
        {
            get { return _currentSerialSettings; }
            set { _currentSerialSettings = value; }
        }

        #endregion
        void _currentSerialSettings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            // if serial port is changed, a new baud query is issued
            if (e.PropertyName.Equals("PortName"))
                UpdateBaudRateCollection();
        }
        public static string Reverses(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        void callRefe(string data)
        {
            string weighBridgeRegex = CurrentSerialSettings.weighBridgeReader;//Properties.Settings.Default["weighBridgeReader"].ToString();
            string isReversedDrop = CurrentSerialSettings.isReversedDrop;//Properties.Settings.Default["isReversedDrop"].ToString();
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(weighBridgeRegex);

            if (isReversedDrop == "Yes")
            {
                data = Reverses(data);
            }

            System.Text.RegularExpressions.Match match = regex.Match(data);
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
        int lengthOfTheData = 0;
        string TotalData;

        void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (_serialPortType == 1)
            {
                int dataLength = serialPort1.BytesToRead;
                byte[] data = new byte[dataLength];
                int nbrDataRead = serialPort1.Read(data, 0, dataLength);
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
                while (serialPort1.BytesToRead > 0)
                {
                    try
                    {
                        data = serialPort1.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
                callRefe(data);
            }

        }

        #endregion

        #region Methods

        /// <summary>
        /// Connects to a serial port defined through the current settings
        /// </summary>
        public void StartListening()
        {
            bool IsComPortPhysical = false;
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("root\\WMI", "SELECT * FROM MSSerial_PortName");
            foreach (System.Management.ManagementObject queryObj in searcher.Get())
            {
                //Console.WriteLine("serial port : {0}", queryObj["PortName"]);
                //Console.WriteLine("serial port : {0}", queryObj["InstanceName"]);
                if (_currentSerialSettings.PortName.Trim() == queryObj["PortName"].ToString())
                {
                    IsComPortPhysical = true;
                    //objWriter.WriteLine(queryObj["PortName"].ToString());
                }
            }
            if (!IsComPortPhysical) throw new Exception("Port is not physical port.");
            // Closing serial port if it is open
            if (serialPort1 != null && serialPort1.IsOpen)
                serialPort1.Close();

            // Setting serial port settings
            serialPort1 = new SerialPort(
                _currentSerialSettings.PortName,
                Convert.ToInt32(_currentSerialSettings.BaudRate),
                (Parity)Convert.ToInt32(_currentSerialSettings.Parity),
                Convert.ToInt32(_currentSerialSettings.DataBits),
                (StopBits)Convert.ToInt32(_currentSerialSettings.StopBits));

            // Subscribe to event and open serial port for data
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);
            serialPort1.Open();
            var timer = new System.Timers.Timer();
            timer.Interval = 10000;
            timer.Start();
            timer.Elapsed += (sender, e) =>
            {
                //MessageBox.Show("Listener Stopped!!");
                if (serialPort1 != null && serialPort1.IsOpen)
                    serialPort1.Close();
                timer.Stop();
            };
        }

        /// <summary>
        /// Closes the serial port
        /// </summary>
        public void StopListening()
        {
            serialPort1.Close();
        }


        /// <summary>
        /// Retrieves the current selected device's COMMPROP structure, and extracts the dwSettableBaud property
        /// </summary>
        private void UpdateBaudRateCollection()
        {
            try
            {
                serialPort1 = new SerialPort(_currentSerialSettings.PortName);
                serialPort1.Open();
                object p = serialPort1.BaseStream.GetType().GetField("commProp", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(serialPort1.BaseStream);
                Int32 dwSettableBaud = (Int32)p.GetType().GetField("dwSettableBaud", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(p);

                serialPort1.Close();
                //  _currentSerialSettings.UpdateBaudRateCollection(dwSettableBaud);
            }
            catch (Exception er)
            {
                //MessageBox.Show(er.Message);
                throw er;
            }
        }


        // Call to release serial port
        public void Dispose()
        {
            Dispose(true);
        }

        // Part of basic design pattern for implementing Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && serialPort1 != null)
            {
                serialPort1.DataReceived -= new SerialDataReceivedEventHandler(_serialPort_DataReceived);
            }
            // Releasing serial port (and other unmanaged objects)
            if (serialPort1 != null)
            {
                if (serialPort1.IsOpen)
                    serialPort1.Close();

                serialPort1.Dispose();
            }
        }


        #endregion
        #endregion
    }
    /// <summary>
    /// EventArgs used to send bytes recieved on serial port
    /// </summary>
    public class SerialDataEventArgs : EventArgs
    {
        public SerialDataEventArgs(string dataInByteArray)
        {
            Data = dataInByteArray;
        }

        /// <summary>
        /// Byte array containing data from serial port
        /// </summary>
        public string Data;
    }
}

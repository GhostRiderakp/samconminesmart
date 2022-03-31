using minesmart.Helper;
using minesmart.ViewModels;
using Newtonsoft.Json.Linq;
using minesmart.DGMS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using WebEye.Controls.WinForms.StreamPlayerControl;
using Timer = System.Windows.Forms.Timer;
using System.Timers;
using minesmart.DGM;

namespace minesmart
{
    public partial class UCCamersetting : UserControl
    {
        MineMart _owner;
        SerialPort myport;
        weightReader _spManager;

        Exception ex = new Exception();
        private string rtsp1 = string.Empty;
        private string rtsp2 = string.Empty;
        public event EventHandler<SerialDataEventArgs> NewSerialDataRecieved;
        private SettingModel _currentSerialSettings = new SettingModel();
        
        public UCCamersetting()
        {
            try
            {

                InitializeComponent();
                _currentSerialSettings.PortNameCollection = SerialPort.GetPortNames();
                if (_currentSerialSettings.PortNameCollection.Length > 0)
                    _currentSerialSettings.PortName = _currentSerialSettings.PortNameCollection[0];
                rtsp1 = WebAPIModelResponse.CameraFrontUrl;
                rtsp2 = WebAPIModelResponse.CameraRearUrl;
                streamPlayerControl1.StartPlay(new Uri(rtsp1), TimeSpan.FromMilliseconds(500), RtspTransport.Undefined, RtspFlags.None);
                streamPlayerControl2.StartPlay(new Uri(rtsp2), TimeSpan.FromMilliseconds(500), RtspTransport.Undefined, RtspFlags.None);
                richTextBox1.ForeColor = Color.Red;

                richTextBox1.Text = "0";
                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                richTextBox1.TabStop = false;
                richTextBox1.ReadOnly = false;
                richTextBox1.Enabled = false;
                richTextBox1.SelectionBackColor = Color.Black;
                richTextBox1.Cursor = Cursors.No;
                if (WebAPIModelResponse.IsSubmitsetting == "0")
                {

                }
                else
                {
                    string[] ports = SerialPort.GetPortNames();

                    bool IsComPortPhysical = false;
                    // Display each port name to the console.
                    foreach (string port in ports)
                    {
                        if (_currentSerialSettings.PortName.Trim() == port.ToString())
                        {
                            IsComPortPhysical = true;
                            //objWriter.WriteLine(queryObj["PortName"].ToString());
                        }

                    }
                    if (IsComPortPhysical)
                    {
                        Timer timer1 = new Timer();
                        timer1.Interval = 1000;
                        timer1.Start();
                        timer1.Enabled = true;
                        timer1.Tick += new System.EventHandler(OnTimerEvent);
                        UserInitializationForSerialPort();
                    }
                    else
                    {

                    }
                }
                //rtsp://manoj:ssspl123@192.168.1.108/cam/realmonitor?channel=4&subtype=01
            }
            catch (Exception exception)
            {
                MessageBox.Show("Stream Url Not Set Please set Your Setting First.....", "UCCamersetting");
            }
        }

        private void OnTimerEvent(object sender, EventArgs e)
        {
            try
            {
                UserInitializationForSerialPort();
                #region Confirm Rawanna
                _spManager._serialPortType = Convert.ToInt32(WebAPIModelResponse.SytemType);
                _spManager.StartListening();
                if (Convert.ToInt64(richTextBox1.Text == "" ? "0" : richTextBox1.Text) == Convert.ToInt64(0))
                {


                    //richTextBox1.Text = "0";
                    // richTextBox1.Text = "0";
                    _spManager.Dispose();
                    _spManager._serialPortType = Convert.ToInt32(WebAPIModelResponse.SytemType);
                    _spManager.StartListening();

                    //  timer.Stop();

                }
                else
                {
                    _spManager.Dispose();
                    _spManager._serialPortType = Convert.ToInt32(WebAPIModelResponse.SytemType);
                    _spManager.StartListening();


                }
                #endregion
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
                    minesmart.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                else
                {
                    errormdl.DateLog = DateTime.Now.ToString();
                    errormdl.Exp = ex;
                    errormdl.Message = ex.Message;
                    minesmart.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                MessageBox.Show(ex.Message, "OnTimerEvent");

            }
        }
        public void UserInitializationForSerialPort()
        {
            try
            {

                _spManager = new weightReader();
                //_spManager.CurrentSerialSettings.
                _spManager.NewSerialDataRecieved += new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved);

            }
            catch (Exception exception)
            {
                ErrorModel errormdl = new ErrorModel();
                if (ex.Message.ToString().Contains("Status"))
                {
                    var parsed = JObject.Parse(ex.Message.ToString());
                    errormdl.DateLog = DateTime.Now.ToString();
                    //errormdl.Exp = parsed.SelectToken("MessageDiscription"  .Value<string>();
                    errormdl.Message = parsed.SelectToken("MessageDiscription").Value<string>();
                    minesmart.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                else
                {
                    errormdl.DateLog = DateTime.Now.ToString();
                    errormdl.Exp = ex;
                    errormdl.Message = ex.Message;
                    minesmart.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                MessageBox.Show(ex.Message, "UserInitializationForSerialPort");
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _spManager.Dispose();
        }
        /// <summary>
        /// On receive of data from weighbridge machine set it to textBox and 
        /// if save settings true then save the setting in user pref
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            try
            {

                if (this.InvokeRequired)
                {
                    // Using this.Invoke causes deadlock when closing serial port, and BeginInvoke is good practice anyway.
                    this.BeginInvoke(new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved), new object[] { sender, e });
                    return;
                }

                string str = e.Data;
                // _spManager.StopListening();
                //str = "0";
                double weight = Int64.Parse(str);
                richTextBox1.Text = weight.ToString();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
                richTextBox1.TabStop = false;
                richTextBox1.ReadOnly = false;
                richTextBox1.Enabled = false;
                richTextBox1.SelectionBackColor = Color.Black;
                richTextBox1.Cursor = Cursors.No;
                //txtweight.Text = weight.ToString();
            }
            catch (Exception exception)
            {
                ErrorModel errormdl = new ErrorModel();
                if (ex.Message.ToString().Contains("Status"))
                {
                    var parsed = JObject.Parse(ex.Message.ToString());
                    errormdl.DateLog = DateTime.Now.ToString();
                    //errormdl.Exp = parsed.SelectToken("MessageDiscription"  .Value<string>();
                    errormdl.Message = parsed.SelectToken("MessageDiscription").Value<string>();
                    minesmart.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                else
                {
                    errormdl.DateLog = DateTime.Now.ToString();
                    errormdl.Exp = ex;
                    errormdl.Message = ex.Message;
                    minesmart.Helper.ErrorLog.SaveErrorLogtable(errormdl);
                }
                MessageBox.Show(ex.Message, "_spManager_NewSerialDataRecieved");
            }
        }


        private void richTextBox1_EnabledChanged(object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.Black;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}

using minesmart.DGMS;
using minesmart.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace minesmart.Print
{
    public partial class ShortLoginInfo : Form
    {
        CheckConnection checkcon = new CheckConnection();
        public ShortLoginInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebAPIModelResponse.UserProfileid = "0";
            WebAPIModelResponse.UserCredtentialId = "0";
            WebAPIModelResponse.Authtoken = checkcon.GetMacAddress();
            WebAPIModelResponse.SsoId = txtssoid.Text;
            WebAPIModelResponse.Password = txtpassword.Text;
            WebAPIModelResponse.Weightbridgeno = txtwegihtno.Text;
            WebAPIModelResponse.Accesskey = txtaccesskey.Text;
            WebAPIModelResponse.ActiveKey = txtactiveNo.Text;
            WebAPIModelResponse.Userid = "0";
            WebAPIModelResponse.CompanyId = 0;
            WebAPIModelResponse.ClientId = 0;
            WebAPIModelResponse.Provider = txtprovoider.Text;
            WebAPIModelResponse.CameraRearUrl = txtcamera1.Text;
            WebAPIModelResponse.CameraFrontUrl = txtcamera2.Text;
            WebAPIModelResponse.CombPortName = txtprotnam.Text;
            WebAPIModelResponse.LanguageSetting = "0";
            WebAPIModelResponse.baudrate = txtbaurdrate.Text;
            WebAPIModelResponse.Parity = txtparity.Text;
            WebAPIModelResponse.DataBits = txtdatabits.Text;
            WebAPIModelResponse.StopBits = txtstopbits.Text;
            MineMart childForm = new MineMart();
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Show();
        }

        private void ShortLoginInfo_Load(object sender, EventArgs e)
        {

        }
    }
}

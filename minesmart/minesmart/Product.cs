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
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesmart
{
    public partial class Product : Form
    {
        public static String ErrorFilePath = @"\ErrorLog\minesmart.txt";
        public static System.IO.StreamWriter objWriter;
        public static string applicationPath = System.Windows.Forms.Application.StartupPath;
        public static string LogFolderPath = applicationPath + ErrorFilePath;
        Login login = new Login();
        Select select = new Select();
        CheckConnection checkcon = new CheckConnection();
        bool result = false;
        static string conn_str = Properties.Settings.Default.Connection_String;
        static string weightbreidge = Properties.Settings.Default.WeightbridgeNo;
        WebAPIModelResponse wb = new WebAPIModelResponse();
        SettingModel _settingModal = new SettingModel();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        static DataTable dtMain = new DataTable();
        MineMart mm = new MineMart();
        public Product()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.TopMost = true;
            this.Text = String.Empty;
            txtproductname.Focus();
        }

        private void btnproductsave_Click(object sender, EventArgs e)
        {
            SettingModel settingModal = new SettingModel();
            DataTable dtvehi = new DataTable();
            try
            {
                var finalerrorResult = true;
                var finalerrormsg = string.Empty;
                var IsResult = string.Empty;
                settingModal.SsoId = Convert.ToString(WebAPIModelResponse.SsoId);
                settingModal.UserId = Convert.ToInt64(WebAPIModelResponse.Userid);
                settingModal.CompanyId = Convert.ToInt64(WebAPIModelResponse.CompanyId);
                settingModal.ClientId = Convert.ToInt64(WebAPIModelResponse.ClientId);
                settingModal.weightbridgeNo = Convert.ToString(WebAPIModelResponse.Weightbridgeno);
                if (string.IsNullOrEmpty(txtproductnme.Text.Trim()))
                {
                    finalerrormsg = "Please Enter Product Name";
                    finalerrorResult = false;
                    MessageBox.Show(finalerrormsg, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (finalerrorResult)
                {
                    try
                    {
                        settingModal.MineralName = txtproductnme.Text.Trim();
                        settingModal.PostUrl = "/Api/ErawaanaAPI/SetProductName/";
                        dtvehi = WebAPI.PostSetProductName(settingModal.PostUrl, settingModal).Result;
                        if (dtvehi != null)
                        {
                            if (Convert.ToString(dtvehi.Rows[0]["Status"]) == "210")
                            {
                                MessageBox.Show("Some Error occured this process", "Error", MessageBoxButtons.OK);
                            }
                            else
                            {
                                Properties.Settings.Default.cmbproductname = Convert.ToString(txtproductnme.Text.Trim());
                                Properties.Settings.Default.Save();
                                mm.SuccessProductMsg();
                                mm.BindProductItem1();
                                mm.ResetText();
                                this.Close();
                            }

                        }

                    }
                    catch (Exception ex)
                    {

                        // Display Message  
                        MessageBox.Show("Something goes wrong, Please try again later.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        minesmart.Helper.ErrorLog.WriteSettingLog(ex, "custommessage", "Something goes wrong, Please try again later.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Display Message 
                MessageBox.Show("Something goes wrong, Please try again later.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                minesmart.Helper.ErrorLog.WriteSettingLog(ex, "custommessage", "Something goes wrong, Please try again later.");

            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            mm.BindProductItem();
            mm.Refresh();
            this.Close();
        }

        private void txtproductnme_Validated(object sender, EventArgs e)
        {
            mm.BindProductItem();
            mm.Refresh();
        }
    }
}

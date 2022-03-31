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
    public partial class SubscriptionPlan : Form
    {
        Login login = new Login();
        CheckConnection checkcon = new CheckConnection();
        public SubscriptionPlan()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataTable dtn = new DataTable();
            SettingModel settingModal = new SettingModel();
            DealerModel dealerListmdl = new DealerModel();
            WebAPIModelResponse webAPImdl = new WebAPIModelResponse();
            try
            {
                //Checking User Name and Password Valid
                login.AccessKey = txtUserId.Text.Trim();
                login.Authtoken = checkcon.GetMacAddress();
                login.SSoUrl = "/Api/ErawaanaAPI/setChecksubcribeplan/";
                dtn = WebAPI.Postsetsubcribeplan(login.SSoUrl, login).Result;
                if (dtn.Rows[0]["Status"].ToString() == "206")
                {
                    txtUserId.Text = string.Empty;
                    txtUserId.Focus();
                    ErrorModel errormdl = new ErrorModel();
                    errormdl.DateLog = DateTime.Now.ToString();
                    errormdl.Message = "subcribe Key Already Used";
                    minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);
                    System.Windows.Forms.MessageBox.Show(dtn.Rows[0]["MessageDiscription"].ToString(), "Subcribe Key", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (dtn.Rows[0]["Status"].ToString() == "210")
                {
                    txtUserId.Text = string.Empty;
                    txtUserId.Focus();
                    ErrorModel errormdl = new ErrorModel();
                    errormdl.DateLog = DateTime.Now.ToString();
                    errormdl.Message = "subcribe Key Already Used";
                    minesmart.Helper.ErrorLog.SaveErrorLogtableCustom(errormdl);
                    System.Windows.Forms.MessageBox.Show(dtn.Rows[0]["MessageDiscription"].ToString(), "Subcribe Key", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    this.Hide();
                    Form1 lgpage = new Form1();
                    lgpage.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Some Error Occured during this process.Please Try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //txtPassword.Text = "";
            txtUserId.Text = "";
        }
    }
}

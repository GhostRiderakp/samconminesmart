using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace minesmart
{
    public partial class ShowDialogBox : Form
    {
        public Dictionary<string, string> dta = new Dictionary<string, string>();
        public string key = string.Empty;
        public ShowDialogBox()
        {
            InitializeComponent();
        }
        private void ShowDialogBox_Load_1(object sender, EventArgs e)
        {
            btnPrint.DialogResult = DialogResult.OK; //print
            btnshare.DialogResult = DialogResult.Abort; //share
            btnclose.DialogResult = DialogResult.Cancel; // close
            btnviewdgms.DialogResult = DialogResult.Retry; // view it on DMG   
            switch (key)
            {
                case "Generate TP":
                    foreach (KeyValuePair<string, string> kvp in dta)
                    {
                        string pattern = "['\"]";
                        Regex.Replace(kvp.Key, pattern, string.Empty);
                        lblstatus.Text = "Generated";
                        lblrawannano.Text = kvp.Key.Replace("\"", string.Empty).Trim(); ;
                        //lblroyaltyamt.Text = kvp.Value;
                    }
                    label6.Visible = false;
                    lblroyaltyamt.Visible = false;
                    label1.Text = "Transit Pass Generated Successfully.. Please find the Transit Pass Details.,";
                    btnshare.Visible = false;
                    btnviewdgms.Visible = true;
                    break;
                case "Print Invoice":
                    foreach (KeyValuePair<string, string> kvp in dta)
                    {
                        lblstatus.Text = "Success";
                        lblrawannano.Text = kvp.Key;
                        lblroyaltyamt.Text = kvp.Value;
                    }
                    label1.Text = "Invoice Generated Successfully.,";
                    label4.Visible = false;
                    label6.Text = "Invoice Amount";
                    label2.Visible = false;
                    lblstatus.Visible = false;
                    btnviewdgms.Visible = false;
                    label6.Visible = false;
                    lblroyaltyamt.Visible = false;
                    btnshare.Visible = false;
                    btnclose.Visible = false;
                    lblrawannano.Visible = false;
                    break;
                case "WeightSlip":
                    foreach (KeyValuePair<string, string> kvp in dta)
                    {
                        lblstatus.Text = "Success";
                        lblrawannano.Text = kvp.Key;
                        lblroyaltyamt.Text = kvp.Value;
                    }
                    label1.Text = "Weight Slip Generated Successfully.,";
                    label4.Text = "Ticket Number";
                    label6.Text = "Type";
                    label2.Visible = false;
                    lblstatus.Visible = false;
                    btnviewdgms.Visible = false;
                    label6.Visible = false;
                    lblroyaltyamt.Visible = false;
                    btnshare.Visible = false;
                    btnclose.Visible = false;
                    break;
                case "TempWeightSlip":
                    foreach (KeyValuePair<string, string> kvp in dta)
                    {
                        lblstatus.Text = "Success";
                        lblrawannano.Text = kvp.Key;
                        lblroyaltyamt.Text = kvp.Value;
                    }
                    label1.Text = "Weight Slip Generated Successfully.,";
                    label4.Text = "Ticket Number";
                    label6.Text = "Type";
                    label2.Visible = false;
                    lblstatus.Visible = false;
                    btnviewdgms.Visible = false;
                    label6.Visible = false;
                    lblroyaltyamt.Visible = false;
                    btnshare.Visible = false;
                    btnclose.Visible = false;
                    break;
                case "Rawanna":
                    foreach (KeyValuePair<string, string> kvp in dta)
                    {
                        lblstatus.Text = "Confirmed";
                        lblrawannano.Text = kvp.Key;
                        lblroyaltyamt.Text = kvp.Value;
                    }
                    label1.Text = "Rawanna Generated Successfully.,";
                    label4.Text = "Rawanna Number";
                    label6.Text = "Type";
                    btnPrint.Text = "DMG/PRINT";
                    label2.Visible = false;
                    lblstatus.Visible = false;
                    btnviewdgms.Visible = false;
                    label6.Visible = false;
                    btnshare.Visible = false;
                    btnclose.Visible = false;
                    break;
                default:
                    foreach (KeyValuePair<string, string> kvp in dta)
                    {
                        lblstatus.Text = "Confirmed";
                        lblrawannano.Text = kvp.Key;
                        lblroyaltyamt.Text = kvp.Value;
                    }
                    break;

            }
        }
    }
}

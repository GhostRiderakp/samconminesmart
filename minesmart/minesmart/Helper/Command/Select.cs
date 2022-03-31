using minesmart.DGMS;
using minesmart.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace minesmart.Helper.Command
{
    public class Select
    {
        SqlCommand cmd;
        public Select()
        {
        }

        static string conn_str = Properties.Settings.Default.Connection_String;
        public DataTable UserMaster(Login userInfo)
        {
            string Message = string.Empty;
            DataTable dt = new DataTable();
            try
            {

                SqlConnection con = new SqlConnection(conn_str);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_GET_USER");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SSoid", userInfo.SSoid);
                cmd.Parameters.AddWithValue("@Password", userInfo.Password);
                cmd.Parameters.AddWithValue("@AccessKey", userInfo.AccessKey);
                cmd.Parameters.AddWithValue("@weightbridge", userInfo.Weightbridge);
                cmd.Parameters.AddWithValue("@Status", 0);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);

                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                //tran.Rollback();
            }

            return dt;
        }

        public DataTable LoadDefaultSetting(SettingModel settingModal)
        {
            DataTable dt = new DataTable();
            try
            {
                string Message = string.Empty;

                //SqlConnection con = new SqlConnection("Data Source=DOTNET01;Initial Catalog=minesmart;User ID=sa;Password=ssspl123");
                SqlConnection con = new SqlConnection(conn_str);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_GET_SETTING");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SOOId", settingModal.SsoId);
                cmd.Parameters.AddWithValue("@UserId", settingModal.UserId);
                cmd.Parameters.AddWithValue("@Status", 0);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
                con.Close();

            }
            catch (Exception ex)
            {

                // Display Message  
                System.Windows.Forms.MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return dt;
        }
        public DataTable LoadLanguageSetting()
        {
            string Message = string.Empty;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(conn_str);
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_GET_LANGUAGE_SETTING");
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            con.Close();
            return dt;
        }
        public static DataTable LoadconsigneeDetails(ConsigneeDetailsModel consigneeDetails)
        {
            DataTable dt = new DataTable();
            try
            {
                string Message = string.Empty;

                //SqlConnection con = new SqlConnection("Data Source=DOTNET01;Initial Catalog=minesmart;User ID=sa;Password=ssspl123");
                SqlConnection con = new SqlConnection(conn_str);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_GET_CONSIGNEEDETAILS");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SOOId", consigneeDetails.SsoId);
                cmd.Parameters.AddWithValue("@UserId", consigneeDetails.UserId);
                cmd.Parameters.AddWithValue("@MineralUsedFor", consigneeDetails.MineralUsedFor);
                cmd.Parameters.AddWithValue("@Status", 0);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
                con.Close();

            }
            catch (Exception ex)
            {

                // Display Message  
                System.Windows.Forms.MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return dt;
        }
        public static DataTable LoadupdateconsigneeDetails(ConsigneeDetailsModel consigneeDetails)
        {
            DataTable dt = new DataTable();
            try
            {
                string Message = string.Empty;

                //SqlConnection con = new SqlConnection("Data Source=DOTNET01;Initial Catalog=minesmart;User ID=sa;Password=ssspl123");
                SqlConnection con = new SqlConnection(conn_str);
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_GET_CONSIGNEEDETAILS");
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SOOId", consigneeDetails.SsoId);
                cmd.Parameters.AddWithValue("@UserId", consigneeDetails.UserId);
                cmd.Parameters.AddWithValue("@MineralUsedFor", consigneeDetails.MineralUsedFor);
                cmd.Parameters.AddWithValue("@Status", 1);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
                sqlDataAdapter.Fill(dt);
                con.Close();

            }
            catch (Exception ex)
            {

                // Display Message  
                System.Windows.Forms.MessageBox.Show(ex.Message, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return dt;
        }
    }
}

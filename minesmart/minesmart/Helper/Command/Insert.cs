using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using minesmart.Helper;
using minesmart.ViewModels;
using System.Data.Common;
using minesmart.DGMS;

/// <summary>
/// Summary description for Insert
/// </summary>
namespace minesmart.Helper.Command
{
    public class Insert
    {

        public Insert()
        {

        }
        //static string dbCon = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + "\\minesmart.mdf";
        //static string conn_str = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + dbCon + ";Integrated Security=True ";
        CheckConnection checkcon = new CheckConnection();
        static string conn_str = Properties.Settings.Default.Connection_String;
        public int SettingSerialPort(SettingModel _weightbridgeModal)
        {
            int Count = -1;
            using (SqlConnection con = new SqlConnection(conn_str))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SET_SETTING"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SOOID", _weightbridgeModal.SsoId);
                    cmd.Parameters.AddWithValue("@UserId", _weightbridgeModal.UserId);
                    cmd.Parameters.AddWithValue("@PortName", _weightbridgeModal.PortName);
                    cmd.Parameters.AddWithValue("@BaudRate", _weightbridgeModal.RatesBaud);
                    cmd.Parameters.AddWithValue("@DataBits", _weightbridgeModal.DataBits);
                    cmd.Parameters.AddWithValue("@StopBits", _weightbridgeModal.StopBits);
                    cmd.Parameters.AddWithValue("@ReaderCode", _weightbridgeModal.ReaderCode);
                    cmd.Parameters.AddWithValue("@SytemType", _weightbridgeModal.SytemType);
                    cmd.Parameters.AddWithValue("@IsReversed", _weightbridgeModal.IsReversed);
                    cmd.Parameters.AddWithValue("@Status", "SerialPort");
                    SqlParameter countParameter = new SqlParameter("@OutputId", 0);
                    countParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(countParameter);
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Count = int.Parse(cmd.Parameters["@OutputId"].Value.ToString());
                    con.Close();
                }

                return Count;
            }




        }
        public int SettingCamera(SettingModel _weightbridgeModal)
        {
            int Count = -1;
            using (SqlConnection con = new SqlConnection(conn_str))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SET_SETTING"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SOOID", _weightbridgeModal.SsoId);
                    cmd.Parameters.AddWithValue("@UserId", _weightbridgeModal.UserId);
                    cmd.Parameters.AddWithValue("@PortName", _weightbridgeModal.PortName);
                    cmd.Parameters.AddWithValue("@BaudRate", _weightbridgeModal.RatesBaud);
                    cmd.Parameters.AddWithValue("@DataBits", _weightbridgeModal.DataBits);
                    cmd.Parameters.AddWithValue("@StopBits", _weightbridgeModal.StopBits);
                    cmd.Parameters.AddWithValue("@ReaderCode", _weightbridgeModal.ReaderCode);
                    cmd.Parameters.AddWithValue("@SytemType", _weightbridgeModal.SytemType);
                    cmd.Parameters.AddWithValue("@IsReversed", _weightbridgeModal.IsReversed);
                    cmd.Parameters.AddWithValue("@CameraRearUrl", _weightbridgeModal.CameraRearUrl);
                    cmd.Parameters.AddWithValue("@CameraFrontUrl", _weightbridgeModal.CameraFrontUrl);
                    cmd.Parameters.AddWithValue("@Status", "Camera");
                    SqlParameter countParameter = new SqlParameter("@OutputId", 0);
                    countParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(countParameter);
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Count = int.Parse(cmd.Parameters["@OutputId"].Value.ToString());
                    con.Close();
                }

                return Count;
            }




        }
        public int SettingCommanSave(SettingModel _weightbridgeModal)
        {
            int Count = -1;
            using (SqlConnection con = new SqlConnection(conn_str))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SET_SETTING"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SOOID", _weightbridgeModal.SsoId);
                    cmd.Parameters.AddWithValue("@UserId", _weightbridgeModal.UserId);
                    cmd.Parameters.AddWithValue("@PortName", _weightbridgeModal.PortName);
                    cmd.Parameters.AddWithValue("@BaudRate", _weightbridgeModal.RatesBaud);
                    cmd.Parameters.AddWithValue("@DataBits", _weightbridgeModal.DataBits);
                    cmd.Parameters.AddWithValue("@StopBits", _weightbridgeModal.StopBits);
                    cmd.Parameters.AddWithValue("@ReaderCode", _weightbridgeModal.ReaderCode);
                    cmd.Parameters.AddWithValue("@SytemType", _weightbridgeModal.SytemType);
                    cmd.Parameters.AddWithValue("@IsReversed", _weightbridgeModal.IsReversed);
                    cmd.Parameters.AddWithValue("@CameraRearUrl", _weightbridgeModal.CameraRearUrl);
                    cmd.Parameters.AddWithValue("@CameraFrontUrl", _weightbridgeModal.CameraFrontUrl);
                    cmd.Parameters.AddWithValue("@WBBridgeNumber", _weightbridgeModal.WBBridgeNumber);
                    cmd.Parameters.AddWithValue("@WBAddress", _weightbridgeModal.WBAddress);
                    cmd.Parameters.AddWithValue("@WBCompanyName", _weightbridgeModal.WBCompanyName);
                    cmd.Parameters.AddWithValue("@WBEmailId", _weightbridgeModal.WBEmailId);
                    cmd.Parameters.AddWithValue("@WBMobileNumber", _weightbridgeModal.WBMobileNumber);
                    cmd.Parameters.AddWithValue("@Status", "Company");
                    SqlParameter countParameter = new SqlParameter("@OutputId", 0);
                    countParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(countParameter);
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Count = int.Parse(cmd.Parameters["@OutputId"].Value.ToString());
                    con.Close();
                }

                return Count;
            }
        }
        public static int SaveconsigneeDetails(ConsigneeDetailsModel _consigneeDetailsModel, string Type)
        {
            int Count = -1;
            using (SqlConnection con = new SqlConnection(conn_str))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SET_CONSIGNEEDETAILS"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SOOID", _consigneeDetailsModel.SsoId);
                    cmd.Parameters.AddWithValue("@UserId", _consigneeDetailsModel.UserId);
                    cmd.Parameters.AddWithValue("@Consigneedetail", _consigneeDetailsModel.consigneeDetails);
                    cmd.Parameters.AddWithValue("@Mineraluserfor", _consigneeDetailsModel.MineralUsedFor);
                    if (Type == "I")
                        cmd.Parameters.AddWithValue("@Status", "Consignee");
                    else
                        cmd.Parameters.AddWithValue("@Status", "upConsignee");
                    SqlParameter countParameter = new SqlParameter("@OutputId", 0);
                    countParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(countParameter);
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Count = int.Parse(cmd.Parameters["@OutputId"].Value.ToString());
                    con.Close();
                }

                return Count;
            }
        }

        public static int SaveConfirmERawanna(ConfirmERawannaModel _confirmErawanna)
        {
            int Count = -1;
            using (SqlConnection con = new SqlConnection(conn_str))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SET_CONFIRMERAWANNA"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SsoId", _confirmErawanna.SSOID);
                    cmd.Parameters.AddWithValue("@UserId", _confirmErawanna.UserId);
                    cmd.Parameters.AddWithValue("@MLNoId", _confirmErawanna.MLNoId);
                    cmd.Parameters.AddWithValue("@MLNo", _confirmErawanna.MLNo);
                    cmd.Parameters.AddWithValue("@MineralName", _confirmErawanna.MineralName);
                    cmd.Parameters.AddWithValue("@MineralId", _confirmErawanna.MineralId);
                    cmd.Parameters.AddWithValue("@MineralUserFor", _confirmErawanna.MineralUserFor);
                    cmd.Parameters.AddWithValue("@RoyaltySchedule", _confirmErawanna.RoyaltySchedule);
                    cmd.Parameters.AddWithValue("@RoyaltyScheduleRate", _confirmErawanna.RoyaltyScheduleRate);
                    cmd.Parameters.AddWithValue("@CollectionThrough", _confirmErawanna.CollectionThrough);
                    cmd.Parameters.AddWithValue("@ConsigneeName", _confirmErawanna.ConsigneeName);
                    cmd.Parameters.AddWithValue("@ConsigneeId", _confirmErawanna.ConsigneeId);
                    cmd.Parameters.AddWithValue("@ConsigneeAddress", _confirmErawanna.ConsigneeAddress);
                    cmd.Parameters.AddWithValue("@ConsigneeAddressId", _confirmErawanna.ConsigneeAddressId);
                    cmd.Parameters.AddWithValue("@ConsigneeGSTNo", _confirmErawanna.ConsigneeGSTNo);
                    cmd.Parameters.AddWithValue("@TransportDetails", _confirmErawanna.TransportDetails);
                    cmd.Parameters.AddWithValue("@Vechicle", _confirmErawanna.Vechicle);
                    cmd.Parameters.AddWithValue("@VechicleId", _confirmErawanna.VechicleId);
                    cmd.Parameters.AddWithValue("@Unit", _confirmErawanna.Unit);
                    cmd.Parameters.AddWithValue("@ApproximateTime", _confirmErawanna.ApproximateTime);
                    cmd.Parameters.AddWithValue("@Vechicleweight", _confirmErawanna.Vechicleweight);
                    cmd.Parameters.AddWithValue("@VechicleRegistration", _confirmErawanna.VechicleRegistration);
                    cmd.Parameters.AddWithValue("@DriverMobileNo", _confirmErawanna.DriverMobileNo);
                    cmd.Parameters.AddWithValue("@DriverName", _confirmErawanna.DriverName);
                    cmd.Parameters.AddWithValue("@TareWeight", _confirmErawanna.TareWeight);
                    cmd.Parameters.AddWithValue("@GrossWeight", _confirmErawanna.GrossWeight);
                    cmd.Parameters.AddWithValue("@TotalWeight", _confirmErawanna.TotalWeight);
                    cmd.Parameters.AddWithValue("@Comment", _confirmErawanna.Comment);
                    cmd.Parameters.AddWithValue("@FirstCameraImage", _confirmErawanna.FirstCameraImageurl);
                    cmd.Parameters.AddWithValue("@SecondCameraImage", _confirmErawanna.SecondCameraImage);
                    cmd.Parameters.AddWithValue("@Rawannastatus", _confirmErawanna.Rawannastatus);
                    cmd.Parameters.AddWithValue("@ERawannaNo", _confirmErawanna.ERawannaNo);
                    cmd.Parameters.AddWithValue("@Royaltyamount", _confirmErawanna.Royaltyamount);
                    cmd.Parameters.AddWithValue("@RawannaCount", _confirmErawanna.RawannaCount);
                    cmd.Parameters.AddWithValue("@RawannaDate", _confirmErawanna.RawannaDate);
                    cmd.Parameters.AddWithValue("@CreatedUser", _confirmErawanna.Isactive);
                    cmd.Parameters.AddWithValue("@CreateIpaddress", _confirmErawanna.IpAddress);
                    cmd.Parameters.AddWithValue("@Status", "CR");
                    SqlParameter countParameter = new SqlParameter("@OutputId", 0);
                    countParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(countParameter);
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Count = int.Parse(cmd.Parameters["@OutputId"].Value.ToString());
                    con.Close();
                }

                return Count;
            }
        }

        public static int SaveconsigneeAddress(ConsigneeDetailsModel _consigneeDetailsModel, string Type)
        {
            int Count = -1;
            using (SqlConnection con = new SqlConnection(conn_str))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SET_CONSIGNEEADDRESS"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SOOID", _consigneeDetailsModel.SsoId);
                    cmd.Parameters.AddWithValue("@UserId", _consigneeDetailsModel.UserId);
                    cmd.Parameters.AddWithValue("@ConsigneeId", _consigneeDetailsModel.ConsigneeId);
                    cmd.Parameters.AddWithValue("@ConsigneeAddress", _consigneeDetailsModel.consigneeAddressDetails);
                    if (Type == "I")
                        cmd.Parameters.AddWithValue("@Status", "CAddress");
                    else
                        cmd.Parameters.AddWithValue("@Status", "UCAddress");
                    SqlParameter countParameter = new SqlParameter("@OutputId", 0);
                    countParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(countParameter);
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Count = int.Parse(cmd.Parameters["@OutputId"].Value.ToString());
                    con.Close();
                }

                return Count;
            }
        }

        public static int SaveLeaselistMineral(ConsigneeDetailsModel _consigneeDetailsModel, string Type)
        {
            int Count = -1;
            using (SqlConnection con = new SqlConnection(conn_str))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SET_LEASELISTMINERALDETAILS"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SOOID", _consigneeDetailsModel.SsoId);
                    cmd.Parameters.AddWithValue("@UserId", _consigneeDetailsModel.UserId);
                    cmd.Parameters.AddWithValue("@LeaseMineralList", _consigneeDetailsModel.LeaseMineralList);
                    if (Type == "I")
                        cmd.Parameters.AddWithValue("@Status", "ILLM");
                    else
                        cmd.Parameters.AddWithValue("@Status", "ULLM");
                    SqlParameter countParameter = new SqlParameter("@OutputId", 0);
                    countParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(countParameter);
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Count = int.Parse(cmd.Parameters["@OutputId"].Value.ToString());
                    con.Close();
                }

                return Count;
            }
        }

        public static int SaveleaseListmineraldetail(LeaseListModel leaseListmdl, string Type)
        {
            int Count = -1;
            using (SqlConnection con = new SqlConnection(conn_str))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SET_LEASELISTMINERALDETAILS"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SSOID", leaseListmdl.SsoId);
                    cmd.Parameters.AddWithValue("@UserId", leaseListmdl.UserId);
                    cmd.Parameters.AddWithValue("@leaselistdt", leaseListmdl.leaselistdt);
                    if (Type == "I")
                        cmd.Parameters.AddWithValue("@Status", "ILLM");
                    else
                        cmd.Parameters.AddWithValue("@Status", "ULLM");
                    SqlParameter countParameter = new SqlParameter("@OutputId", 0);
                    countParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(countParameter);
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Count = int.Parse(cmd.Parameters["@OutputId"].Value.ToString());
                    con.Close();
                }

                return Count;
            }
        }

        public static int SavedealerInfoDetail(DealerModel Dealermdl, string Type)
        {
            int Count = -1;
            using (SqlConnection con = new SqlConnection(conn_str))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SET_DEALERINFODETAILS"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@SSOID", Dealermdl.SSOID);
                    cmd.Parameters.AddWithValue("@UserId", Dealermdl.UserId);
                    cmd.Parameters.AddWithValue("@DealerInfolist", Dealermdl.DealerInfolist);
                    if (Type == "I")
                        cmd.Parameters.AddWithValue("@Status", "IDL");
                    else
                        cmd.Parameters.AddWithValue("@Status", "UDL");
                    SqlParameter countParameter = new SqlParameter("@OutputId", 0);
                    countParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(countParameter);
                    con.Open();
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    Count = int.Parse(cmd.Parameters["@OutputId"].Value.ToString());
                    con.Close();
                }

                return Count;
            }
        }

        public static int ExecuteNonQuery(string sSQL)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conn_str);

            try
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sSQL;
                cmd.Connection = con;

                int ReturnValue = 0;
                ReturnValue = Convert.ToInt32(cmd.ExecuteScalar());
                return ReturnValue;
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        public string ExecuteScaler(string sSQL)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(conn_str);
            try
            {
                con.Open();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sSQL;
                cmd.Connection = con;
                return cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                cmd.Dispose();
                con.Close();
            }
        }
        //public bool Contact(string EmpId, SqlTransaction tran, SqlConnection con)
        //{
        //    bool Return = false;
        //    try
        //    {
        //        cmd = new SqlCommand();
        //        cmd.Connection = con;
        //        cmd.Transaction = tran;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            cmd.CommandText = "insert into ContactInfo(EmpId,ContactType,Detail,IsActive) values ('" + EmpId + "','" + CType + "','" + dt.Rows[i][0].ToString() + "',1)";
        //            cmd.ExecuteNonQuery();
        //        }
        //        Return = true;
        //    }
        //    catch
        //    {
        //        tran.Rollback();
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //    }
        //    return Return;
        //}



    }   //End Of Class
}
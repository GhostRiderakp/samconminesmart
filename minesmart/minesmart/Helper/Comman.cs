using ERawaana.Helper;
using minesmart.DGMS;
using minesmart.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.IO;

namespace minesmart.Helper
{

    public class Comman
    {
        #region Encrypt / Decrypt
        public static string Encrypt(string Message)
        {
            //string Password = "cyrustechnoedge";
            string Password = "ACEGIK";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Password));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

        public static string EncryptData(string encryptString)
        {
            string EncryptionKey = "012Educategirls34ED56CodeKey789";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string DecryptData(string cipherText)
        {
            string EncryptionKey = "012Educategirls34ED56CodeKey789";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }


        public static string Decrypt(string Message)
        {
            string Password = "ACEGIK";
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Password));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }

        public static string Encrypt1(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            string key = (string)settingsReader.GetValue("SecurityKey",
                                                             typeof(String));
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt1(string cipherString)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!
            string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        #endregion

        public static WebAPIModelResponse LogoutUser(string ssoid, long companyid, long userid)
        {
            WebAPIModelResponse webAPImdl = new WebAPIModelResponse();
            try
            {
                CheckConnection checkcon = new CheckConnection();

                Login login = new Login();
                login.SSoid = ssoid;
                login.CompanyId = companyid;
                login.MacAddress = checkcon.GetMacAddress();
                login.IpAddress = checkcon.GetIPAddress();
                login.Userid = userid;
                login.SSoUrl = "/Api/ErawaanaAPI/CheckUserLogout/";
                webAPImdl = WebAPI.PostCheckUserLogout(login.SSoUrl, login).Result;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Some Error Occured during this process.Please Try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return webAPImdl;
        }
        /// <summary>
        /// Common function >> show pring share pop up for all pages.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="key"></param>
        public static async void handleShare(Dictionary<string, string> response, String key)
        {
            ShowDialogBox box = new ShowDialogBox();
            box.dta = response;
            box.key = key;
            DialogResult d = box.ShowDialog();
            if (d == DialogResult.Abort) //share
            {
                box.Close();
            }
            else if (d == DialogResult.OK) //print
            {
                foreach (KeyValuePair<string, string> kvp in response)
                {
                    if (kvp.Value == "WeightSlip")
                    {
                        var targetURL = minesmart.Helper.HttpServiceUrl.PrintWeightSlipWebsiteUrl + Comman.EncryptData(kvp.Key.Replace("\"", string.Empty).Trim().ToString()) + "&UId=" + Comman.EncryptData(WebAPIModelResponse.UserCredtentialId);
                        var psi = new ProcessStartInfo
                        {
                            FileName = targetURL,
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                        break;
                    }
                    else if (kvp.Value == "Generate TP")
                    {
                        var targetURL = minesmart.Helper.HttpServiceUrl.PrintCTPWebsiteUrl + Comman.EncryptData(kvp.Key.Replace("\"", string.Empty).Trim().ToString());
                        var psi = new ProcessStartInfo
                        {
                            FileName = targetURL,
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                        break;
                    }
                    else if (kvp.Value == "Print Invoice")
                    {
                        var targetURL = minesmart.Helper.HttpServiceUrl.PrintCTPWebsiteUrl + Comman.EncryptData(kvp.Key.Replace("\"", string.Empty).Trim().ToString());
                        var psi = new ProcessStartInfo
                        {
                            FileName = targetURL,
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                        break;
                    }
                    else if (kvp.Value == "TempWeightSlip")
                    {
                        var targetURL = minesmart.Helper.HttpServiceUrl.PrintTempWeightSlipWebsiteUrl + Comman.EncryptData(kvp.Key.Replace("\"", string.Empty).Trim().ToString()) + "&UId=" + Comman.EncryptData(WebAPIModelResponse.UserCredtentialId);
                        var psi = new ProcessStartInfo
                        {
                            FileName = targetURL,
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                        break;
                    }
                    else
                    {
                        var targetURL = minesmart.Helper.HttpServiceUrl.DMGravannaStatusURL + Comman.EncryptData(kvp.Key.Replace("\"", string.Empty).Trim().ToString());
                        var psi = new ProcessStartInfo
                        {
                            FileName = targetURL,
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                        break;
                    }

                }
                box.Close();
            }
            else
            {
                if (d == DialogResult.Retry) //view it on DMG
                {
                    foreach (KeyValuePair<string, string> kvp in response)
                    {
                        var targetURL = minesmart.Helper.HttpServiceUrl.DMGravannaStatusURL + kvp.Key.Replace("\"", string.Empty).Trim().ToString();
                        var psi = new ProcessStartInfo
                        {
                            FileName = targetURL,
                            UseShellExecute = true
                        };
                        Process.Start(psi);
                        break;
                    }

                }
                //save For Hsitory..
                //  shareApiandPrint(request, response, new Dictionary<string, string>(), key);
                box.Close();
            }

        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace minesmart.DGMS
{
    public class SettingModel : INotifyPropertyChanged
    {
        public int SId { get; set; }

        public long UserCredid { get; set; }

        public long UserProfileid { get; set; }
        public long UserId { get; set; }
        public string ProductId { get; set; }

        public long ConsigneeNameId { get; set; }
        public string SsoId { get; set; }
        public long CompanyId { get; set; }
        public long ClientId { get; set; }

        public string weightbridgeNo { get; set; }

        public string weightbridge { get; set; }

        public string Vehicleweight { get; set; }
        public string SearchDetails { get; set; }
        public string VehicleNo { get; set; }
        public string PortName { get; set; }
        public string DeviceMode { get; set; }
        public string DeviceType { get; set; }
        public string PostUrl { get; set; }
        public string BaudRate { get; set; }

        public string LanguageSetting { get; set; }

        public string PrintDetails { get; set; }
        public string[] PortNameCollection { get; set; }

        //private string _BaudRate;

        public string Authtoken { get; set; }
        public string RatesBaud
        {
            get;
            set;

        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        //public event PropertyChangedEventHandler PropertyChanged;
        public string DataBits { get; set; }

        public string StopBits { get; set; }

        public string ParityNew { get; set; }

        public string StopBitsNew { get; set; }

        public string Parity { get; set; }

        public string ReaderCode { get; set; }

        public string SytemType { get; set; }

        public string IsReversed { get; set; }

        public string WBCompanyName { get; set; }

        public string WBMobileNumber { get; set; }

        public string WBAddress { get; set; }

        public string WBBridgeNumber { get; set; }

        public string WBEmailId { get; set; }

        public string CameraRearUrl { get; set; }

        public string CameraFrontUrl { get; set; }
        public string CameraIPAddress { get; set; }
        public string CameraUserName { get; set; }
        public string CameraUserPassword { get; set; }

        public string CamerabackIPAddress { get; set; }
        public string CamerabackUserName { get; set; }
        public string CamerabackUserPassword { get; set; }

        public string IsActive { get; set; }

        public DateTime CreateDate { get; set; }

        public string Success { get; set; }

        public string Message { get; set; }
        public string MineralId { get; set; }
        public string DriverName { get; set; }

        public string DriverMobile { get; set; }

        public string TareWeight { get; set; }
        public string GrossWeight { get; set; }
        public string TotalWeight { get; set; }

        public string Remark { get; set; }

        public string MineralName { get; set; }

        public string TicketNumber { get; set; }

        public string WSDate { get; set; }

    }
    public class PortnameModel
    {
        public string portnamevalue { get; set; }
    }
    public class BaudRate
    {
        public int Id { set; get; }
        public string name { set; get; }
    }

    public class Databits
    {
        public int Id { set; get; }
        public string name { set; get; }
    }
    public class Paritys
    {
        public int Id { set; get; }
        public string name { set; get; }
    }
    public class StopBit
    {
        public int Id { set; get; }
        public string name { set; get; }
    }
    public class IsReverseds
    {
        public int Id { set; get; }
        public string name { set; get; }
    }

    public class Mineraluserfor
    {
        public int Id { set; get; }
        public string name { set; get; }
    }
    public class TransportType
    {
        public int Id { set; get; }
        public string name { set; get; }
    }
    public class stype
    {
        public int Id { set; get; }
        public string name { set; get; }
    }
}

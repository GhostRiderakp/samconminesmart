using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace minesmart.ViewModels
{
    public class ConfirmERawannaModel : INotifyDataErrorInfo
    {
        public long CRId { get; set; }
        private readonly Dictionary<string, string> _validationErrors = new Dictionary<string, string>();

        public string SSOID { get; set; }

        public long CompanyId { get; set; }
        public long ClientId { get; set; }
        public string WeightbridgeNo { get; set; }
        public DataTable dtrawwana { get; set; }

        public long UserCredid { get; set; }

        public long UserProfileid { get; set; }
        public long UserId { get; set; }
        public long MLNoId { get; set; }
        public string MLNo { get; set; }

        public long dealerId { get; set; }
        public string dealerName { get; set; }
        public string Location { get; set; }
        public string LocationId { get; set; }
        public string MineralName { get; set; }
        public long MineralId { get; set; }
        public string MineralUserFor { get; set; }

        public string success { get; set; }
        public string RoyaltySchedule { get; set; }
        public string RoyaltyScheduleRate { get; set; }
        public string CollectionThrough { get; set; }
        public string ConsigneeName { get; set; }
        public long ConsigneeId { get; set; }
        public string ConsigneeAddress { get; set; }
        public long ConsigneeAddressId { get; set; }

        public string ConsigneeGSTNo { get; set; }

        public string TransportDetails { get; set; }
        public string Vechicle { get; set; }
        public long VechicleId { get; set; }
        public string Unit { get; set; }
        public string ApproximateTime { get; set; }

        public string Approximatedistance { get; set; }
        public string Vechicleweight { get; set; }
        public string VechicleRegistration { get; set; }
        public string DriverMobileNo { get; set; }
        public string DriverName { get; set; }
        public string TareWeight { get; set; }
        public string GrossWeight { get; set; }
        public string TotalWeight { get; set; }
        public string Comment { get; set; }
        public string FirstCameraImageurl { get; set; }
        public string SecondCameraImage { get; set; }

        public long CreatedUser { get; set; }
        public bool Isactive { get; set; }

        public string PostUrl { get; set; }

        public string IpAddress { get; set; }

        public string Rawannastatus { get; set; }

        public string ERawannaNo { get; set; }

        public string Royaltyamount { get; set; }

        public string RawannaCount { get; set; }

        public DateTime RawannaDate { get; set; }

        public string ErrorCode { get; set; }

        public string Status { get; set; }

        public string Authtoken { get; set; }

        public string Destination { get; set; }

        public string TotalCgstAmount { get; set; }

        public string TotalSgstAmount { get; set; }

        public string CgstPrecent { get; set; }

        public string SgstPercennt { get; set; }
        public string HSNNumber { get; set; }

        public string InvoiceNo { get; set; }

        public string Invoicedate { get; set; }
        public long InvoiceId { get; set; }
        

        public string TransitPassNumber { get; set; }

        public string MessageDiscription { get; set; }


        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                //validate:
                if (_text?.Length < 3)
                    _validationErrors[nameof(Text)] = "Too short...";
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(Text)));
            }
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public bool HasErrors => _validationErrors.Count > 0;
        public IEnumerable GetErrors(string propertyName) =>
            _validationErrors.TryGetValue(propertyName, out string error) ? new string[1] { error } : null;


    }

   
}

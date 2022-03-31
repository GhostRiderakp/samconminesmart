using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minesmart.ViewModels
{
    public class Login : INotifyPropertyChanged
    {
        public string SSoid { get; set; }

        public long Userid { get; set; }

        public long UserCredid { get; set; }

        public long UserProfileid { get; set; }

        public long CompanyId { get; set; }

        public long clientId { get; set; }

        public string Weightbridge { get; set; }

        public string Weightbridgeno { get; set; }

        public string Password { get; set; }

        public string Authtoken { get; set; }
        public string AccessKey { get; set; }

        public string SSoUrl { get; set; }

        public string PostUrl { get; set; }

        public int Action { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string LanguageSetting { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    public class consigneeDet
    {
        public string ErrorMsg { get; set; }
        public string ShowResult { get; set; }


    }

    public class ResultData
    {

        public string Status { get; set; }

        public string consignee { get; set; }

        public List<consigneelist> consigneeDetail { get; set; }
    }

    public class consigneelist
    {
        public string consigneedistance { get; set; }

        public string consigneename { get; set; }

        public long consigneeId { get; set; }
    }
}

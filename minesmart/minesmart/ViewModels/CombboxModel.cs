using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
 
namespace minesmart.ViewModels
{
    public class CombboxModel : INotifyPropertyChanged
    {
        private int _baudrateId;
        private string _baudrate;

        public int baudrate
        {
            get { return _baudrateId; }
            set
            {
                if (_baudrateId != value)
                {
                    _baudrateId = value;
                    
                }
                 
                
            }
        }
       

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public string baudrateName
        {
            get { return _baudrate; }
            set
            {
                _baudrate = value;
              
            }
        }

        
        private int _databits = 0;

        public int databits
        {
            get { return _databits; }
            set
            {
                _databits = value;
                // OnPropertyChanged("Amount");
            }
        }

        private int _Parity = 0;

        public int Parity
        {
            get { return _Parity; }
            set
            {
                _Parity = value;
                // OnPropertyChanged("Amount");
            }
        }

        private int _StopBits = 0;

        public int StopBits
        {
            get { return _StopBits; }
            set
            {
                _StopBits = value;
                // OnPropertyChanged("Amount");
            }
        }

        private int _IsReversed = 0;

        public int IsReversed
        {
            get { return _IsReversed; }
            set
            {
                _IsReversed = value;
                // OnPropertyChanged("Amount");
            }
        }
        
    }
   

}
 

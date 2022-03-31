using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace minesmart.ViewModels
{
    public class Language : INotifyPropertyChanged
    {
        private string _languageValue;
        private string _languageId;

        public string LanguageValue
        {
            get
            {
                return _languageValue;
            }
            set
            {
                _languageValue = value;
                OnPropertyChanged(nameof(LanguageValue));
            }
        }

        public string LanguageId
        {
            get
            {
                return _languageId;
            }
            set
            {
                _languageId = value;
                OnPropertyChanged(nameof(LanguageId));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace minesmart.ViewModels
{
    public class ErrorModel
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public string EventName { get; set; }
        public string PageName { get; set; }
        public string FileName { get; set; }
        public string DateLog { get; set; }
        public string LineNo { get; set; }
        public string Sendtype { get; set; }
        public string PostUrl { get; set; }
        public Exception Exp { get; set; }
    }

}

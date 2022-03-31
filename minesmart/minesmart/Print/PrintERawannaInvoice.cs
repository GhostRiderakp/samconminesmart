using minesmart.DGMS;
using minesmart.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minesmart.Print
{
    public partial class PrintERawannaInvoice : Form
    {
        public PrintERawannaInvoice()
        {
            if (Convert.ToString(WebAPIModelResponse.eRawannaNo) != null)
            {
                var eRawannaNo = Convert.ToString(WebAPIModelResponse.eRawannaNo);
                var ssoid = Convert.ToString(WebAPIModelResponse.SsoId);
                var invoid = Convert.ToString(WebAPIModelResponse.InvoiceId);
                webBrowser1.Navigate("http://minesmart.in/Rawaana.aspx?Id=" + eRawannaNo + "&WId=" + ssoid + "&invoid=" + invoid);
            }
            InitializeComponent();
        }
    }
}

using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraPrinting;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    public partial class AP_EX_register : DevExpress.XtraReports.UI.XtraReport
    {
        public AP_EX_register(int index,string name)
        {
            InitializeComponent();
            this.Parameters["id"].Value = index;
            label1.Text = "سجل إمتحانات : " + name;
        }

    }
}

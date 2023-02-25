using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    public partial class exam_result : DevExpress.XtraReports.UI.XtraReport
    {
        public exam_result(int id , DateTime ex_date)
        {
            InitializeComponent();
            label2.Text = ex_date.ToShortDateString();
            Parameters[0].Value = id;
        }

    }
}

using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    public partial class test : DevExpress.XtraReports.UI.XtraReport
    {
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;

        public test()
        {
            InitializeComponent();

            






        }

    }
}

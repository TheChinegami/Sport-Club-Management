using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE8
{
    public partial class page8 : DevExpress.XtraEditors.XtraUserControl
    {
        XtraReport rp = new XtraReport();
        XlsxExportOptions excel = new XlsxExportOptions(new TextExportMode(), true, true, true);
        public page8(Form Previous)
        {
            InitializeComponent();
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //reports.affichage_act report1 = new reports.affichage_act();
            //report1.CreateDocument();
            //for (int i = 0; i < 2; i++)
            //{
            //    reports.affichage_pass report2 = new reports.affichage_pass();
            //    report2.CreateDocument();
            //    //report1.CreateDocument();
            //    //report1.Pages.AddRange(report2.Pages);

            //}
            //report1.PrintingSystem.ContinuousPageNumbering = true;
            new ReportPrintTool(new reports.exam_result(24,new DateTime(2022,8,2))).ShowPreview();
            new ReportPrintTool(new reports.exam_result(25, new DateTime(2022, 8, 2))).ShowPreview();
            new ReportPrintTool(new reports.exam_result(27, new DateTime(2022, 8, 2))).ShowPreview();
            new ReportPrintTool(new reports.exam_result(28, new DateTime(2022, 8, 2))).ShowPreview();
        }
    }
}

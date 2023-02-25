using DevExpress.XtraEditors;
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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE3
{
    public partial class Select_a_month : DevExpress.XtraEditors.XtraForm
    {
        int anne;
        Form Previous;

        public Select_a_month(int anne , Form Previous)
        {
            InitializeComponent();

            this.anne = anne;
            this.Previous = Previous;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                new ReportPrintTool(new reports.PAY_register(anne, comboBoxEdit2.SelectedIndex,1)).ShowPreview();
            }
            catch
            {

            }
            this.Close();
        }

        private void Select_a_month_FormClosing(object sender, FormClosingEventArgs e)
        {
            Previous.Show();
        }
    }
}
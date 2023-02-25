using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE4
{
    public partial class add_exam : DevExpress.XtraEditors.XtraForm
    {
        Form previous;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;

        public add_exam(Form previous)
        {
            InitializeComponent();

            this.previous = previous;

            dateEdit1.DateTime = DateTime.Now;
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if (dateEdit1.Text == "")
            {
                simpleButton5.Enabled = false;
            }
            else
            {
                simpleButton5.Enabled = true;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_exam_FormClosing(object sender, FormClosingEventArgs e)
        {
            previous.Show();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            DAL.Open();
            para = new SqlParameter[1];
            para[0] = new SqlParameter("date", dateEdit1.DateTime);
            DAL.procedure("EX_ajout",para);
            DAL.Close();
            this.Close();
        }
    }
}
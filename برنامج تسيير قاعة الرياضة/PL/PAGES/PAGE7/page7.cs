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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE7
{
    public partial class page7 : DevExpress.XtraEditors.XtraUserControl
    {
        Form Previous;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        public page7(Form Previous)
        {
            InitializeComponent();

            this.Previous = Previous;

            para = new SqlParameter[1];
            para[0] = new SqlParameter("rang", user.rang);
            DAL.Open();
            dataGridView1.DataSource = DAL.Read_data("UTIL_affichage", para);
            DAL.Close();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 13, FontStyle.Regular);
        }


        private void simpleButton5_Click(object sender, EventArgs e)
        {
            new PAGES.PAGE7.add_edit_user(-1, Previous).Show();
            Previous.Visible = false;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                new PAGES.PAGE7.add_edit_user(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), Previous).Show();
                Previous.Visible = false;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                new PAGES.MESSAGES.confirmation(Previous, Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), "UTIL_suppression").Show();
                Previous.Visible = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                new PAGES.PAGE7.user_details(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), Previous).Show();
                Previous.Visible = false;
            }
        }

        private void page7_VisibleChanged(object sender, EventArgs e)
        {
            para = new SqlParameter[1];
            para[0] = new SqlParameter("rang", user.rang);
            DAL.Open();
            dataGridView1.DataSource = DAL.Read_data("UTIL_affichage", para);
            DAL.Close();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 13, FontStyle.Regular);
        }
    }
}

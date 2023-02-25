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
    public partial class add_subscriber : DevExpress.XtraEditors.XtraForm
    {
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;
        int test_id;
        Form Previous;
        public add_subscriber(int test_id , Form Previous)
        {
            InitializeComponent();

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);
            selected.Width = dataGridView1.Width / 4;

            this.test_id = test_id;
            this.Previous = Previous;

            DAL.Open();
            para = new SqlParameter[1];
            para[0] = new SqlParameter("id", test_id);
            dr = DAL.Read("CAN_list non candidats", para);
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr.GetInt32(0),"false", dr.GetString(1));
            }
            DAL.Close();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value.ToString() == "true")
                {
                    DAL.Open();
                    para = new SqlParameter[2];
                    para[0] = new SqlParameter("idap", Convert.ToInt32(row.Cells[0].Value));
                    para[1] = new SqlParameter("idex", test_id);
                    DAL.procedure("CAN_ajout", para);
                    DAL.Close();
                }
            }
            Previous.Visible = true ;
            this.Close();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Previous.Visible = true;
            this.Close();
        }

        private void add_subscriber_FormClosing(object sender, FormClosingEventArgs e)
        {
            Previous.Visible = true;
        }
    }
}
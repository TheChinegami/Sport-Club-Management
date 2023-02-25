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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE2
{
    public partial class absence : DevExpress.XtraEditors.XtraForm
    {
        DateTime date;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;
        string[] data;
        Form Previous;
        int index;
        Point point;

        public absence(DateTime date , Form Previous)
        {
            InitializeComponent();
            this.date = date;
            this.Previous = Previous;
            label3.Text = date.ToShortDateString();
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView2.DefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);
            Column3.Width = dataGridView2.Width / 4;
            show_data();
        }

        void show_data()
        {
            dataGridView2.Rows.Clear();
            para = new SqlParameter[1];
            data = new string[3];
            para[0] = new SqlParameter("date", date.Month.ToString()+"/"+date.Day.ToString() +"/" +date.Year.ToString());
            DAL.Open();
            dr = DAL.Read("PRE_affichage", para);
            while (dr.Read())
            {
                data[0] = dr.GetInt32(0).ToString();
                data[1] = dr.GetString(1);
                data[2] = dr.GetString(2);
                dataGridView2.Rows.Add(data);
            }
            DAL.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                index = Convert.ToInt32(row.Cells[0].Value.ToString());
                if (row.Cells[2].Value.ToString() == "حاضر")
                {
                    DAL.Open();
                    para = new SqlParameter[2];
                    para[0] = new SqlParameter("id", index);
                    para[1] = new SqlParameter("date_session",date);
                    DAL.procedure("PRE_ajout",para);
                    DAL.Close();
                }
                else
                {
                    DAL.Open();
                    para = new SqlParameter[2];
                    para[0] = new SqlParameter("id", index);
                    para[1] = new SqlParameter("date", date);
                    DAL.procedure("PRE_supression", para);
                    DAL.Close();
                }
            }
            this.Close();
        }

        private void absence_FormClosing(object sender, FormClosingEventArgs e)
        {
            Previous.Visible = true;
        }

        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex != -1)
            {
                pictureBox1.Show();
                point = dataGridView2.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Location;
                pictureBox1.Location = new Point(point.X + dataGridView2.Location.X + 170, point.Y + dataGridView2.Location.Y + 38);
            }
        }

        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            pictureBox1.Hide();
        }
    }
}
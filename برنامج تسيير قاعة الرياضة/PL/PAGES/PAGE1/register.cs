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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE1
{
    public partial class register : DevExpress.XtraEditors.XtraForm
    {
        Form Previous;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        int id ,count;

        public register(Form Previous)
        {
            InitializeComponent();

            this.Previous = Previous;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView2.DefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);
            panel5.Width = panel3.Width / 2;

            show_data();

            if (user.acce.Contains("-6-"))
            {
                panel7.Visible = true;
            }
        }

        void show_data()
        {
            DAL.Open();
            para = new SqlParameter[1];
            para[0] = new SqlParameter("nom", textBox1.Text);
            dataGridView1.DataSource = DAL.Read_data("AP_affichage list",para);
            dataGridView1.Columns[0].Visible = false;
            //dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            DAL.Close();
            color1();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                try
                {
                    id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    para = new SqlParameter[1];
                    para[0] = new SqlParameter("id", id);
                    DAL.Open();
                    dataGridView2.DataSource = DAL.Read_data("HE_affichage", para);
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[3].Visible = false;
                    DAL.Close();
                    color2();
                }
                catch
                {

                }
            }
        }

        private void register_FormClosing(object sender, FormClosingEventArgs e)
        {
            Previous.Visible = true;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count != 0)
            {
                this.Hide();
                new PAGES.MESSAGES.confirmation(this, Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value), "HE_suppression").Show();
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count != 0)
            {
                count = 0;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells[2].Value.ToString() == "")
                    {
                        count++;
                    }
                }
                this.Hide();
                new PAGES.PAGE1.edit_out_of_training(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value), dataGridView2.CurrentRow.Cells[2].Value.ToString(), dataGridView2.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), this,count,1).Show();
            }
        }

        private void dataGridView2_VisibleChanged(object sender, EventArgs e)
        {
            show_data();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            count = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[2].Value.ToString() == "")
                {
                    count++;
                }
            }
            this.Hide();
            new PAGES.PAGE1.edit_out_of_training(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value),-1, "", DateTime.Now.ToShortDateString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), this, count,0).Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            show_data();
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            color1();
        }

        private void register_Resize(object sender, EventArgs e)
        {
            panel5.Width = panel3.Width / 2;
        }

        public void color1()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[3].Value.ToString().Contains("r"))
                {
                    row.Cells[1].Style.BackColor = Color.Pink;
                    row.Cells[1].Style.SelectionBackColor = Color.DeepPink;
                    row.Cells[2].Style.BackColor = Color.White;
                    row.Cells[2].Style.SelectionBackColor = Color.FromArgb(0, 120, 215);
                }
                if (row.Cells[3].Value.ToString().Contains("w"))
                {
                    row.Cells[1].Style.BackColor = Color.White;
                    row.Cells[1].Style.SelectionBackColor = Color.FromArgb(0, 120, 215);
                    row.Cells[2].Style.BackColor = Color.White;
                    row.Cells[2].Style.SelectionBackColor = Color.FromArgb(0, 120, 215);
                }
                if (row.Cells[3].Value.ToString().Contains("f"))
                {
                    row.Cells[2].Style.BackColor = Color.Pink;
                    row.Cells[2].Style.SelectionBackColor = Color.DeepPink;
                }
            }
        }

        private void dataGridView2_Sorted(object sender, EventArgs e)
        {
            color2();
        }

        public void color2()
        {
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells[3].Value.ToString().Contains("r"))
                {
                    row.Cells[1].Style.BackColor = Color.Pink;
                    row.Cells[1].Style.SelectionBackColor = Color.DeepPink;
                    row.Cells[2].Style.BackColor = Color.Pink;
                    row.Cells[2].Style.SelectionBackColor = Color.DeepPink;
                }
                if (row.Cells[3].Value.ToString().Contains("w"))
                {
                    row.Cells[1].Style.BackColor = Color.White;
                    row.Cells[1].Style.SelectionBackColor = Color.FromArgb(0, 120, 215);
                    row.Cells[2].Style.BackColor = Color.White;
                    row.Cells[2].Style.SelectionBackColor = Color.FromArgb(0, 120, 215);
                }
            }
        }
    }
}
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
using DevExpress.XtraReports.UI;

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE4
{
    public partial class page4 : DevExpress.XtraEditors.XtraUserControl
    {
        Form Previous;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;
        int id;
        Point point;

        public page4(Form Previous)
        {
            InitializeComponent();

            this.Previous = Previous;

            comboBoxEdit1.Text = DateTime.Now.Year.ToString();

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView2.DefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);

            if (user.acce.Contains("-14-"))
            {
                panel4.Visible = true;
                panel7.Visible = true;
                dataGridView2.Columns[2].ReadOnly = false ;
            }
            if (user.acce.Contains("-17-"))
            {
                save.Visible = true;
            }
        }

        private void page4_Resize(object sender, EventArgs e)
        {
            Column3.Width = dataGridView2.Width / 4;
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DAL.Open();
            para = new SqlParameter[1];
            para[0] = new SqlParameter("anne", comboBoxEdit1.Text);
            dataGridView1.DataSource = DAL.Read_data("EX_affichage", para);
            DAL.Close();
            dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                try
                {
                    dataGridView2.Rows.Clear();
                    id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    DAL.Open();
                    para = new SqlParameter[1];
                    para[0] = new SqlParameter("id", id);
                    dr = DAL.Read("CAN_list candidats", para);
                    while (dr.Read())
                    {
                        dataGridView2.Rows.Add(dr.GetInt32(0), dr.GetString(1), dr.GetString(2));
                    }
                    DAL.Close();
                }
                catch (Exception ex)
                {
                    dataGridView2.Rows.Clear();
                }
            }
            else
            {
                dataGridView2.Rows.Clear();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                new PL.PAGES.PAGE4.add_subscriber(id , Previous).Show();
                Previous.Visible = false;
            }
        }

        private void page4_VisibleChanged(object sender, EventArgs e)
        {
            dataGridView2_SelectionChanged(sender, e);
            comboBoxEdit1_SelectedIndexChanged(sender, e);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            new PAGES.PAGE4.add_exam(Previous).Show();
            Previous.Hide();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                Previous.Hide();
                new PAGES.MESSAGES.confirmation(Previous, Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), "EX_supression").Show();
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count != 0)
            {
                DAL.Open();
                para = new SqlParameter[1];
                para[0] = new SqlParameter("id", Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value));
                DAL.procedure("CAN_suppression", para);
                DAL.Close();

                dataGridView2_SelectionChanged(sender, e);
            }

        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Rows.Count!=0 && dataGridView2.CurrentCell.ColumnIndex == 2)
            {
                DAL.Open();
                para = new SqlParameter[2];
                para[0] = new SqlParameter("id", Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value));
                para[1] = new SqlParameter("resultat", dataGridView2.CurrentCell.Value.ToString());
                DAL.procedure("CAN_modif", para);
                DAL.Close();
            }
        }

        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex != -1)
            {
                pictureBox1.Show();
                point = dataGridView2.GetCellDisplayRectangle(e.ColumnIndex - 1, e.RowIndex, false).Location;
                pictureBox1.Location = new Point(point.X + dataGridView2.Location.X - 20 , point.Y + dataGridView2.Location.Y + 38);
            }
        }

        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            pictureBox1.Hide();
        }

        private void save_MouseEnter(object sender, EventArgs e)
        {
            save_mess.Visible = true;
            save.Location = new Point(save.Location.X - 2, save.Location.Y - 2);
            save.Size = new Size(save.Size.Width + 4, save.Size.Height + 4);
        }

        private void save_MouseLeave(object sender, EventArgs e)
        {
            save_mess.Visible = false;
            save.Location = new Point(save.Location.X + 2, save.Location.Y + 2);
            save.Size = new Size(save.Size.Width - 4, save.Size.Height - 4);
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 0)
            {
                try
                {
                    new ReportPrintTool(new reports.exam_result(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value),Convert.ToDateTime(dataGridView1.CurrentRow.Cells[1].Value))).ShowPreview();
                }
                catch
                {

                }
            }
        }
    }
}

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
    public partial class page2 : DevExpress.XtraEditors.XtraUserControl
    {
        Form Previous;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        int rows ;
        DateTime startofthemonth;
        DateTime date;
        int days;
        int dayoftheweek;
        int day;
        List<int> sessions;
        public page2(Form Previous)
        {
            InitializeComponent();

            this.Previous = Previous;

            comboBoxEdit1.Text = DateTime.Now.Year.ToString();
            comboBoxEdit2.SelectedIndex = DateTime.Now.Month - 1;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 18, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 18, FontStyle.Bold);

            if (user.acce.Contains("-8-"))
            {
                panel1.Visible = true;
                simpleButton5.Enabled = true;
                simpleButton4.Enabled = true;
            }
            if (user.acce.Contains("-9-"))
            {
                panel1.Visible = true;
                simpleButton2.Enabled = true;
            }
            if (user.acce.Contains("-10-"))
            {
                panel1.Visible = true;
                simpleButton3.Enabled = true;
            }
        }

        private void rowsresize()
        {
            for (int i = 0; i < rows; i++)
            {
                dataGridView1.Rows[i].Height = (panel4.Height - dataGridView1.ColumnHeadersHeight - 4) / rows;
            }
        }

        private void to_array(SqlDataReader dr)
        {
            sessions = new List<int>();
            while (dr.Read())
            {
                sessions.Add (dr.GetInt32(0));
            }
        }

        private void get_data()
        {
            DAL.Open();
            para = new SqlParameter[2];
            para[0] = new SqlParameter("anne", comboBoxEdit1.Text);
            para[1] = new SqlParameter("mois", comboBoxEdit2.SelectedIndex +1);
            to_array(DAL.Read("SES_affichage", para));
            DAL.Close();
        }
        private void datachanged(List<int> sessions)
        {
            dataGridView1.Rows.Clear();
            startofthemonth = new DateTime(Convert.ToInt32(comboBoxEdit1.Text), Convert.ToInt32(comboBoxEdit2.SelectedIndex + 1), 1);
            days = DateTime.DaysInMonth(Convert.ToInt32(comboBoxEdit1.Text), Convert.ToInt32(comboBoxEdit2.SelectedIndex + 1));
            dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d"));
            dataGridView1.Rows.Add();
            day = 1;
            rows = 1;
            for (int i = dayoftheweek; i < 7; i++)
            {
                if (sessions.Contains(day))
                {
                    dataGridView1.Rows[0].Cells[i].Style.BackColor = Color.LimeGreen;
                }
                else
                {
                    dataGridView1.Rows[0].Cells[i].Style.BackColor = Color.White;
                }
                dataGridView1.Rows[0].Cells[i].Value = day++;

            }
            for (int i = 1; days >= day; i++)
            {
                dataGridView1.Rows.Add();
                rows++;
                for (int j = 0; days >= day && j < 7; j++)
                {
                    if (sessions.Contains(day))
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.LimeGreen;
                    }
                    else
                    {
                        dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.White;
                    }
                    dataGridView1.Rows[i].Cells[j].Value = day++;
                }
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_data();
            datachanged(sessions);
            rowsresize();
        }

        private void page2_Resize(object sender, EventArgs e)
        {
            rowsresize();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Style.BackColor == Color.LimeGreen)
            {
                new PL.PAGES.PAGE2.absence(new DateTime(Convert.ToInt32(comboBoxEdit1.Text), Convert.ToInt32(comboBoxEdit2.SelectedIndex + 1), Convert.ToInt32(dataGridView1.CurrentCell.Value)),Previous).Show();
                Previous.Visible = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            new PL.PAGES.PAGE2.absence_record(Previous,comboBoxEdit2.SelectedIndex, comboBoxEdit1.SelectedIndex).Show();
            Previous.Hide();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Style.BackColor == Color.White)
            {
                date = new DateTime(Convert.ToInt32(comboBoxEdit1.Text), Convert.ToInt32(comboBoxEdit2.SelectedIndex + 1), Convert.ToInt32(dataGridView1.CurrentCell.Value));
                DAL.Open();
                para = new SqlParameter[1];
                para[0] = new SqlParameter("date",date);
                DAL.procedure("SES_ajout", para);
                DAL.Close();
                dataGridView1.CurrentCell.Style.BackColor = Color.LimeGreen;
            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.Style.BackColor == Color.LimeGreen)
            {
                date = new DateTime(Convert.ToInt32(comboBoxEdit1.Text), Convert.ToInt32(comboBoxEdit2.SelectedIndex + 1), Convert.ToInt32(dataGridView1.CurrentCell.Value));
                DAL.Open();
                para = new SqlParameter[1];
                para[0] = new SqlParameter("date", date);
                DAL.procedure("SES_suppression", para);
                DAL.Close();
                dataGridView1.CurrentCell.Style.BackColor = Color.White;
            }
        }
    }
}

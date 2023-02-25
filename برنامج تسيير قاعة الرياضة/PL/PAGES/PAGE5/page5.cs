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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE5
{
    public partial class page5 : DevExpress.XtraEditors.XtraUserControl
    {
        Form Previous;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;
        string[] data;
        double profit;
        double charges;

        public page5(Form Previous)
        {
            InitializeComponent();

            this.Previous = Previous;

            comboBoxEdit1.Text = DateTime.Now.Year.ToString();
            comboBoxEdit2.SelectedIndex = DateTime.Now.Month - 1;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 10, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);
            dataGridView2.DefaultCellStyle.Font = new Font("Dubai", 10, FontStyle.Bold);

            dataGridView2.Columns[1].Width = dataGridView2.Width / 4 + 20;
            dataGridView2.Columns[2].Width = dataGridView2.Width / 4 - 20;
            dataGridView1.Columns[1].Width = dataGridView1.Width / 4 + 20;
            dataGridView1.Columns[2].Width = dataGridView1.Width / 4 - 20;

            if (user.acce.Contains("-16-"))
            {
                simpleButton1.Visible = true;
                simpleButton2.Visible = true;
                simpleButton3.Visible = true;
                simpleButton4.Visible = true;
                simpleButton5.Visible = true;
                simpleButton6.Visible = true;
            }
            else
            {
                simpleButton7.Size = simpleButton9.Size;
                simpleButton8.Size = simpleButton10.Size;
                simpleButton7.Location = simpleButton9.Location;
                simpleButton8.Location = simpleButton10.Location;
            }
            if (user.acce.Contains("-17-"))
            {
                save.Visible = true;
            }
        }

        private void page5_Resize(object sender, EventArgs e)
        {
            panel5.Width = panel1.Width / 2;
            panel11.Width = panel8.Width / 3;
            panel12.Width = panel8.Width / 3;
            panel13.Width = panel8.Width / 3;
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();

            simpleButton3.Enabled = false;
            simpleButton5.Enabled = false;
            simpleButton4.Enabled = false;
            simpleButton6.Enabled = false;
            simpleButton7.Enabled = false;
            simpleButton8.Enabled = false;

            DAL.Open();
            para = new SqlParameter[2];
            data = new string[4];
            para[0] = new SqlParameter("anne", comboBoxEdit1.Text);
            para[1] = new SqlParameter("mois", comboBoxEdit2.SelectedIndex + 1);
            dr = DAL.Read("CHA_affichage",para);
            while (dr.Read())
            {
                data[0] = dr.GetInt32(0).ToString();
                data[1] = dr.GetString(1);
                data[2] = dr.GetDouble(2).ToString();
                data[3] = dr.GetString(3);
                dataGridView1.Rows.Add(data);
            }
            dr.Close();
            DAL.Close();
            dataGridView1.Columns[0].Visible = false;
            //---------------------------
            DAL.Open();
            para = new SqlParameter[2];
            para[0] = new SqlParameter("anne", comboBoxEdit1.Text);
            para[1] = new SqlParameter("mois", comboBoxEdit2.SelectedIndex + 1);
            dr = DAL.Read("PRO_affichage", para);
            while (dr.Read())
            {
                data[0] = dr.GetInt32(0).ToString();
                data[1] = dr.GetString(1);
                data[2] = dr.GetDouble(2).ToString();
                data[3] = dr.GetString(3);
                dataGridView2.Rows.Add(data);
            }
            dr.Close();
            DAL.Close();
            dataGridView2.Columns[0].Visible = false;
            //---------------------------
            DAL.Open();
            para = new SqlParameter[2];
            para[0] = new SqlParameter("anne", comboBoxEdit1.Text);
            para[1] = new SqlParameter("mois", comboBoxEdit2.SelectedIndex + 1);
            dr = DAL.Read("PAY_somme", para);
            while (dr.Read())
            {
                data[0] = dr.GetInt32(0).ToString();
                data[1] = dr.GetString(1);
                data[2] = dr.GetDouble(2).ToString();
                data[3] = dr.GetString(3);
                dataGridView2.Rows.Add(data);
            }
            dr.Close();
            DAL.Close();
            Conclusion();

        }

        private void Conclusion()
        {
            profit = 0;
            charges = 0;
            if (dataGridView1.RowCount != 0)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    charges += Convert.ToDouble(row.Cells[2].Value);
                }
            }
            if (dataGridView2.RowCount != 0)
            {
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    profit += Convert.ToDouble(row.Cells[2].Value);
                }
            }
            label4.Text = "المصاريف : " + charges.ToString();
            label5.Text = "المداخيل : " + profit.ToString();
            label6.Text = "الباقي : " + (profit - charges).ToString();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count == 0 || dataGridView2.CurrentRow.Cells[0].Value.ToString() == "-1" )
            {
                simpleButton3.Enabled = false;
                simpleButton5.Enabled = false;
            }
            else
            {
                simpleButton3.Enabled = true;
                simpleButton5.Enabled = true;
            }
            if (dataGridView2.Rows.Count == 0)
            {
                simpleButton7.Enabled = false;
            }
            else
            {
                simpleButton7.Enabled = true;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            new PAGES.PAGE5.add_edit_details(Previous,-1,0,comboBoxEdit2.SelectedIndex, comboBoxEdit1.SelectedIndex).Show();
            Previous.Hide();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            new PAGES.PAGE5.add_edit_details(Previous, -1, 1, comboBoxEdit2.SelectedIndex, comboBoxEdit1.SelectedIndex).Show();
            Previous.Hide();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            new PAGES.PAGE5.add_edit_details(Previous, Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value), 0, comboBoxEdit2.SelectedIndex, comboBoxEdit1.SelectedIndex).Show();
            Previous.Hide();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                simpleButton4.Enabled = false;
                simpleButton6.Enabled = false;
                simpleButton8.Enabled = false;
            }
            else
            {
                simpleButton4.Enabled = true;
                simpleButton6.Enabled = true;
                simpleButton8.Enabled = true;
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            new PAGES.PAGE5.add_edit_details(Previous, Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), 1, comboBoxEdit2.SelectedIndex, comboBoxEdit1.SelectedIndex).Show();
            Previous.Hide();
        }

        private void page5_VisibleChanged(object sender, EventArgs e)
        {
            comboBoxEdit1_SelectedIndexChanged(sender, e);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Previous.Hide();
            new PAGES.MESSAGES.confirmation(Previous, Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value), "PRO_suppression").Show();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            Previous.Hide();
            new PAGES.MESSAGES.confirmation(Previous, Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), "CHA_suppression").Show();
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

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow.Cells[0].Value.ToString() == "-1")
            {
                new PAGES.PAGE3.register_payement(Previous, Convert.ToInt32(comboBoxEdit1.Text), comboBoxEdit2.SelectedIndex + 1,Convert.ToDouble(dataGridView2.CurrentRow.Cells[2].Value)).Show();
                Previous.Hide();
            }
            else
            {
                new PAGES.PAGE5.add_edit_details(Previous, Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value), 2, comboBoxEdit2.SelectedIndex, comboBoxEdit1.SelectedIndex).Show();
                Previous.Hide();
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            new PAGES.PAGE5.add_edit_details(Previous, Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), 3, comboBoxEdit2.SelectedIndex, comboBoxEdit1.SelectedIndex).Show();
            Previous.Hide();
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                new ReportPrintTool(new reports.Rapport_financier(Convert.ToInt32(comboBoxEdit1.Text), (comboBoxEdit2.SelectedIndex + 1))).ShowPreview();
            }
            catch
            {

            }
        }
    }
}

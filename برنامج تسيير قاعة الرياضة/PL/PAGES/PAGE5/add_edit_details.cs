using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE5
{
    public partial class add_edit_details : DevExpress.XtraEditors.XtraForm
    {
        Form previous;
        int index , condition , days ;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;
        string[] data = new string[3];
        double montant = 0;
        bool is_day_in_month;

        public add_edit_details(Form previous, int index, int condition , int month , int year)
        {
            InitializeComponent();

            this.previous = previous;
            this.index = index;
            this.condition = condition;

            comboBoxEdit1.SelectedIndex = year;
            comboBoxEdit2.SelectedIndex = month;

            if (condition == 0 && index != -1)
            {
                this.Text = "تعديل مدخول";
                label4.Text = "تعديل مدخول";
                label6.Text = "المصدر";
                dataGridView1.Columns[0].HeaderText = "المصدر";
                simpleButton2.Visible = true;
                get_data();
                get_details();
            }
            else if (condition == 0 && index == -1)
            {
                this.Text = "إضافة مدخول";
                label4.Text = "إضافة مدخول";
                label6.Text = "المصدر";
                dataGridView1.Columns[0].HeaderText = "المصدر";
                simpleButton5.Visible = true;
                label3.Text = montant.ToString();
            }
            else if (condition == 1 && index == -1)
            {
                this.Text = "إضافة مصروف";
                label4.Text = "إضافة مصروف";
                label6.Text = "الوجهة";
                dataGridView1.Columns[0].HeaderText = "الوجهة";
                simpleButton5.Visible = true;
                label3.Text = montant.ToString();
            }
            else if (condition == 1 && index != -1)
            {
                this.Text = "تعديل مصروف";
                label4.Text = "تعديل مصروف";
                label6.Text = "الوجهة";
                dataGridView1.Columns[0].HeaderText = "الوجهة";
                simpleButton2.Visible = true;
                get_data();
                get_details();
            }
            else if (condition == 2)
            {
                this.Text = "تفاصيل مدخول";
                label4.Text = "تفاصيل مدخول";
                label6.Text = "المصدر";
                dataGridView1.Columns[0].HeaderText = "المصدر";
                panel2.Visible = false;
                panel1.Visible = false;
                get_data();
                get_details();

                if (user.acce.Contains("-17-"))
                {
                    save.Visible = true;
                }
            }
            else if (condition == 3)
            {
                this.Text = "تفاصيل مصروف";
                label4.Text = "تفاصيل مصروف";
                label6.Text = "الوجهة";
                dataGridView1.Columns[0].HeaderText = "الوجهة";
                panel2.Visible = false;
                panel1.Visible = false;
                get_data();
                get_details();

                save_mess.Text = "تحميل تفاصيل المصروف";
                if (user.acce.Contains("-17-"))
                {
                    save.Visible = true;
                }
            }

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 10, FontStyle.Bold);

            dataGridView1.Columns[0].Width = dataGridView1.Width / 2;

        }

        public bool is_empty(string text, Panel panel)
        {
            if (text.Replace(" ", "") == "")
            {
                panel.BackColor = Color.Red;
                return true;
            }
            else
            {
                panel.BackColor = Color.White;
                return false;
            }
        }

        public bool is_in_month()
        {
            is_day_in_month = true;
            days = DateTime.DaysInMonth(Convert.ToInt32(comboBoxEdit1.Text), Convert.ToInt32(comboBoxEdit2.SelectedIndex + 1));
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells[2].Value) > days)
                {
                    row.Cells[2].Style.BackColor = Color.Pink;
                    row.Cells[2].Style.SelectionBackColor = Color.DeepPink;
                    is_day_in_month = false;
                }
                else
                {
                    row.Cells[2].Style.BackColor = Color.White;
                    row.Cells[2].Style.SelectionBackColor = Color.FromArgb(0, 120, 215);
                }
            }
            return is_day_in_month;
        }

        public bool is_zero()
        {
            if (montant == 0)
            {
                panel6.BackColor = Color.Red;
                return true;
            }
            else
            {
                panel6.BackColor = Color.White;
                return false;
            }
        }

        public void soom()
        {
            montant = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                montant = montant + Convert.ToDouble(row.Cells[1].Value);
            }
            label3.Text = montant.ToString();
        }

        public void get_data()
        {
            para = new SqlParameter[1];
            para[0] = new SqlParameter("id", index);
            DAL.Open();
            if (condition == 0 || condition == 2)
            {
                dr = DAL.Read("PRO_recuperation", para);
            }
            else
            {
                dr = DAL.Read("CHA_recuperation", para);
            }

            dr.Read();

            if (condition == 0 || condition == 1)
            {
                comboBoxEdit2.Text = dr.GetString(0);
                comboBoxEdit1.Text = dr.GetInt32(1).ToString();
                textBox1.Text = dr.GetString(2);
            }
            else
            {
                label10.Show();
                label11.Show();
                label9.Show();

                comboBoxEdit2.Hide();
                comboBoxEdit1.Hide();
                textBox1.Hide();

                label10.Text = dr.GetString(0);
                label11.Text = dr.GetInt32(1).ToString();
                label9.Text = dr.GetString(2);
            }
            label3.Text = dr.GetDouble(3).ToString();
            dr.Close();
            DAL.Close();
        }

        public void get_details()
        {
            para = new SqlParameter[1];
            para[0] = new SqlParameter("id", index);
            DAL.Open();
            if (condition == 0 || condition == 2)
            {
                dr = DAL.Read("PRO_DET_affichage", para);
            }
            else
            {
                dr = DAL.Read("CHA_DET_affichage", para);
            }

            while (dr.Read())
            {
                data[0] = dr.GetString(3);
                data[1] = dr.GetDouble(2).ToString();
                data[2] = dr.GetInt32(1).ToString();
                dataGridView1.Rows.Add(data);
            }

            dr.Close();
            DAL.Close();
            soom();
        }

        private void add_edit_details_FormClosing(object sender, FormClosingEventArgs e)
        {
            previous.Show();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            textEdit1.Text = "";
            textBox2.Text = "";
            comboBoxEdit3.SelectedIndex = 0;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (is_empty(textEdit1.Text, panel8) || is_empty(textBox2.Text, panel7))
            {
                is_empty(textEdit1.Text, panel8);
                is_empty(textBox2.Text, panel7);
            }
            else
            {
                data[0] = textBox2.Text;
                data[1] = textEdit1.Text;
                data[2] = comboBoxEdit3.Text;
                dataGridView1.Rows.Add(data);
                dataGridView1_SelectionChanged(sender, e);
                soom();
                is_in_month();
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                soom();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textEdit1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                comboBoxEdit3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            else
            {
                textEdit1.Text = "";
                textBox2.Text = "";
                comboBoxEdit3.SelectedIndex = 0;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textEdit1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                comboBoxEdit3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            else
            {
                textEdit1.Text = "";
                textBox2.Text = "";
                comboBoxEdit3.SelectedIndex = 0;
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                if (is_empty(textEdit1.Text, panel8) || is_empty(textBox2.Text, panel7))
                {
                    is_empty(textEdit1.Text, panel8);
                    is_empty(textBox2.Text, panel7);
                }
                else
                {
                    dataGridView1.CurrentRow.Cells[0].Value = textBox2.Text;
                    dataGridView1.CurrentRow.Cells[1].Value = textEdit1.Text;
                    dataGridView1.CurrentRow.Cells[2].Value = comboBoxEdit3.Text;
                    dataGridView1_SelectionChanged(sender, e);
                    is_in_month();
                    soom();
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (is_empty(textBox1.Text, panel5) || is_empty(label3.Text, panel6) || is_zero() || !is_in_month())
            {
                is_empty(textBox1.Text, panel5);
                is_empty(label3.Text, panel6);
                is_zero();
                is_in_month();
            }
            else
            {
                para = new SqlParameter[4];
                para[0] = new SqlParameter("id", index);
                para[1] = new SqlParameter("date", new DateTime(Convert.ToInt32(comboBoxEdit1.Text),comboBoxEdit2.SelectedIndex+1,1));
                para[2] = new SqlParameter("montant", montant.ToString());
                DAL.Open();
                if (condition == 0)
                {
                    para[3] = new SqlParameter("source", textBox1.Text);
                    DAL.procedure("PRO_modif", para);
                }
                else
                {
                    para[3] = new SqlParameter("destination", textBox1.Text);
                    DAL.procedure("CHA_modif", para);
                }
                DAL.Close();

                para = new SqlParameter[1];
                para[0] = new SqlParameter("id", index);
                DAL.Open();
                if (condition == 0)
                {
                    DAL.procedure("PRO_DET_supression", para);
                }
                else
                {
                    DAL.procedure("CHA_DET_supression", para);
                }
                DAL.Close();

                
                if (condition == 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        para = new SqlParameter[4];
                        para[0] = new SqlParameter("id", index);
                        DAL.Open();
                        para[1] = new SqlParameter("source", row.Cells[0].Value);
                        para[2] = new SqlParameter("montant", row.Cells[1].Value);
                        para[3] = new SqlParameter("jour", row.Cells[2].Value);
                        DAL.procedure("PRO_DET_ajout", para);
                        DAL.Close();
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        para = new SqlParameter[4];
                        para[0] = new SqlParameter("id", index);
                        DAL.Open();
                        para[1] = new SqlParameter("destination", row.Cells[0].Value);
                        para[2] = new SqlParameter("montant", row.Cells[1].Value);
                        para[3] = new SqlParameter("jour", row.Cells[2].Value);
                        DAL.procedure("CHA_DET_ajout", para);
                        DAL.Close();
                    }
                }

                this.Close();
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (is_empty(textBox1.Text, panel5) || is_empty(label3.Text, panel6) || is_zero() || !is_in_month())
            {
                is_empty(textBox1.Text, panel5);
                is_empty(label3.Text, panel6);
                is_zero();
                is_in_month();
            }
            else
            {
                para = new SqlParameter[3];
                para[0] = new SqlParameter("date", new DateTime(Convert.ToInt32(comboBoxEdit1.Text), comboBoxEdit2.SelectedIndex + 1, 1));
                para[1] = new SqlParameter("montant", montant.ToString());
                DAL.Open();
                if (condition == 0)
                {
                    para[2] = new SqlParameter("source", textBox1.Text);
                    dr=DAL.Read("PRO_ajout", para);
                }
                else
                {
                    para[2] = new SqlParameter("destination", textBox1.Text);
                    dr=DAL.Read("CHA_ajout", para);
                }
                dr.Read();
                index = dr.GetInt32(0);
                dr.Close();
                DAL.Close();

                

                if (condition == 0)
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        para = new SqlParameter[4];
                        para[0] = new SqlParameter("id", index);
                        DAL.Open();
                        para[1] = new SqlParameter("source", row.Cells[0].Value);
                        para[2] = new SqlParameter("montant", row.Cells[1].Value);
                        para[3] = new SqlParameter("jour", row.Cells[2].Value);
                        DAL.procedure("PRO_DET_ajout", para);
                        DAL.Close();
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        para = new SqlParameter[4];
                        para[0] = new SqlParameter("id", index);
                        DAL.Open();
                        para[1] = new SqlParameter("destination", row.Cells[0].Value);
                        para[2] = new SqlParameter("montant", row.Cells[1].Value);
                        para[3] = new SqlParameter("jour", row.Cells[2].Value);
                        DAL.procedure("CHA_DET_ajout", para);
                        DAL.Close();
                    }
                }

                
                this.Close();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxEdit2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxEdit3.Properties.Items.Clear();
            days = DateTime.DaysInMonth(Convert.ToInt32(comboBoxEdit1.Text), Convert.ToInt32(comboBoxEdit2.SelectedIndex + 1));
            for (int i = 1; i<=days; i++)
            {
                comboBoxEdit3.Properties.Items.Add(i);
            }
            comboBoxEdit3.SelectedIndex = 0;
            is_in_month();
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                new ReportPrintTool(new reports.dep_rev_det(index, condition)).ShowPreview();
            }
            catch
            {

            }
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
    }
}
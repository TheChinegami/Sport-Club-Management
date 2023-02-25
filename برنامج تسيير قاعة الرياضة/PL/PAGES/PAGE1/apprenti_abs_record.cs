using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
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


namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE1
{
    public partial class apprenti_abs_record : DevExpress.XtraEditors.XtraForm
    {
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        Form previous;
        XlsxExportOptions excel = new XlsxExportOptions(new TextExportMode(), true, true, true);
        int id , condition;
        string name;

        public apprenti_abs_record(Form previous,int id,string name,int condition)
    {
            InitializeComponent();

            if (user.acce.Contains("-17-"))
            {
                save.Visible = true;
            }

            this.previous = previous;
            this.id = id;
            this.condition = condition;
            this.name = name;

            comboBoxEdit1.Text = DateTime.Now.Year.ToString();

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);
            if (condition == 0)
            {
                label1.Text = "سجل غياب : " + name;
                this.Text = "سجل غياب : " + name;
                save_mess.Text = "تحميل سجل الغياب";
                pictureBox1.Visible = true;
            }
            if (condition == 1)
            {
                label1.Text = "سجل إمتحانات : " + name;
                this.Text = "سجل إمتحانات : " + name;
                save_mess.Text = "تحميل سجل الإمتحانات";
                panel2.Visible = false;
            }
            get_data();
        }

        public void get_data()
        {
            if (condition == 0)
            {
                DAL.Open();
                para = new SqlParameter[2];
                para[0] = new SqlParameter("anne", comboBoxEdit1.Text);
                para[1] = new SqlParameter("id", id);
                dataGridView1.DataSource = DAL.Read_data("AP_PRE_register", para);
                DAL.Close();

                dataGridView1.Columns[0].DefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value.ToString().Contains("g"))
                        {
                            cell.Style.BackColor = Color.LightGray;
                            cell.Value = "";
                        }
                        if (cell.Value.ToString().Contains("r"))
                        {
                            cell.Style.BackColor = Color.DimGray;
                            cell.Value = "";
                        }
                        if (cell.Value.ToString().Contains("f"))
                        {
                            cell.Style.BackColor = Color.LightSlateGray;
                            cell.Value = "";
                        }
                    }
                }

                for (int i = 0; i <= 12; i++)
                {
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            if (condition == 1)
            {
                DAL.Open();
                para = new SqlParameter[1];
                para[0] = new SqlParameter("id", id);
                dataGridView1.DataSource = DAL.Read_data("AP_EX_register", para);
                DAL.Close();
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_data();
        }

        private void absence_record_FormClosed(object sender, FormClosedEventArgs e)
        {
            previous.Show();
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

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            panel4.Visible = true;
            pictureBox1.Size = new Size(pictureBox1.Size.Width + 4, pictureBox1.Size.Height + 4);
            pictureBox1.Location = new Point(pictureBox1.Location.X - 2, pictureBox1.Location.Y - 2);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            panel4.Visible = false;
            pictureBox1.Size = new Size(pictureBox1.Size.Width - 4, pictureBox1.Size.Height - 4);
            pictureBox1.Location = new Point(pictureBox1.Location.X + 2, pictureBox1.Location.Y + 2);
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (condition == 0)
            {
                try
                {
                    new ReportPrintTool(new reports.AP_PRE_register(id, Convert.ToInt32(comboBoxEdit1.Text), name)).ShowPreview();
                }
                catch
                {

                }
            }
            if (condition == 1)
            {
                try
                {
                    new ReportPrintTool(new reports.AP_EX_register(id, name)).ShowPreview();
                }
                catch
                {

                }
            }
        }
    }
}
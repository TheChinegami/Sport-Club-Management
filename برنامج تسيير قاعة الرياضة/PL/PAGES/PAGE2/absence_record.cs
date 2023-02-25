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
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE2
{
    public partial class absence_record : DevExpress.XtraEditors.XtraForm
    {
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        Form previous;
        XlsxExportOptions excel = new XlsxExportOptions(new TextExportMode(), true, true, true);

        public absence_record(Form previous, int month, int year)
        {
            InitializeComponent();

            if (user.acce.Contains("-17-"))
            {
                save.Visible = true;
            }

            this.previous = previous;

            comboBoxEdit1.SelectedIndex = year;
            comboBoxEdit2.SelectedIndex = month;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);
        }

        private void get_data()
        {
            dataGridView1.Columns.Clear();
            DAL.Open();
            para = new SqlParameter[2];
            para[0] = new SqlParameter("anne", comboBoxEdit1.Text);
            para[1] = new SqlParameter("mois", comboBoxEdit2.SelectedIndex + 1);
            dataGridView1.DataSource = DAL.Read_data("PRE_register", para);
            DAL.Close();
            
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].DefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView1.Columns[1].Width = dataGridView1.Width / 4;
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

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                new ReportPrintTool(new reports.PRE_register(Convert.ToInt32(comboBoxEdit1.Text), comboBoxEdit2.SelectedIndex + 1)).ShowPreview();
            }
            catch
            {

            }
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
    }
}
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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE3
{
    public partial class page3 : DevExpress.XtraEditors.XtraUserControl
    {
        Form Previous;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        int anne;
        string montant , mois;

        public page3(Form Previous)
        {
            InitializeComponent();

            this.Previous = Previous;

            comboBoxEdit1.Text = DateTime.Now.Year.ToString();

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 10, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 8, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (user.acce.Contains("-17-"))
            {
                save.Visible = true;
                save1.Visible = true;
            }
        }

        private void page3_VisibleChanged(object sender, EventArgs e)
        {
            get_data();
        }

        public void get_data()
        {
            DAL.Open();
            para = new SqlParameter[1];
            para[0] = new SqlParameter("anne", comboBoxEdit1.Text);
            dataGridView1.DataSource = DAL.Read_data("PAY_calendrier",para);
            DAL.Close();
            resize();
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value.ToString().Contains("g") && cell.ColumnIndex > 1)
                    {
                        cell.Value = "";
                        cell.Style.BackColor = Color.Gray;
                    }
                    if (cell.Value.ToString().Contains("r") && cell.ColumnIndex > 1)
                    {
                        cell.Value = cell.Value.ToString().Replace("r","");
                        cell.Style.BackColor = Color.Pink;
                    }
                }
            }
        }
        private void resize()
        {
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].DefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);
            dataGridView1.Columns[1].Width = dataGridView1.Width / 4;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 2; i <= 14; i++)
            {
                dataGridView1.Columns[i].Width = (dataGridView1.Width - dataGridView1.Columns[1].Width) / 13;
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_data();
        }

        private void page3_Resize(object sender, EventArgs e)
        {
            resize();
        }

        private void save_MouseEnter(object sender, EventArgs e)
        {
            save_mess.Visible = true;
            save.Location = new Point(save.Location.X - 2, save.Location.Y - 2);
            save.Size = new Size(save.Size.Width + 4, save.Size.Height + 4);
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.RowCount > 0 && dataGridView1.CurrentCell.ColumnIndex != 1 && dataGridView1.CurrentCell.Style.BackColor != Color.Gray && user.acce.Contains("-12-"))
            {
                montant = dataGridView1.CurrentCell.Value.ToString().Replace('.', ',');
                mois = (dataGridView1.CurrentCell.ColumnIndex - 2).ToString();
                anne = Convert.ToInt32(comboBoxEdit1.Text);

                new PAGES.PAGE3.add_edit_payment(Previous, Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value), montant, mois, anne).Show();
                Previous.Hide();
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                new ReportPrintTool(new reports.PAY_calendrier(Convert.ToInt32(comboBoxEdit1.Text))).ShowPreview();
            }
            catch
            {

            }
        }

        private void save1_MouseEnter(object sender, EventArgs e)
        {
            save_mess1.Visible = true;
            save1.Location = new Point(save1.Location.X - 2, save1.Location.Y - 2);
            save1.Size = new Size(save1.Size.Width + 4, save1.Size.Height + 4);
        }

        private void save1_MouseLeave(object sender, EventArgs e)
        {
            save_mess1.Visible = false;
            save1.Location = new Point(save1.Location.X + 2, save1.Location.Y + 2);
            save1.Size = new Size(save1.Size.Width - 4, save1.Size.Height - 4);
        }

        private void save1_Click(object sender, EventArgs e)
        {
            new PL.PAGES.PAGE3.Select_a_month(Convert.ToInt32(comboBoxEdit1.Text), Previous).Show();
            Previous.Hide();
        }

        private void save_MouseLeave(object sender, EventArgs e)
        {
            save_mess.Visible = false;
            save.Location = new Point(save.Location.X + 2, save.Location.Y + 2);
            save.Size = new Size(save.Size.Width - 4, save.Size.Height - 4);
        }
    }
}

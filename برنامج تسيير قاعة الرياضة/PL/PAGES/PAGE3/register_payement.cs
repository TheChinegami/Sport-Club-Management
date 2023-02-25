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
    public partial class register_payement : DevExpress.XtraEditors.XtraForm
    {
        Form previous;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        string mois_string;
        int anne;
        int mois;

        public register_payement(Form previous,int anne,int mois,double montant)
        {
            InitializeComponent();

            if (user.acce.Contains("-17-"))
            {
                save.Visible = true;
            }

            this.previous = previous;
            this.anne = anne;
            this.mois = mois;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Dubai", 14, FontStyle.Bold);
            dataGridView1.DefaultCellStyle.Font = new Font("Dubai", 12, FontStyle.Bold);

            DAL.Open();
            para = new SqlParameter[2];
            para[0] = new SqlParameter("anne", anne);
            para[1] = new SqlParameter("mois", mois);
            dataGridView1.DataSource = DAL.Read_data("PAY_register", para);
            DAL.Close();
            dataGridView1.Columns[0].Visible = false;
            label10.Text = get_month(mois);
            label11.Text = anne.ToString();
            label3.Text = montant.ToString();
        }

        private void register_payement_FormClosing(object sender, FormClosingEventArgs e)
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

        private string get_month(int mois_int)
        {
            switch (mois_int)
            {
                case 1: mois_string = "يناير";
                    break;
                case 2: mois_string = "فبراير";
                    break;
                case 3: mois_string = "مارس";
                    break;
                case 4: mois_string = "أبريل";
                    break;
                case 5: mois_string = "ماي";
                    break;
                case 6: mois_string = "يونيو";
                    break;
                case 7: mois_string = "يوليوز";
                    break;
                case 8: mois_string = "غشت";
                    break;
                case 9: mois_string = "شتنبر";
                    break;
                case 10: mois_string = "أكتوبر";
                    break;
                case 11: mois_string = "مارس";
                    break;
                case 12: mois_string = "أبريل";
                    break;
            }
            return mois_string;
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                new ReportPrintTool(new reports.PAY_register(anne, mois, 0)).ShowPreview();
            }
            catch
            {

            }
        }
    }
}
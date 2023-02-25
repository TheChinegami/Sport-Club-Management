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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE3
{
    public partial class add_edit_payment : DevExpress.XtraEditors.XtraForm
    {
        Form Previous;
        int index ;
        string mois;
        int anne;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;
        double montant=0;
        
        public add_edit_payment(Form Previous,int index,string montant,string mois,int anne)
        {
            InitializeComponent();
            this.Previous = Previous;
            this.index = index;
            this.mois = mois;
            this.anne = anne;
            textBox1.Text = montant;

            DAL.Open();
            para = new SqlParameter[3];
            para[0] = new SqlParameter("id", index);
            para[1] = new SqlParameter("anne", anne);
            para[2] = new SqlParameter("mois", mois);
            dr = DAL.Read("PAY_date", para);
            
            if (dr.Read())
            {
                dateEdit1.DateTime = dr.GetDateTime(0);
            }
            else
            {
                dateEdit1.DateTime = DateTime.Now;
            }
            DAL.Close();
            //this.montant = 0;
            if (textBox1.Text != "")
            {
                this.montant = Convert.ToDouble(textBox1.Text);
            }
        }

        private void add_edit_payment_FormClosing(object sender, FormClosingEventArgs e)
        {
            Previous.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            montant = 0;
            if (textBox1.Text != "")
            {
                montant = Convert.ToDouble(textBox1.Text);
            }
            if (montant!=0 && dateEdit1.Text == "")
            {
                label2.Text = "⚠️ أدخل تاريخ الأداء";
                simpleButton5.Enabled = false;
            }
            else
            {
                label2.Text = "";
                simpleButton5.Enabled = true;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            DAL.Open();
            para = new SqlParameter[5];
            para[0] = new SqlParameter("id", index);
            para[1] = new SqlParameter("mois", mois);
            para[2] = new SqlParameter("anne", anne);
            if (montant == 0)
            {
                para[3] = new SqlParameter("montant", DBNull.Value);
                para[4] = new SqlParameter("date", DBNull.Value);
            }
            else
            {
                para[3] = new SqlParameter("montant", montant);
                para[4] = new SqlParameter("date", dateEdit1.DateTime);
            }
            DAL.procedure("PAY_operteur",para);
            DAL.Close();
            this.Close();
        }
    }
}
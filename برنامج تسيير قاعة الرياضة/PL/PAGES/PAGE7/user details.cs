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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE7
{
    public partial class user_details : DevExpress.XtraEditors.XtraForm
    {

        int index;
        Form previous;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        CheckEdit[] check;
        string acce = "" , rang = "مستخدم";
        SqlDataReader dr;

        public user_details(int index , Form previous)
        {
            InitializeComponent();

            check = new CheckEdit[] {check1 , check2 , check3 , check4 , check5 , check6 , check7 , check8
                 , check9 , check10 , check11 , check12 , check13 , check14  , check15 , check16 , check17  };

            this.previous = previous;
            this.index = index;

            if (user.rang == "مبرمج")
            {
                checkEdit1.Visible = true;
                checkEdit2.Visible = true;
            }

            para = new SqlParameter[1];
            para[0] = new SqlParameter("id", index);
            DAL.Open();
            dr = DAL.Read("UTIL_affichage det", para);
            dr.Read();
            label13.Text = dr.GetString(1);
            label12.Text = dr.GetString(2);
            label10.Text = dr.GetString(3);
            label6.Text = dr.GetString(4);
            label4.Text = dr.GetString(5);
            label11.Text = dr.GetString(6);
            rang = dr.GetString(8);
            acce = dr.GetString(9);
            set_acce();
            DAL.Close();

            if (rang == "مدير")
            {
                checkEdit1.Checked = true;
            }
            else
            {
                checkEdit2.Checked = true;
            }
        }

        public void set_acce()
        {
            for (int i = 0; i < 17; i++)
            {
                if (acce.Contains("-" + (i + 1).ToString() + "-"))
                {
                    check[i].Checked = true;
                }
                else
                {
                    check[i].Checked = false;
                }
            }
        }

        private void user_details_FormClosing(object sender, FormClosingEventArgs e)
        {
            previous.Show();
        }

        private void check1_CheckedChanged(object sender, EventArgs e)
        {
            set_acce();
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (rang == "مدير")
            {
                checkEdit1.Checked = true;
                checkEdit2.Checked = false;
            }
            else
            {
                checkEdit2.Checked = true;
                checkEdit1.Checked = false;
            }
        }
    }
}
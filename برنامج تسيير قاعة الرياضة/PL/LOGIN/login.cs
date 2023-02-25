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

namespace برنامج_تسيير_قاعة_الرياضة.PL.LOGIN
{
    public partial class login : DevExpress.XtraEditors.XtraForm
    {
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;

        public login()
        {
            InitializeComponent();
            this.Opacity = .90;

            DAL.Open();
            dr = DAL.Read("UTIL_users");
            while (dr.Read())
            {
                if (dr.GetString(1) != "مبرمج")
                {
                    comboBoxEdit1.Properties.Items.Add(dr.GetString(0));
                }
                user.users.Add(dr.GetString(0));
            }
            dr.Close();
            DAL.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DAL.Open();
            para = new SqlParameter[1];
            para[0] = new SqlParameter("login",comboBoxEdit1.Text);
            dr = DAL.Read("UTIL_login",para);
            if (dr.Read())
            {
                if (BCrypt.Net.BCrypt.Verify(textBox1.Text, dr.GetString(7)))
                {
                    user.id = dr.GetInt32(0);
                    user.prenom = dr.GetString(1);
                    user.nom = dr.GetString(2);
                    user.cni = dr.GetString(3);
                    user.telephone = dr.GetString(4);
                    user.email = dr.GetString(5);
                    user.login = dr.GetString(6);
                    user.mdp = dr.GetString(7);
                    user.rang = dr.GetString(8);
                    user.acce = dr.GetString(9);
                    label4.Text = "";
                    label3.Text = "";
                    comboBoxEdit1.Text = "";
                    textBox1.Text = "";
                    new Form1(this).Show();
                    this.Hide();
                }
                else
                {
                    label4.Text = "⚠️ كلمة المرور غير صحيحة";
                }
                label3.Text = "";
            }
            else
            {
                label3.Text = "⚠️ إسم المستخدم غير صحيحة";
            }
            dr.Close ();
            DAL.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void login_VisibleChanged(object sender, EventArgs e)
        {
            user.users.Clear();
            comboBoxEdit1.Properties.Items.Clear();
            DAL.Open();
            dr = DAL.Read("UTIL_users");
            while (dr.Read())
            {
                if (dr.GetString(1) != "مبرمج")
                {
                    comboBoxEdit1.Properties.Items.Add(dr.GetString(0));
                }
                user.users.Add(dr.GetString(0));
            }
            dr.Close();
            DAL.Close();
        }
    }
}
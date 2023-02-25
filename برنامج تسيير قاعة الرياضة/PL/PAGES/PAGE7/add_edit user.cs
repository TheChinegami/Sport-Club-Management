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
    public partial class add_edit_user : DevExpress.XtraEditors.XtraForm
    {
        int index;
        Form previous;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        CheckEdit[] check ;
        string acce = "";
        SqlDataReader dr;
        string login , rang = "مستخدم";

        public add_edit_user( int index , Form previous )
        {
            InitializeComponent();

            check = new CheckEdit[] {check1 , check2 , check3 , check4 , check5 , check6 , check7 , check8
                 , check9 , check10 , check11 , check12 , check13 , check14  , check15 , check16 , check17  };

            this.index = index;
            this.previous = previous;

            if (index == -1)
            {
                add.Show();
                label1.Text = "إضافة مستخدم";
                this.Text = "إضافة مستخدم";
            }
            else
            {
                edit.Show();
                get_data();
                label1.Text = "تعديل مستخدم";
                this.Text = "تعديل مستخدم";
            }
            if (user.rang == "مبرمج")
            {
                checkEdit1.Visible = true;
                checkEdit2.Visible = true;
            }
        }

        public void get_data()
        {
            para = new SqlParameter[1];
            para[0] = new SqlParameter("id", index);
            DAL.Open();
            dr = DAL.Read("UTIL_affichage det", para);
            dr.Read();
            textBox1.Text = dr.GetString(1);
            textBox2.Text = dr.GetString(2);
            textBox6.Text = dr.GetString(3);
            textBox7.Text = dr.GetString(4);
            textBox8.Text = dr.GetString(5);
            textBox4.Text = dr.GetString(6);
            login = dr.GetString(6);
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
        public bool is_zero(string text, Panel panel)
        {
            if (text == "0")
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

        public bool login_exist(string text)
        {
            if (user.users.Contains(text) && login != text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool pass_confirmation()
        {
            if (textBox3.Text != textBox5.Text)
            {
                panel8.BackColor = Color.Red;
                return true;
            }
            else
            {
                panel8.BackColor = Color.White;
                return false;
            }
        }

        public void enable_check()
        {
            if (check1.Checked == false)
            {
                check2.Checked = false;
                check3.Checked = false;
                check4.Checked = false;
                check5.Checked = false;
                check2.Enabled = false;
                check3.Enabled = false;
                check4.Enabled = false;
                check5.Enabled = false;
            }
            else
            {
                check2.Enabled = true;
                check3.Enabled = true;
                check4.Enabled = true;
                check5.Enabled = true;
            }
            if (check5.Checked == false)
            {
                check6.Checked = false;
                check6.Enabled = false;
            }
            else
            {
                check6.Enabled = true;
            }
            if (check7.Checked == false)
            {
                check8.Checked = false;
                check9.Checked = false;
                check10.Checked = false;
                check8.Enabled = false;
                check9.Enabled = false;
                check10.Enabled = false;
            }
            else
            {
                check8.Enabled = true;
                check9.Enabled = true;
                check10.Enabled = true;
            }
            if (check11.Checked == false)
            {
                check12.Checked = false;
                check12.Enabled = false;
            }
            else
            {
                check12.Enabled = true;
            }
            if (check13.Checked == false)
            {
                check14.Checked = false;
                check14.Enabled = false;
            }
            else
            {
                check14.Enabled = true;
            }
            if (check15.Checked == false)
            {
                check16.Checked = false;
                check16.Enabled = false;
            }
            else
            {
                check16.Enabled = true;
            }
        }

        private void check1_CheckedChanged(object sender, EventArgs e)
        {
            enable_check();
        }

        private void add_edit_user_FormClosing(object sender, FormClosingEventArgs e)
        {
            previous.Show();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (login_exist(textBox4.Text) || is_empty(textBox1.Text, panel4) || is_empty(textBox2.Text, panel5) || is_empty(textBox3.Text, panel6) || is_empty(textBox4.Text, panel7) || is_empty(textBox5.Text, panel8) || is_empty(textBox6.Text, panel10) || is_zero(textBox7.Text, panel11) || is_empty(textBox7.Text, panel11) || is_empty(textBox8.Text, panel12) || pass_confirmation())
            {
                login_exist(textBox4.Text);
                is_empty(textBox1.Text, panel4);
                is_empty(textBox2.Text, panel5);
                is_empty(textBox3.Text, panel6);
                is_empty(textBox4.Text, panel7);
                is_empty(textBox5.Text, panel8);
                is_empty(textBox6.Text, panel10);
                is_empty(textBox7.Text, panel11);
                is_zero(textBox7.Text, panel11);
                is_empty(textBox8.Text, panel12);
                pass_confirmation();
            }
            else
            {
                get_acce();
                para = new SqlParameter[9];
                para[0] = new SqlParameter("prenom", textBox1.Text);
                para[1] = new SqlParameter("nom", textBox2.Text);
                para[2] = new SqlParameter("cni", textBox6.Text);
                para[3] = new SqlParameter("telephone", textBox7.Text);
                para[4] = new SqlParameter("email", textBox8.Text);
                para[5] = new SqlParameter("login", textBox4.Text);
                para[6] = new SqlParameter("mdp", BCrypt.Net.BCrypt.HashPassword(textBox3.Text));
                para[7] = new SqlParameter("rang", rang);
                para[8] = new SqlParameter("acce",acce);
                DAL.Open();
                DAL.procedure("UTIL_ajout", para);
                DAL.Close();
                user.users_change();
                this.Close();
            }
            if (login_exist(textBox4.Text))
            {
                MessageBox.Show("إسم المستخدم موجود بالفعل", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            }
        }

        public void get_acce()
        {
            acce = "";
            for (int i = 0; i<17 ; i++)
            {
                if (check[i].Checked == true)
                {
                    acce = acce + "-"+(i + 1).ToString()+"-";
                }
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            if (login_exist(textBox4.Text) || is_empty(textBox1.Text, panel4) || is_empty(textBox2.Text, panel5) || is_empty(textBox4.Text, panel7) || is_empty(textBox6.Text, panel10) || is_zero(textBox7.Text, panel11) || is_empty(textBox7.Text, panel11) || is_empty(textBox8.Text, panel12) || pass_confirmation())
            {
                login_exist(textBox4.Text);
                is_empty(textBox1.Text, panel4);
                is_empty(textBox2.Text, panel5);
                is_empty(textBox4.Text, panel7);
                is_empty(textBox6.Text, panel10);
                is_empty(textBox7.Text, panel11);
                is_zero(textBox7.Text, panel11);
                is_empty(textBox8.Text, panel12);
                pass_confirmation();
            }
            else
            {
                get_acce();
                para = new SqlParameter[10];
                para[0] = new SqlParameter("id", index);
                para[1] = new SqlParameter("prenom", textBox1.Text);
                para[2] = new SqlParameter("nom", textBox2.Text);
                para[3] = new SqlParameter("cni", textBox6.Text);
                para[4] = new SqlParameter("telephone", textBox7.Text);
                para[5] = new SqlParameter("email", textBox8.Text);
                para[6] = new SqlParameter("login", textBox4.Text);
                if (textBox3.Text == "")
                {
                    para[7] = new SqlParameter("mdp", "");
                }
                else
                {
                    para[7] = new SqlParameter("mdp", BCrypt.Net.BCrypt.HashPassword(textBox3.Text));
                }
                para[8] = new SqlParameter("rang", rang);
                para[9] = new SqlParameter("acce", acce);
                DAL.Open();
                DAL.procedure("UTIL_modif", para);
                DAL.Close();
                user.users_change();
                this.Close();
            }
            if (login_exist(textBox4.Text))
            {
                MessageBox.Show("إسم المستخدم موجود بالفعل", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked == true)
            {
                checkEdit1.Checked = false;
                foreach (CheckEdit ch in check)
                {
                    ch.Enabled = true;
                }
                rang = "مستخدم";
            }
            else
            {
                checkEdit1.Checked = true;
                rang = "مدير";
            }
        }

        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked == true)
            {
                checkEdit2.Checked = false;
                foreach(CheckEdit ch in check)
                {
                    ch.Checked = true;
                }
                foreach (CheckEdit ch in check)
                {
                    ch.Enabled = false;
                }
                rang = "مدير";
            }
            else
            {
                checkEdit2.Checked = true;
                rang = "مستخدم";
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;
            pictureBox2.Visible = true;
            textBox3.UseSystemPasswordChar = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox4.Visible = true;
            textBox3.UseSystemPasswordChar = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox3.Visible = true;
            textBox5.UseSystemPasswordChar = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            pictureBox1.Visible = true;
            textBox5.UseSystemPasswordChar = true;
        }
    }

    
}
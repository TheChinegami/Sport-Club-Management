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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE6
{
    public partial class page6 : DevExpress.XtraEditors.XtraUserControl
    {
        Form Previous;
        int height;
        public string mdp = "";
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        public page6(Form Previous)
        {
            InitializeComponent();

            this.Previous = Previous;
            get_data();
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
            if (user.users.Contains(text) && user.login != text )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void get_data()
        {
            if (user.rang != "مستخدم")
            {
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;

                panel11.Visible = true;
                panel17.Visible = true;
                panel18.Visible = true;
                panel19.Visible = true;
                panel20.Visible = true;

                textBox1.Text = user.prenom;
                textBox2.Text = user.nom;
                textBox3.Text = user.cni;
                textBox4.Text = user.telephone;
                textBox5.Text = user.email;
            }
            else
            {
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;

                label1.Text = user.prenom;
                label2.Text = user.nom;
                label3.Text = user.cni;
                label4.Text = user.telephone;
                label5.Text = user.email;
            }
            textBox6.Text = user.login;
        }

        private void page6_Resize(object sender, EventArgs e)
        {
            height = (this.Height - 40) / 9;
            panel2.Height = height * 3 + 20;
            panel4.Height = height;
            panel5.Height = height;
            panel6.Height = height;
            panel7.Height = height;
            panel8.Height = height;
            panel9.Height = height;
            panel12.Height = height;
            panel13.Height = height + 20;
            panel14.Height = height + 20;
            panel3.Width = (panel1.Width - 712) / 4;
            panel16.Width = panel3.Width;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new PAGES.PAGE6.change_password(Previous,this).Show();
            Previous.Hide();
        }

        private void simpleButton1_MouseEnter(object sender, EventArgs e)
        {
            save_mess.Visible = true;
        }

        private void simpleButton1_MouseLeave(object sender, EventArgs e)
        {
            save_mess.Visible = false;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (user.rang != "مستخدم")
            {
                if (login_exist(textBox6.Text) || is_empty(textBox1.Text,panel11) || is_empty(textBox2.Text, panel17) || is_empty(textBox3.Text, panel18) || is_zero(textBox4.Text, panel19) || is_empty(textBox4.Text, panel19) || is_empty(textBox5.Text, panel20) || is_empty(textBox6.Text, panel21))
                {
                    login_exist(textBox6.Text);
                    is_empty(textBox1.Text, panel11);
                    is_empty(textBox2.Text, panel17);
                    is_empty(textBox3.Text, panel18);
                    is_empty(textBox4.Text, panel19);
                    is_zero(textBox4.Text, panel19);
                    is_empty(textBox5.Text, panel20);
                    is_empty(textBox6.Text, panel21);
                }
                else
                {
                    user.prenom = textBox1.Text;
                    user.nom = textBox2.Text;
                    user.cni = textBox3.Text;
                    user.telephone = textBox4.Text;
                    user.email = textBox5.Text;
                    user.login = textBox6.Text;
                    if (mdp != "")
                    {
                        user.mdp = BCrypt.Net.BCrypt.HashPassword(mdp);
                    }
                    para = new SqlParameter[10];
                    para[0] = new SqlParameter("id", user.id);
                    para[1] = new SqlParameter("prenom", user.prenom);
                    para[2] = new SqlParameter("nom", user.nom);
                    para[3] = new SqlParameter("cni", user.cni);
                    para[4] = new SqlParameter("telephone", user.telephone);
                    para[5] = new SqlParameter("email", user.email);
                    para[6] = new SqlParameter("login", user.login);
                    para[7] = new SqlParameter("mdp", user.mdp);
                    para[8] = new SqlParameter("rang", user.rang);
                    para[9] = new SqlParameter("acce", user.acce);
                    DAL.Open();
                    DAL.procedure("UTIL_modif", para);
                    DAL.Close();
                    user.users_change();
                    MessageBox.Show("تم حفظ المعلومات");
                }
                
            }
            else
            {
                if (!is_empty(textBox6.Text, panel21) && !login_exist(textBox6.Text))
                {
                    user.login = textBox6.Text;
                    if (mdp != "")
                    {
                        user.mdp = BCrypt.Net.BCrypt.HashPassword(mdp);
                    }
                    para = new SqlParameter[10];
                    para[0] = new SqlParameter("id", user.id);
                    para[1] = new SqlParameter("prenom", user.prenom);
                    para[2] = new SqlParameter("nom", user.nom);
                    para[3] = new SqlParameter("cni", user.cni);
                    para[4] = new SqlParameter("telephone", user.telephone);
                    para[5] = new SqlParameter("email", user.email);
                    para[6] = new SqlParameter("login", user.login);
                    para[7] = new SqlParameter("mdp", user.mdp);
                    para[8] = new SqlParameter("rang", user.rang);
                    para[9] = new SqlParameter("acce", user.acce);
                    DAL.Open();
                    DAL.procedure("UTIL_modif", para);
                    DAL.Close();
                    user.users_change();
                    MessageBox.Show("تم حفظ المعلومات");
                }
            }
            if (login_exist(textBox6.Text))
            {
                MessageBox.Show("إسم المستخدم موجود بالفعل","",MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
            }
        }

        private void linkLabel3_MouseEnter(object sender, EventArgs e)
        {
            label18.Visible = true;
        }

        private void linkLabel3_MouseLeave(object sender, EventArgs e)
        {
            label18.Visible = false;
        }

    }
}

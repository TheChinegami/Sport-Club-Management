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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE6
{
    public partial class change_password : DevExpress.XtraEditors.XtraForm
    {
        Form previous;
        page6 page;

        public change_password(Form previous , page6 page)
        {
            InitializeComponent();

            this.previous = previous;
            this.page = page;
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

        public bool pass_confirmation()
        {
            if (textBox1.Text != textBox2.Text)
            {
                panel5.BackColor = Color.Red;
                return true;
            }
            else
            {
                panel5.BackColor = Color.White;
                return false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;
            pictureBox2.Visible = true;
            textBox1.UseSystemPasswordChar = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;
            pictureBox4.Visible = true;
            textBox1.UseSystemPasswordChar = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureBox3.Visible = true;
            textBox2.UseSystemPasswordChar = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            pictureBox1.Visible = true;
            textBox2.UseSystemPasswordChar = true;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (pass_confirmation() || is_empty(textBox1.Text,panel6))
            {
                pass_confirmation();
                is_empty(textBox1.Text, panel6);
            }
            else
            {
                page.mdp = textBox1.Text;
                this.Close();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void change_password_FormClosing(object sender, FormClosingEventArgs e)
        {
            previous.Show();
        }
    }
}
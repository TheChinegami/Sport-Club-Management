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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.PAGE1
{
    public partial class add_edit_trainee : DevExpress.XtraEditors.XtraForm
    {
        int index , ceinture;
        string min;
        Form Previous;
        SqlParameter[] para;
        DAL.DAL DAL = new DAL.DAL();
        SqlDataReader dr;
        
        public add_edit_trainee(int index, Form Previous)
        {
            InitializeComponent();
            this.Previous = Previous;
            this.index = index;
            if (index == -1)
            {
                add.Visible = true;
                label1.Text = "إضافة متدرب";
                this.Text = "إضافة متدرب";
                comboBoxEdit1.Text = DateTime.Now.Year.ToString();
                comboBoxEdit2.SelectedIndex = DateTime.Now.Month - 1;
            }
            else
            {
                edit.Visible = true;
                label1.Text = "تعديل متدرب";
                this.Text = "تعديل متدرب";
                get_data();
            }
        }

        public bool is_empty(string text,Panel panel)
        {
            if (text.Replace(" ","") == "")
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

        public bool min_verify()
        {
            if (min=="")
            {
                panel7.BackColor = Color.White;
                return false;
            }
            else
            {
                if (Convert.ToDateTime(min)< new DateTime(Convert.ToInt32(comboBoxEdit1.Text),comboBoxEdit2.SelectedIndex + 1, 1))
                {
                    MessageBox.Show("هناك إنقطاع قبل "+min+" يجب أن تقوم بحذفه لتتم هذه العملية", "تاريخ الإلتحاق غير صحيح", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    panel7.BackColor = Color.Red;
                    return true;
                }
                else
                {
                    panel7.BackColor = Color.White;
                    return false;
                }
            }
        }

        public bool birthday(DateTime date, Panel panel)
        {
            if (Convert.ToInt32(comboBoxEdit1.Text) - date.Year >= 60 || Convert.ToInt32(comboBoxEdit1.Text) - date.Year < 4)
            {
                MessageBox.Show("يجب أن يكون عمر المتدرب عند الإلتحاق بين 5 و 60 سنة", "تاريخ الإزدياد غير صحيح", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                panel.BackColor = Color.Red;
                return true;
            }
            else
            {
                panel.BackColor = Color.White;
                return false;
            }
        }
        public bool Joining_Date(DateTime date, Panel panel)
        {
            if (date>DateTime.Now.AddMonths(1))
            {
                MessageBox.Show("يمكن أن يكون تاريخ الإلتحاق في الشهر القادم أو ما قبله فقط", "تاريخ الإلتحاق غير صحيح", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                panel.BackColor = Color.Red;
                return true;
            }
            else
            {
                panel.BackColor = Color.White;
                return false;
            }
        }

        public void get_data()
        {
            para = new SqlParameter[1];
            para[0] = new SqlParameter("id",index);
            DAL.Open();
            dr = DAL.Read("AP_affichage det", para);
            dr.Read();
            textBox1.Text = dr.GetString(0);
            textBox2.Text = dr.GetString(1);
            dateEdit1.DateTime = dr.GetDateTime(2);
            textBox7.Text = dr.GetString(3);
            comboBoxEdit2.SelectedIndex = dr.GetDateTime(5).Month - 1;
            comboBoxEdit1.Text = dr.GetDateTime(5).Year.ToString();
            comboBoxEdit3.Text = dr.GetString(6);
            imageComboBoxEdit1.Text = dr.GetString(7);
            textBox8.Text = dr.GetString(8);
            textBox4.Text = dr.GetString(10);
            textBox3.Text = dr.GetString(11);
            textBox6.Text = dr.GetString(12);
            textBox5.Text = dr.GetString(13);
            ceinture = dr.GetInt32(14);
            min = dr.GetString(15);
            DAL.Close();
            for (int i=0; i<ceinture && i<9; i++)
            {
                imageComboBoxEdit1.Properties.Items.RemoveAt(0);
            }
        }


        private void add_edit_trainee_FormClosing(object sender, FormClosingEventArgs e)
        {
            Previous.Visible = true;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (is_empty(textBox1.Text,panel4) || is_empty(textBox2.Text, panel5) || is_empty(dateEdit1.Text, panel6) || Joining_Date(new DateTime(Convert.ToInt32(comboBoxEdit1.Text),comboBoxEdit2.SelectedIndex+1,1), panel7) || birthday(dateEdit1.DateTime, panel6))
            {
                is_empty(textBox1.Text, panel4);
                is_empty(textBox2.Text, panel5);
                is_empty(dateEdit1.Text, panel6);
            }
            else
            {
                para = new SqlParameter[12];
                para[0] = new SqlParameter("prenom", textBox1.Text);
                para[1] = new SqlParameter("nom", textBox2.Text);
                para[2] = new SqlParameter("date_de_naissance", dateEdit1.DateTime.Month.ToString()+'/'+ dateEdit1.DateTime.Day.ToString()+'/'+ dateEdit1.DateTime.Year.ToString());
                para[3] = new SqlParameter("cni", textBox7.Text);
                para[4] = new SqlParameter("date_dinscription", (comboBoxEdit2.SelectedIndex +1).ToString()+"/01/"+comboBoxEdit1.Text);
                para[5] = new SqlParameter("group", comboBoxEdit3.Text);
                para[6] = new SqlParameter("ceinture", imageComboBoxEdit1.SelectedIndex);
                if (textBox8.Text=="0")
                    para[7] = new SqlParameter("telephone","");
                else
                    para[7] = new SqlParameter("telephone", textBox8.Text);
                para[8] = new SqlParameter("prenom_de_tuteur", textBox4.Text);
                para[9] = new SqlParameter("nom_de_tuteur", textBox3.Text);
                para[10] = new SqlParameter("cni_de_tuteur", textBox6.Text);
                if (textBox5.Text == "0")
                    para[11] = new SqlParameter("telephone_de_tuteur", "0");
                else
                    para[11] = new SqlParameter("telephone_de_tuteur", textBox5.Text);
                DAL.Open();
                DAL.procedure("AP_ajout", para);
                DAL.Close();
                this.Close();
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            if (is_empty(textBox1.Text, panel4) || is_empty(textBox2.Text, panel5) || is_empty(dateEdit1.Text, panel6) || Joining_Date(new DateTime(Convert.ToInt32(comboBoxEdit1.Text), comboBoxEdit2.SelectedIndex + 1, 1), panel7) || birthday(dateEdit1.DateTime, panel6) || min_verify())
            {
                is_empty(textBox1.Text, panel4);
                is_empty(textBox2.Text, panel5);
                is_empty(dateEdit1.Text, panel6);
            }
            else
            {
                para = new SqlParameter[13];
                para[0] = new SqlParameter("id", index);
                para[1] = new SqlParameter("prenom", textBox1.Text);
                para[2] = new SqlParameter("nom", textBox2.Text);
                para[3] = new SqlParameter("date_de_naissance", dateEdit1.DateTime.Month.ToString() + '/' + dateEdit1.DateTime.Day.ToString() + '/' + dateEdit1.DateTime.Year.ToString());
                para[4] = new SqlParameter("cni", textBox7.Text);
                para[5] = new SqlParameter("date_dinscription", (comboBoxEdit2.SelectedIndex + 1).ToString() + "/01/" + comboBoxEdit1.Text);
                para[6] = new SqlParameter("group", comboBoxEdit3.Text);
                para[7] = new SqlParameter("ceinture", imageComboBoxEdit1.SelectedIndex + ceinture);
                if (textBox8.Text == "0")
                    para[8] = new SqlParameter("telephone", "");
                else
                    para[8] = new SqlParameter("telephone", textBox8.Text);
                para[9] = new SqlParameter("prenom_de_tuteur", textBox4.Text);
                para[10] = new SqlParameter("nom_de_tuteur", textBox3.Text);
                para[11] = new SqlParameter("cni_de_tuteur", textBox6.Text);
                if (textBox5.Text == "0")
                    para[12] = new SqlParameter("telephone_de_tuteur", "");
                else
                    para[12] = new SqlParameter("telephone_de_tuteur", textBox5.Text);
                DAL.Open();
                DAL.procedure("AP_modif", para);
                DAL.Close();
                this.Close();
            }
        }
    }
}
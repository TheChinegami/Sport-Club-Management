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
    public partial class edit_out_of_training : DevExpress.XtraEditors.XtraForm
    {
        Form Previous;
        int index , count , cas,id;
        SqlParameter[] para;
        DAL.DAL DAL = new DAL.DAL();
        SqlDataReader dr;
        DateTime date1, date2, date3;
        bool date1b = false , date11b = true , date2b=false;

        public edit_out_of_training(int index , int id, string date1s, string date2s, string date3s, Form Previous, int count,int cas )
        {
            InitializeComponent();

            this.Previous = Previous;
            this.index = index;
            this.count = count;
            this.cas = cas;
            this.id = id;
            
            if (date1s != "")
            {
                date1 = Convert.ToDateTime(date1s);
                comboBoxEdit3.Text = date1.Year.ToString();
                comboBoxEdit4.SelectedIndex = date1.Month;
                date1b = true;
                date11b = false;
            }
            if (cas == 0)
            {
                this.Text = "إضافة";
                simpleButton2.Visible = false;
                simpleButton3.Visible = true;
                date11b = false;
            }

            date2 = Convert.ToDateTime(date2s);
            comboBoxEdit1.Text = date2.Year.ToString();
            comboBoxEdit2.SelectedIndex = (date2.Month - 1);
            
            date3 = Convert.ToDateTime(date3s);

            date2b = true;  // 39l 3La debut d'interuption : janvier
            changing();
        }

        public bool verify()
        {
            DAL.Open();
            para = new SqlParameter[4];
            para[0] = new SqlParameter("id_app", index);
            para[1] = new SqlParameter("id", id);
            para[2] = new SqlParameter("date_debut", date2);
            if (date1b == true)
            {
                para[3] = new SqlParameter("date_fin", date1);
            }
            else
            {
                para[3] = new SqlParameter("date_fin", DBNull.Value);
            }
            dr = DAL.Read("HE_verifier", para);
            dr.Read();
            if (dr.GetInt32(0) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            DAL.Close();
        }

        private void edit_out_of_training_FormClosing(object sender, FormClosingEventArgs e)
        {
            Previous.Visible = true;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (verify())
            {
                DAL.Open();
                para = new SqlParameter[3];
                para[0] = new SqlParameter("id", index);
                para[1] = new SqlParameter("date_debut", date2);
                if (date1b == true)
                {
                    para[2] = new SqlParameter("date_fin", date1);
                }
                else
                {
                    para[2] = new SqlParameter("date_fin", DBNull.Value);
                }
                DAL.procedure("HE_ajout", para);
                DAL.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("يوجد بالفعل إنقطاع في هذه الفترة، قم بحذفه أو غير التواريخ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (date2b == true)
            {
                changing();
            }
        }

        private void changing()
        {
            date2 = new DateTime(Convert.ToInt32(comboBoxEdit1.Text), (comboBoxEdit2.SelectedIndex + 1), 01);
            if (comboBoxEdit3.SelectedIndex != 0 && comboBoxEdit4.SelectedIndex != 0)
            {
                date1 = new DateTime(Convert.ToInt32(comboBoxEdit3.Text), (comboBoxEdit4.SelectedIndex), 01);
                date1b = true;
            }
            else
            {
                date1b = false;
            }
            

            if (DateTime.Compare(date2, date3) < 0 || (date1b == true && DateTime.Compare(date1, date2) < 0))
            {
                if (DateTime.Compare(date2, date3) < 0)
                {
                    label3.Text = "⚠️ يجب أن يكون تاريخ الإنقطاع بعد تاريخ التسجيل";
                }
                else
                {
                    label3.Text = "";
                }
                if (date1b == true && DateTime.Compare(date1, date2) < 0)
                {
                    label2.Text = "⚠️ يجب أن يكون تاريخ الرجوع بعد تاريخ الإنقطاع";
                }
                else
                {
                    label2.Text = "";
                }
                if (cas == 0)
                {
                    simpleButton3.Visible = true;
                    simpleButton3.Enabled = false;
                }
                else
                {
                    simpleButton2.Visible = true;
                    simpleButton2.Enabled = false;
                }
                simpleButton5.Visible = false;
                simpleButton2.Enabled = false;
            }
            else if (DateTime.Compare(date1, date2) == 0)
            {
                label2.Text = "⚠️ سوف يتم حذف الإنقطاع";
                if (cas == 0)
                {
                    simpleButton3.Visible = false;
                }
                else
                {
                    simpleButton2.Visible = false;
                }
                simpleButton5.Visible = true;
            }
            else if (date11b == false && date1b == false && count > 0)
            {
                if (cas == 0)
                {
                    simpleButton3.Visible = true;
                    simpleButton3.Enabled = false;
                }
                else
                {
                    simpleButton2.Visible = true;
                    simpleButton2.Enabled = false;
                }
                simpleButton5.Visible = false;
                label2.Text = "⚠️ أدخل تاريخ الرجوع";
            }
            else
            {
                label3.Text = "";
                label2.Text = "";
                if (cas == 0)
                {
                    simpleButton3.Visible = true;
                    simpleButton3.Enabled = true;
                }
                else
                {
                    simpleButton2.Visible = true;
                    simpleButton2.Enabled = true;
                }
                simpleButton5.Visible = false;
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new PAGES.MESSAGES.confirmation(this, index, "HE_suppression", 1).Show();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (verify())
            {
                DAL.Open();
                para = new SqlParameter[3];
                para[0] = new SqlParameter("id", id);
                para[1] = new SqlParameter("date_debut", date2);
                if (date1b == true)
                {
                    para[2] = new SqlParameter("date_fin", date1);
                }
                else
                {
                    para[2] = new SqlParameter("date_fin", DBNull.Value);
                }
                DAL.procedure("HE_modification", para);
                DAL.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("يوجد بالفعل إنقطاع في هذه الفترة، قم بحذفه أو غير التواريخ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
    }
}
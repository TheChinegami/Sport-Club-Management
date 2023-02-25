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
    public partial class page6__password : DevExpress.XtraEditors.XtraUserControl
    {
        Form1 Previous;
        int i;

        public page6__password(Form1 Previous,int i)
        {
            InitializeComponent();

            this.Previous = Previous;
            this.i = i;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (BCrypt.Net.BCrypt.Verify(textBox1.Text, user.mdp))
            {
                if (i == 0)
                {
                    Previous.LoadPage(new PL.PAGES.PAGE6.page6(Previous));
                }
                else
                {
                    Previous.LoadPage(new PL.PAGES.PAGE7.page7(Previous));
                }
                label4.Text = "";
            }
            else
            {
                label4.Text = "⚠️ كلمة المرور غير صحيحة";
            }
        }
    }
}

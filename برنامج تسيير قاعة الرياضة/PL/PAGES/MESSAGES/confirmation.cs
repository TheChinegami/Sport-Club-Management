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

namespace برنامج_تسيير_قاعة_الرياضة.PL.PAGES.MESSAGES
{
    public partial class confirmation : DevExpress.XtraEditors.XtraForm
    {
        Form previous;
        int index;
        int choix = 0;
        string proc;
        bool click = false;
        SqlParameter[] para;
        DAL.DAL DAL = new DAL.DAL();

        public confirmation(Form previous, int index , string proc, int choix )
        {
            InitializeComponent();
            this.previous = previous;
            this.index = index;
            this.choix = choix;
            this.proc= proc;
        }
        public confirmation(Form previous, int index, string proc)
        {
            InitializeComponent();
            this.previous = previous;
            this.index = index;
            this.proc = proc;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            click = true;
            DAL.Open();
            para = new SqlParameter[1];
            para[0] = new SqlParameter("id", index);
            DAL.procedure(proc, para);
            DAL.Close();
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmation_FormClosing(object sender, FormClosingEventArgs e)
        {
            previous.Visible = true;
            if (choix == 1 && click==true)
            {
                previous.Close();
            }
        }
    }
}
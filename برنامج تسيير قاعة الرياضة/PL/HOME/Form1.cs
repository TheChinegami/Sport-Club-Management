using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace برنامج_تسيير_قاعة_الرياضة
{
    public partial class Form1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        Form previous;
        PL.PAGES.PAGE3.page3 Page3;
        DevExpress.XtraEditors.XtraUserControl Page;
        public Form1(Form previous)
        {
            InitializeComponent();
            //LoadPage(new PL.PAGES.PAGE1.page1(this));

            this.previous = previous;

            acce();
        }

        public void LoadPage(DevExpress.XtraEditors.XtraUserControl Page)
        {
            try
            {
                fluentDesignFormContainer1.Controls.Clear();
                Page.Dock = DockStyle.Fill;
                fluentDesignFormContainer1.Controls.Add(Page);
            }
            catch 
            {
            }
        }

        public void acce()
        {
            barStaticItem1.Caption = user.prenom + " " + user.nom;
            barStaticItem2.Caption = "الصفة : " + user.rang;
            //LoadPage(new PL.PAGES.PAGE6.page6__password(this, 0));
            Page = new PL.PAGES.PAGE6.page6__password(this, 0);
            if (user.rang == "مستخدم")
            {
                accordionControlElement7.Visible = false;
            }
            if (user.acce.Contains("-17-"))
            {
                accordionControlElement8.Enabled = true;
                //LoadPage(new PL.PAGES.PAGE8.page8(this));
                Page = new PL.PAGES.PAGE8.page8(this);
            }
            if (user.acce.Contains("-15-"))
            {
                accordionControlElement5.Enabled = true;
                //LoadPage(new PL.PAGES.PAGE5.page5(this));
                Page = new PL.PAGES.PAGE5.page5(this);
            }
            if (user.acce.Contains("-13-"))
            {
                accordionControlElement4.Enabled = true;
                //LoadPage(new PL.PAGES.PAGE4.page4(this));
                Page = new PL.PAGES.PAGE4.page4(this);
            }
            if (user.acce.Contains("-11-"))
            {
                accordionControlElement3.Enabled = true;
                //Page3 = new PL.PAGES.PAGE3.page3(this);
                //LoadPage(Page3);
                //Page3.get_data();

                Page = new PL.PAGES.PAGE3.page3(this);
            }
            if (user.acce.Contains("-7-"))
            {
                accordionControlElement2.Enabled = true;
                //LoadPage(new PL.PAGES.PAGE2.page2(this));
                Page = new PL.PAGES.PAGE2.page2(this);
            }
            if (user.acce.Contains("-1-"))
            {
                accordionControlElement1.Enabled = true;
                //LoadPage(new PL.PAGES.PAGE1.page1(this));
                Page = new PL.PAGES.PAGE1.page1(this);
            }

            LoadPage(Page);
        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {
            LoadPage(new PL.PAGES.PAGE1.page1(this));
        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {
            LoadPage(new PL.PAGES.PAGE2.page2(this));
        }

        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            Page3 = new PL.PAGES.PAGE3.page3(this);
            LoadPage(Page3);
            Page3.get_data();
        }

        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            LoadPage(new PL.PAGES.PAGE4.page4(this));
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            LoadPage(new PL.PAGES.PAGE5.page5(this));
        }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            LoadPage(new PL.PAGES.PAGE6.page6__password(this,0));
        }

        private void accordionControlElement7_Click(object sender, EventArgs e)
        {
            LoadPage(new PL.PAGES.PAGE6.page6__password(this, 1));
        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            LoadPage(new PL.PAGES.PAGE8.page8(this));
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            previous.Close();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Hide();
            previous.Show();
        }
    }
}

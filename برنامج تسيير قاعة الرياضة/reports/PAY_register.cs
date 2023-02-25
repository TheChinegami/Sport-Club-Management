using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    public partial class PAY_register : DevExpress.XtraReports.UI.XtraReport
    {
        double sum = 0;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;
        string month;
        private string mois(int mois)
        {
            switch (mois)
            {
                case 0:
                    month = "تأمين";
                    break;
                case 1:
                    month = "يناير";
                    break;
                case 2:
                    month = "فبراير";
                    break;
                case 3:
                    month = "مارس";
                    break;
                case 4:
                    month = "أبريل";
                    break;
                case 5:
                    month = "ماي";
                    break;
                case 6:
                    month = "يونيو";
                    break;
                case 7:
                    month = "يوليوز";
                    break;
                case 8:
                    month = "غشت";
                    break;
                case 9:
                    month = "شتنبر";
                    break;
                case 10:
                    month = "أكتوبر";
                    break;
                case 11:
                    month = "نونبر";
                    break;
                case 12:
                    month = "دجنبر";
                    break;
                default:
                    month = "";
                    break;
            }
            return month;
        }

        public PAY_register(int anne, int mois,int condition)
        {
            InitializeComponent();

            label2.Text = this.mois(mois) + " " + anne.ToString();

            DAL.Open();
            para = new SqlParameter[2];
            para[0] = new SqlParameter("anne", anne);
            para[1] = new SqlParameter("mois", mois);
            if(condition == 0)
            {
                dr = DAL.Read("PAY_register", para);
            }
            else
            {
                dr = DAL.Read("PAY_register_par_mois", para);
                xrLabel4.Text = "سجل الأداء حسب الشهر المؤدى";
            }

            if (dr.Read())
            {
                xrTable1.Rows[0].Cells[0].Text = dr.GetDouble(2).ToString();
                xrTable1.Rows[0].Cells[1].Text = dr.GetString(3);
                xrTable1.Rows[0].Cells[2].Text = dr.GetDateTime(4).ToShortDateString();
                xrTable1.Rows[0].Cells[3].Text = dr.GetString(1);
                sum = sum + dr.GetDouble(2);
            }
            else
            {
                xrTable1.Rows.RemoveAt(0);
            }
            while (dr.Read())
            {
                xrTable1.InsertRowBelow(xrTable1.Rows[xrTable1.Rows.Count - 1]);

                xrTable1.Rows[xrTable1.Rows.Count - 1].Cells[0].Text = dr.GetDouble(2).ToString();
                xrTable1.Rows[xrTable1.Rows.Count - 1].Cells[1].Text = dr.GetString(3);
                xrTable1.Rows[xrTable1.Rows.Count - 1].Cells[2].Text = dr.GetDateTime(4).ToShortDateString();
                xrTable1.Rows[xrTable1.Rows.Count - 1].Cells[3].Text = dr.GetString(1);
                sum = sum + dr.GetDouble(2);

                
            }

            xrLabel1.Text = sum.ToString();
            dr.Close();
            DAL.Close();

        }

    }
}

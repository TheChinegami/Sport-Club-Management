using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    public partial class Rapport_financier : DevExpress.XtraReports.UI.XtraReport
    {
        string month;
        private string mois(int mois)
        {
            switch (mois)
            {
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

        public Rapport_financier(int anne , int mois)
        {
            InitializeComponent();

            label2.Text = this.mois(mois) + " " + anne.ToString();

            reports.revenus rp1 = new reports.revenus(anne, mois);
            reports.dépenses rp2 = new reports.dépenses(anne,mois);

            xrSubreport2.ReportSource = rp2;
            xrSubreport1.ReportSource = rp1;

            xrLabel1.Text = (rp1.summ() - rp2.summ()).ToString();
        }

    }
}

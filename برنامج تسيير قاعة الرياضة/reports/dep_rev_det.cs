using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    public partial class dep_rev_det : DevExpress.XtraReports.UI.XtraReport
    {
        double sum = 0;
        string mois_anne;
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;

        public dep_rev_det( int id , int condition)
        {
            InitializeComponent();


            DAL.Open();
            para = new SqlParameter[1];
            para[0] = new SqlParameter("id", id);
            if (condition == 2)
            {
                dr = DAL.Read("PRO_recuperation", para);
            }
            else
            {
                dr = DAL.Read("CHA_recuperation", para);
                xrLabel4.Text = "تفاصيل المصروف";
                tableCell4.Text = "الوجهة";
            }
            if (dr.Read())
            {
                label2.Text = dr.GetString(2);
                mois_anne = dr.GetString(0) + " " + dr.GetInt32(1).ToString();
            }
            dr.Close();
            DAL.Close();


            DAL.Open();
            para = new SqlParameter[1];
            para[0] = new SqlParameter("id", id);
            if (condition == 2)
            {
                dr = DAL.Read("PRO_DET_affichage", para);
            }
            else
            {
                dr = DAL.Read("CHA_DET_affichage", para);
            }

            if (dr.Read())
            {
                xrTable1.Rows[0].Cells[0].Text = dr.GetDouble(2).ToString();
                xrTable1.Rows[0].Cells[1].Text = dr.GetInt32(1).ToString() + " " + mois_anne;
                xrTable1.Rows[0].Cells[1].RightToLeft = RightToLeft.Yes;
                xrTable1.Rows[0].Cells[2].Text = dr.GetString(3);
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
                xrTable1.Rows[xrTable1.Rows.Count - 1].Cells[1].Text = dr.GetInt32(1).ToString() + " " + mois_anne;
                xrTable1.Rows[xrTable1.Rows.Count - 1].Cells[1].RightToLeft = RightToLeft.Yes;
                xrTable1.Rows[xrTable1.Rows.Count - 1].Cells[2].Text = dr.GetString(3);
                sum = sum + dr.GetDouble(2);


            }

            xrLabel1.Text = sum.ToString();
            dr.Close();
            DAL.Close();
        }

    }
}

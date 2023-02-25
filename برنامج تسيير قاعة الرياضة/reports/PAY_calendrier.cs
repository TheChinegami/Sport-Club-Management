using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    public partial class PAY_calendrier : DevExpress.XtraReports.UI.XtraReport
    {
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;

        public PAY_calendrier(int anne)
        {
            InitializeComponent();

            label2.Text = "السنة : " + anne.ToString();

            DAL.Open();
            para = new SqlParameter[1];
            para[0] = new SqlParameter("anne", anne);
            dr = DAL.Read("PAY_calendrier", para);

            if (dr.Read())
            {
                xrTable1.Visible = true;

                int j = xrTable1.Rows[0].Cells.Count - 1;
                for (int i = 1; i < dr.FieldCount; i++)
                {
                    xrTable1.Rows[0].Cells[j--].Text = dr.GetString(i);
                }

            }

            while (dr.Read())
            {
                xrTable1.InsertRowBelow(xrTable1.Rows[xrTable1.Rows.Count - 1]);

                int j = xrTable1.Rows[0].Cells.Count - 1;
                for (int i = 1; i < dr.FieldCount; i++)
                {
                    xrTable1.Rows[xrTable1.Rows.Count - 1].Cells[j--].Text = dr.GetString(i);
                }
            }

            dr.Close();
            DAL.Close();

            foreach (XRTableRow row in xrTable1.Rows)
            {
                foreach (XRTableCell cell in row.Cells)
                {
                    if (cell.Text.Contains("g") && cell.Index < 13)
                    {
                        cell.Text = "";
                        cell.BackColor = Color.Gray;
                    }
                    if (cell.Text.Contains("r") && cell.Index < 13)
                    {
                        cell.Text = cell.Text.Replace("r", "");
                        cell.BackColor = Color.Pink;
                    }
                }
            }
        }

    }
}

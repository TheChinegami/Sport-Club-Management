using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data.SqlClient;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    public partial class AP_PRE_register : DevExpress.XtraReports.UI.XtraReport
    {
        public AP_PRE_register(int id,int anne,string name)
        {
            InitializeComponent();
            label1.Text = "سجل غياب : " + name;
            label2.Text = "السنة : " + anne.ToString();

            DAL.DAL DAL = new DAL.DAL();
            SqlParameter[] para;
            SqlDataReader dr;

            DAL.Open();
            para = new SqlParameter[2];
            para[0] = new SqlParameter("anne", anne);
            para[1] = new SqlParameter("id", id);
            dr = DAL.Read("AP_PRE_register", para);

            foreach (XRTableRow row in this.table2.Rows)
            {
                row.Cells[12].Text = (row.Index + 1).ToString();
                dr.Read();

                row.Cells[0].Text = dr.GetString(12);
                row.Cells[1].Text = dr.GetString(11);
                row.Cells[2].Text = dr.GetString(10);
                row.Cells[3].Text = dr.GetString(9);
                row.Cells[4].Text = dr.GetString(8);
                row.Cells[5].Text = dr.GetString(7);
                row.Cells[6].Text = dr.GetString(6);
                row.Cells[7].Text = dr.GetString(5);
                row.Cells[8].Text = dr.GetString(4);
                row.Cells[9].Text = dr.GetString(3);
                row.Cells[10].Text = dr.GetString(2);
                row.Cells[11].Text = dr.GetString(1);
            }
            dr.Close();
            DAL.Close();

            foreach (XRTableRow row in this.table2.Rows)
            {
                foreach (XRTableCell cell in row.Cells)
                {
                    if (cell.Text.Contains("g"))
                    {
                        cell.BackColor = Color.LightGray;
                        cell.Text = "";
                    }
                    if (cell.Text.Contains("r"))
                    {
                        cell.BackColor = Color.DimGray;
                        cell.Text = "";
                    }
                    if (cell.Text.Contains("f"))
                    {
                        cell.BackColor = Color.LightSlateGray;
                        cell.Text = "";
                    }
                }
            }
        }
        

    }
}

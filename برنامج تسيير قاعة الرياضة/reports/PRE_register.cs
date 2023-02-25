using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    public partial class PRE_register : DevExpress.XtraReports.UI.XtraReport
    {
        DAL.DAL DAL = new DAL.DAL();
        SqlParameter[] para;
        SqlDataReader dr;
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

        public PRE_register(int anne , int mois)
        {
            InitializeComponent();

            label2.Text = this.mois(mois) + " " + anne.ToString();

            DAL.Open();
            para = new SqlParameter[2];
            para[0] = new SqlParameter("anne", anne);
            para[1] = new SqlParameter("mois", mois);
            dr = DAL.Read("PRE_register", para);

            xrTableCell2.Text = dr.GetName(1);

            for (int i = 2; i < dr.FieldCount; i++)
            {
                xrTable2.InsertColumnToLeft(xrTableCell2);
            }

            int j = xrTable2.Rows[0].Cells.Count - 2;
            for (int i = 2; i < dr.FieldCount; i++)
            {
                xrTable2.Rows[0].Cells[j--].Text = dr.GetName(i);
            }

            foreach (XRTableCell cell in xrTable2.Rows[0].Cells)
            {
                if (cell.Index == xrTable2.Rows[0].Cells.Count - 1)
                {
                    if (xrTable2.Rows[0].Cells.Count > 1)
                    {
                        cell.WidthF = xrTable2.WidthF / 5;
                    }
                }
                else
                {
                    cell.WidthF = ((4 * xrTable2.WidthF / 5) / (xrTable2.Rows[0].Cells.Count-1));
                }
                cell.Font = xrTableCell2.Font;
                cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                cell.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
                cell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(202)))), ((int)(((byte)(166)))));
            }

            if (dr.Read())
            {
                xrTable1.Visible = true;
                xrTableCell1.Text = dr.GetString(1);

                for (int i = 2; i < dr.FieldCount; i++)
                {
                    xrTable1.InsertColumnToLeft(xrTableCell1);
                }

                j = xrTable1.Rows[0].Cells.Count - 2;
                for (int i = 2; i < dr.FieldCount; i++)
                {
                    xrTable1.Rows[0].Cells[j--].Text = dr.GetString(i);
                }

                foreach (XRTableCell cell in xrTable1.Rows[0].Cells)
                {
                    if (cell.Index == xrTable1.Rows[0].Cells.Count - 1)
                    {
                        if (xrTable1.Rows[0].Cells.Count > 1)
                        {
                            cell.WidthF = xrTable1.WidthF / 5;
                        }
                        else
                        {
                            cell.WidthF = xrTable1.WidthF;
                        }
                        cell.Font = xrTableCell2.Font;
                    }
                    else
                    {
                        cell.WidthF = ((4 * xrTable1.WidthF / 5) / (xrTable1.Rows[0].Cells.Count - 1));
                        cell.Font = xrTableCell1.Font;
                    }
                    cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                    cell.RightToLeft = DevExpress.XtraReports.UI.RightToLeft.Yes;
                    if (cell.Index == xrTable1.Rows[0].Cells.Count-1)
                    {
                        cell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(202)))), ((int)(((byte)(166)))));
                    }
                    else
                    {
                        cell.BackColor = Color.White;
                    }
                }
            }

            while (dr.Read())
            {
                xrTable1.InsertRowBelow(xrTable1.Rows[xrTable1.Rows.Count - 1]);

                j = xrTable1.Rows[0].Cells.Count - 1;
                //xrTable1.Rows[xrTable1.Rows.Count - 1].Cells[j--].Text = dr.GetString(1);
                for (int i = 1; i < dr.FieldCount; i++)
                {
                    xrTable1.Rows[xrTable1.Rows.Count - 1].Cells[j--].Text = dr.GetString(i);
                    //xrTable1.Rows[xrTable1.Rows.Count - 1].Cells[j--].BackColor = Color.White;
                }
            }

            dr.Close();
            DAL.Close();

        }

    }
}

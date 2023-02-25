using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace برنامج_تسيير_قاعة_الرياضة.reports
{
    class datagv_to_datat
    {
        public static DataTable convert(DataGridView dg)
        {
            DataTable exportdatat = new DataTable();
            foreach (DataGridViewColumn col in dg.Columns)
            {
                exportdatat.Columns.Add(col.HeaderText);
            }
            foreach (DataGridViewRow row in dg.Rows)
            {
                DataRow drow = exportdatat.NewRow();
                foreach (DataGridViewCell cell in row.Cells)
                {
                    drow[cell.ColumnIndex] = cell.Value;
                }
                exportdatat.Rows.Add(drow);
            }
            return exportdatat;
        }
    }
}

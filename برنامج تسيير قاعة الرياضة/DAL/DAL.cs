using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace برنامج_تسيير_قاعة_الرياضة.DAL
{
    internal class DAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public DAL()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ismai\OneDrive\Desktop\stage3\برنامج تسيير قاعة  الرياضة v5\برنامج تسيير قاعة الرياضة\DATABASE\salle de sport.mdf;Integrated Security=True");
        }

        public void Open()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            else
            {
                con.Close();
                con.Open();
            }
        }

        public void Close()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        public DataTable Read_data(string rq , SqlParameter[] para)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = rq;
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(para);
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable Read_data(string rq)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = rq;
            da = new SqlDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public SqlDataReader Read(string rq , SqlParameter[] para)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = rq;
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(para);
            return cmd.ExecuteReader();
        }

        public SqlDataReader Read(string rq)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = rq;
            return cmd.ExecuteReader();
        }
        public void procedure(string rq)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.CommandText = rq;
            cmd.ExecuteNonQuery(); 
        }
        public void procedure(string rq , SqlParameter[] para)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = rq;
            cmd.Parameters.Clear();
            cmd.Parameters.AddRange(para);
            cmd.ExecuteNonQuery();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace برنامج_تسيير_قاعة_الرياضة
{
    internal class user
    {
        static DAL.DAL DAL = new DAL.DAL();
        static SqlDataReader dr;

        public static List<string> users = new List<string>();
        public static int id;
        public static string prenom;
        public static string nom;
        public static string cni;
        public static string telephone;
        public static string email;
        public static string login;
        public static string mdp;
        public static string rang;
        public static string acce;

        public static void users_change()
        {
            DAL.Open();
            dr = DAL.Read("UTIL_users");
            user.users.Clear();
            while (dr.Read())
            {
                user.users.Add(dr.GetString(0));
            }
            dr.Close();
            DAL.Close();
        }
    }
}

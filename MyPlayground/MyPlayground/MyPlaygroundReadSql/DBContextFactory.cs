using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlayground.MyPlaygroundReadSql
{
    public class DBContextFactory
    {
        private string _conStr;

        public DBContextFactory()
        {
            _conStr = ConfigurationManager.ConnectionStrings["DbConnection"].ToString();
        }

        public List<string> getData(string context)
        {

            List<string> list = new List<string>();

            SqlConnection conn = new SqlConnection(_conStr);

            conn.Open();

            string query = $"{context}";

            SqlCommand cmd = new SqlCommand(query, conn);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader.GetString(0));
                    list.Add(reader.GetString(1));
                }
            }

            conn.Close();

            return list;
        }
    }
}

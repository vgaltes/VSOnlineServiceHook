using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSOnlineServiceHook.Tests
{
    public static class DBHelper
    {
        static string connection = "ServiceHooksEvents";
        internal static void ClearWorkItemsTable()
        {
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            List<string> workItems = new List<string>();

            try
            {
                sqlConnection =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings[connection].ConnectionString);

                sqlConnection.Open();
                var command =
                    new SqlCommand(
                        string.Format("delete from Events"),
                        sqlConnection);

                command.ExecuteNonQuery();
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();

                if (sqlDataReader != null)
                    sqlDataReader.Close();
            }
        }

        internal static List<string> GetWorkItems()
        {
            SqlConnection sqlConnection = null;
            SqlDataReader sqlDataReader = null;
            List<string> workItems = new List<string>();

            try
            {
                sqlConnection =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings[connection].ConnectionString);

                sqlConnection.Open();
                var command =
                    new SqlCommand(
                        string.Format("select * from Events"),
                        sqlConnection);

                sqlDataReader = command.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    string title = sqlDataReader["Title"].ToString();

                    workItems.Add(title);
                }
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();

                if (sqlDataReader != null)
                    sqlDataReader.Close();
            }

            return workItems;
        }
    }
}

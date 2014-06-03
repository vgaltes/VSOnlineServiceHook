using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSOnlineServiceHook.Tests
{
    public static class DBHelper
    {
        static string connection = "ServiceHooksEvents";
        private const string createDatabaseQuery = @"CREATE DATABASE ""{0}""";
        private const string tableName = "Events";
        private static string createTableQuery = string.Format(@"CREATE TABLE [dbo].[{0}](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](256) NOT NULL,
 CONSTRAINT [PK_Evemts] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]", tableName);
        

        internal static void ClearWorkItemsTable()
        {
            if (!DatabaseExists(connection))
            {
                CreateDatabase(connection);
            }
            if ( !TableExists(connection))
            {
                CreateWorkItemsTable(connection);
            }
            else
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
                            string.Format("delete from {0}", tableName),
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
        }

        private static void CreateWorkItemsTable(string connection)
        {
            SqlConnection sqlConnection = null;

            if (TableExists(connection))
                return;

            try
            {
                sqlConnection =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings[connection].ConnectionString);

                sqlConnection.Open();
                var command =
                    new SqlCommand(
                        createTableQuery,
                        sqlConnection);

                // get data stream
                command.ExecuteNonQuery();
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        private static void CreateDatabase(string connection)
        {
            SqlConnection sqlConnection = null;

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
                string databaseName = connectionString.Split(new string[] { ";" }, StringSplitOptions.None)
                    .First(s => s.StartsWith("Initial Catalog=")).Substring("Initial Catalog=".Count());
                connectionString = connectionString.Replace(databaseName, "master");

                sqlConnection =
                    new SqlConnection(connectionString);


                sqlConnection.Open();
                var command =
                    new SqlCommand(
                        string.Format(createDatabaseQuery, databaseName),
                        sqlConnection);

                // get data stream
                command.ExecuteNonQuery();
            }
            catch (Exception)
            { }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        private static bool DatabaseExists(string connection)
        {
            SqlConnection sqlConnection = null;
            bool databaseExists = false;

            try
            {
                sqlConnection =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings[connection].ConnectionString);

                sqlConnection.Open();

                databaseExists = true;
            }
            catch (SqlException)
            {
                databaseExists = false;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }

            return databaseExists;
        }

        private static bool TableExists(string connectionName)
        {
            SqlConnection sqlConnection = null;
            bool tableExists = false;

            try
            {
                sqlConnection =
                    new SqlConnection(
                        ConfigurationManager.ConnectionStrings[connectionName].ConnectionString);

                sqlConnection.Open();

                DataTable dataTable = sqlConnection.GetSchema("TABLES", new string[] { null, null, tableName });

                tableExists = dataTable.Rows.Count > 0;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();

            }

            return tableExists;
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
                        string.Format("select * from {0}", tableName),
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

using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace ImageConversionUtility
{
    internal class Program
    {
        #region Private Methods

        private static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false)
                .Build();

            var connectionString = configuration.GetSection("connectionString").Value;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("No connection string in config.json");
            }

            using (var conn = new SqlConnection(connectionString))
            {
                const string sql = "select dbo.ConvertImageToBase64(PersonId) from Ali_tblPerson where PhotoImage is not null";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var resultTable = new DataTable();
                        adapter.Fill(resultTable);

                        var writer = new StreamWriter("Queries.sql", false);

                        foreach (DataRow row in resultTable.Rows)
                        {
                            writer.WriteLine(row[0] as string);
                        }

                        writer.Close();
                    }
                }
            }
        }

        #endregion Private Methods
    }
}
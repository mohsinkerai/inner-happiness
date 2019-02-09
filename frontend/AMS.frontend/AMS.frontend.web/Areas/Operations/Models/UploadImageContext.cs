using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;
using AMS.frontend.web.Areas.Operations.Models.Persons;

namespace AMS.frontend.web.Areas.Operations.Models
{
    public class UploadImageContext
    {
        public string ConnectionString { get; set; }

        public UploadImageContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public bool uploadImageToDB(string image, string formNumber)
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    string query = "UPDATE person SET image= \""+image+"\" WHERE id="+formNumber;

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    int i = cmd.ExecuteNonQuery();
                    
                    return i == 1 ? true : false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool GetPersons()
        {
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    string query = "Select * from person LIMIT 20";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    var data = cmd.ExecuteReader();

                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

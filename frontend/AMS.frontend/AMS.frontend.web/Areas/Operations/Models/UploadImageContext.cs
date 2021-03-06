﻿using System;
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

        public List<PersonModel> GetPersons(string query)
        {
            try
            {
                List<PersonModel> personList = new List<PersonModel>();
                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.CommandTimeout = 99999;
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var person = new PersonModel
                            {
                                Id = reader["id"].ToString(),
                                Cnic = reader["cnic"].ToString(),
                                FirstName = reader["full_name"].ToString(),
                                MobilePhone = reader["mobile_phone"].ToString(),
                                PreComputedLatestEducation = reader["latest_education"].ToString(),
                                PreComputedLatestEmployment = reader["latest_employment"].ToString(),
                                Institution = reader["institution_name"].ToString()
                            };

                            personList.Add(person);
                        }
                    }
                    
                    return personList;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

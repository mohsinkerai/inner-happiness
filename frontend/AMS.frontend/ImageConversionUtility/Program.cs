using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
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

            //using (var conn = new SqlConnection(connectionString))
            //{
            //    const string sql = "select dbo.ConvertImageToBase64(PersonId) from Ali_tblPerson where PhotoImage is not null";
            //    using (var cmd = new SqlCommand(sql, conn))
            //    {
            //        using (var adapter = new SqlDataAdapter(cmd))
            //        {
            //            var resultTable = new DataTable();
            //            adapter.Fill(resultTable);

            //            var writer = new StreamWriter("Queries.sql", false);

            //            foreach (DataRow row in resultTable.Rows)
            //            {
            //                writer.WriteLine(row[0] as string);
            //            }

            //            writer.Close();
            //        }
            //    }
            //}

            using (var conn = new MySqlConnection(connectionString))
            {
                const string sql = "SELECT * FROM person";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.CommandTimeout = 5000;
                    using (var adapter = new MySqlDataAdapter(cmd))
                    {
                        var resultTable = new DataTable();
                        adapter.Fill(resultTable);
                        var writer = new StreamWriter("Queries.sql", false);
                        writer.WriteLine("UPDATE country SET country.id = 10000 + country.id;");

                        foreach (DataRow row in resultTable.Rows)
                        {
                            var educations = row["educations"] as string;
                            var akdnTrainings = row["akdn_trainings"] as string;
                            var professionalTrainings = row["professional_trainings"] as string;
                            var id = Convert.ToInt32(row["id"]);

                            if (!string.IsNullOrWhiteSpace(educations) && educations.Contains("countryOfStudy"))
                            {
                                var educationsJson = JArray.Parse(educations);
                                foreach (var education in educationsJson)
                                {
                                    if (Convert.ToInt32(education["countryOfStudy"]) != 0)
                                    {
                                        education["countryOfStudy"] =
                                            10000 + Convert.ToInt32(education["countryOfStudy"]);
                                    }
                                }

                                educations = educationsJson.ToString(Formatting.None);

                                var updateSql =
                                        $"UPDATE person SET person.educations = '{educations}' WHERE person.id = {id};";
                                writer.WriteLine(updateSql);
                            }

                            if (!string.IsNullOrWhiteSpace(akdnTrainings) && akdnTrainings.Contains("countryOfTraining"))
                            {
                                var akdnTrainingsJson = JArray.Parse(akdnTrainings);
                                foreach (var akdnTraining in akdnTrainingsJson)
                                {
                                    if (Convert.ToInt32(akdnTraining["countryOfTraining"]) != 0)
                                    {
                                        akdnTraining["countryOfTraining"] =
                                            10000 + Convert.ToInt32(akdnTraining["countryOfTraining"]);
                                    }
                                    if (Convert.ToInt32(akdnTraining["training"]) != 0)
                                    {
                                        akdnTraining["training"] =
                                            10000 + Convert.ToInt32(akdnTraining["training"]);
                                    }
                                }

                                akdnTrainings = akdnTrainingsJson.ToString(Formatting.None);

                                var updateSql =
                                    $"UPDATE person SET person.akdn_trainings = '{akdnTrainings}' WHERE person.id = {id};";
                                writer.WriteLine(updateSql);
                            }

                            if (!string.IsNullOrWhiteSpace(professionalTrainings) && professionalTrainings.Contains("countryOfTraining"))
                            {
                                var professionalTrainingsJson = JArray.Parse(professionalTrainings);
                                foreach (var professionalTraining in professionalTrainingsJson)
                                {
                                    if (Convert.ToInt32(professionalTraining["countryOfTraining"]) != 0)
                                    {
                                        professionalTraining["countryOfTraining"] =
                                            10000 + Convert.ToInt32(professionalTraining["countryOfTraining"]);
                                    }
                                }

                                professionalTrainings = professionalTrainingsJson.ToString(Formatting.None);

                                var updateSql =
                                    $"UPDATE person SET person.professional_trainings = '{professionalTrainings}' WHERE person.id = {id};";
                                writer.WriteLine(updateSql);
                            }
                        }

                        writer.Close();
                    }
                }
            }
        }

        #endregion Private Methods
    }
}
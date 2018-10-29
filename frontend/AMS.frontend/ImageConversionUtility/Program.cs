using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", false)
                .Build();

            string connectionString = configuration.GetSection("connectionString").Value;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("No connection string in config.json");
            }

            //var writer = new StreamWriter("Queries.sql");
            //var reader = new StreamReader("person_education_update.sql");

            //while (!reader.EndOfStream)
            //{
            //    var query = reader.ReadLine();
            //    var start = query.Substring(0, query.IndexOf("'") + 1);
            //    var json = query.Substring(query.IndexOf("'") + 1, query.LastIndexOf("'") - query.IndexOf("'") - 1);
            //    var end = query.Substring(query.LastIndexOf("'"), query.Length - query.LastIndexOf("'"));
            //    var educations = JsonConvert.DeserializeObject<List<Query>>(json);

            //    foreach (var education in educations)
            //    {
            //        if (education.CountryOfStudy != 0)
            //        {
            //            education.CountryOfStudy = 10000 + education.CountryOfStudy;
            //        }
            //        if (education.Institution != 0)
            //        {
            //            education.Institution = 10000 + education.Institution;
            //        }
            //        if (education.NameOfDegree != 0)
            //        {
            //            education.NameOfDegree = 10000 + education.NameOfDegree;
            //        }
            //        if (education.MajorAreaOfStudy != 0)
            //        {
            //            education.MajorAreaOfStudy = 10000 + education.MajorAreaOfStudy;
            //        }
            //    }

            //    writer.WriteLine(
            //        $"{start}{JsonConvert.SerializeObject(educations, Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore})}{end}");
            //}

            //writer.Close();

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

            StreamReader reader = new StreamReader("institutionMappings.txt");
            Dictionary<int, int> institutionMappings = new Dictionary<int, int>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line.Contains("--"))
                {
                    institutionMappings.Add(-1 * Convert.ToInt32(line.Split('-')[2]), Convert.ToInt32(line.Split('-')[0]));
                }
                else
                {
                    institutionMappings.Add(Convert.ToInt32(line.Split('-')[1]), Convert.ToInt32(line.Split('-')[0]));
                }
            }

            reader.Close();

            StreamReader positionReader = new StreamReader("positionMapping.txt");
            Dictionary<int, int> positionMappings = new Dictionary<int, int>();

            while (!positionReader.EndOfStream)
            {
                string line = positionReader.ReadLine();
                positionMappings.Add(Convert.ToInt32(line.Split('-')[1]), Convert.ToInt32(line.Split('-')[0]));
            }

            positionReader.Close();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                const string sql = "select personId from Ali_tblPerson;";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable resultTable = new DataTable();
                        adapter.Fill(resultTable);

                        StreamWriter writer = new StreamWriter("Queries.sql", false);
                        string start = "UPDATE person SET person.voluntary_community_services = '";
                        string end = "' WHERE person.id = ";

                        foreach (DataRow row in resultTable.Rows)
                        {
                            int personId = Convert.ToInt32(row[0]);
                            List<VoluntaryCommunity> services = new List<VoluntaryCommunity>();
                            using (SqlCommand innerCmd = new SqlCommand($"select * from Ali_tblCommunityService where PersonId = {personId};", conn))
                            {
                                using (SqlDataAdapter innerAdapter = new SqlDataAdapter(innerCmd))
                                {
                                    DataTable innerResultTable = new DataTable();
                                    innerAdapter.Fill(innerResultTable);

                                    foreach (DataRow dataRow in innerResultTable.Rows)
                                    {
                                        object value = dataRow[4];
                                        if (value != DBNull.Value && Convert.ToInt32(dataRow[4]) > 0)
                                        {
                                            services.Add(new VoluntaryCommunity
                                            {
                                                institution = dataRow[2] == DBNull.Value ? 0 :
                                                    Convert.ToInt32(dataRow[2]) == 0 ? 0 : institutionMappings.ContainsKey(Convert.ToInt32(dataRow[2])) ?
                                                        institutionMappings[Convert.ToInt32(dataRow[2])] : 0,
                                                cycleId = dataRow[4] == DBNull.Value ? 0 : Convert.ToInt32(dataRow[4]),
                                                fromYear = dataRow[5] == DBNull.Value ? 0 : Convert.ToInt32(dataRow[5]),
                                                toYear = dataRow[6] == DBNull.Value ? 0 : Convert.ToInt32(dataRow[6]),
                                                isImamatAppointee = true,
                                                position = dataRow[3] == DBNull.Value ? 0 :
                                                    Convert.ToInt32(dataRow[3]) == 0 ? 0 : positionMappings.ContainsKey(Convert.ToInt32(dataRow[3])) ?
                                                        positionMappings[Convert.ToInt32(dataRow[3])] : 0,
                                                priority = dataRow[8] == DBNull.Value ? 0 : Convert.ToInt32(dataRow[8]),
                                                voluntaryCommunityId = dataRow[1] as string
                                            });
                                        }
                                        else
                                        {
                                            services.Add(new VoluntaryCommunity
                                            {
                                                institution = dataRow[2] == DBNull.Value ? 0 :
                                                    Convert.ToInt32(dataRow[2]) == 0 ? 0 : institutionMappings.ContainsKey(Convert.ToInt32(dataRow[2]))?
                                                    institutionMappings[Convert.ToInt32(dataRow[2])] : 0,
                                                fromYear = dataRow[5] == DBNull.Value ? 0 : Convert.ToInt32(dataRow[5]),
                                                toYear = dataRow[6] == DBNull.Value ? 0 : Convert.ToInt32(dataRow[6]),
                                                isImamatAppointee = false,
                                                position = dataRow[3] == DBNull.Value? 0 :
                                                    Convert.ToInt32(dataRow[3]) == 0 ? 0 : positionMappings.ContainsKey(Convert.ToInt32(dataRow[3])) ?
                                                    positionMappings[Convert.ToInt32(dataRow[3])] : 0,
                                                priority = dataRow[8] == DBNull.Value ? 0 : Convert.ToInt32(dataRow[8]),
                                                voluntaryCommunityId = dataRow[1] as string
                                            });
                                        }
                                    }

                                    if (services.Count > 0)
                                    {
                                        writer.WriteLine(
                                            $"{start}{JsonConvert.SerializeObject(services, Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore})}{end}{personId};");
                                    }
                                }
                            }
                        }

                        writer.Close();
                    }
                }
            }

            //using (MySqlConnection conn = new MySqlConnection(connectionString))
            //{
            //    const string sql = "SELECT * FROM person";
            //    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            //    {
            //        cmd.CommandTimeout = 5000;
            //        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
            //        {
            //            DataTable resultTable = new DataTable();
            //            adapter.Fill(resultTable);
            //            StreamWriter writer = new StreamWriter("Queries.sql", false);
            //            //writer.WriteLine("UPDATE country SET country.id = 10000 + country.id;");
            //            //writer.WriteLine("UPDATE area_of_study SET area_of_study.id = 10000 + area_of_study.id;");
            //            //writer.WriteLine("UPDATE business_nature SET business_nature.id = 10000 + business_nature.id;");
            //            //writer.WriteLine("UPDATE business_type SET business_type.id = 10000 + business_type.id;");
            //            //writer.WriteLine("UPDATE educational_degree SET educational_degree.id = 10000 + educational_degree.id;");
            //            //writer.WriteLine("UPDATE educational_institution SET educational_institution.id = 10000 + educational_institution.id;");

            //            foreach (DataRow row in resultTable.Rows)
            //            {
            //                //string educations = row["educations"] as string;
            //                //var employments = row["employments"] as string;
            //                //var akdnTrainings = row["akdn_trainings"] as string;
            //                //var professionalTrainings = row["professional_trainings"] as string;
            //                //int id = Convert.ToInt32(row["id"]);

            //                //if (!string.IsNullOrWhiteSpace(akdnTrainings) /*&& akdnTrainings.Contains("countryOfTraining")*/)
            //                //{
            //                //    var akdnTrainingsJson = JArray.Parse(akdnTrainings);
            //                //    foreach (var akdnTraining in akdnTrainingsJson)
            //                //    {
            //                //        //if (Convert.ToInt32(akdnTraining["countryOfTraining"]) != 0)
            //                //        //{
            //                //        //    akdnTraining["countryOfTraining"] =
            //                //        //        10000 + Convert.ToInt32(akdnTraining["countryOfTraining"]);
            //                //        //}
            //                //        if (Convert.ToInt32(akdnTraining["training"]) != 0)
            //                //        {
            //                //            akdnTraining["training"] =
            //                //                10000 + Convert.ToInt32(akdnTraining["training"]);
            //                //        }
            //                //    }

            //                //    akdnTrainings = akdnTrainingsJson.ToString(Formatting.None);

            //                //    var updateSql =
            //                //        $"UPDATE person SET person.akdn_trainings = '{akdnTrainings}' WHERE person.id = {id};";
            //                //    writer.WriteLine(updateSql);
            //                //}

            //                //if (!string.IsNullOrWhiteSpace(educations) && educations.Contains("majorAreaOfStudy"))
            //                //{
            //                //    var educationsJson = JArray.Parse(educations);
            //                //    foreach (var education in educationsJson)
            //                //    {
            //                //        if (Convert.ToInt32(education["majorAreaOfStudy"]) != 0)
            //                //        {
            //                //            education["majorAreaOfStudy"] =
            //                //                10000 + Convert.ToInt32(education["majorAreaOfStudy"]);
            //                //        }
            //                //    }

            //                //    educations = educationsJson.ToString(Formatting.None);

            //                //    var updateSql =
            //                //            $"UPDATE person SET person.educations = '{educations}' WHERE person.id = {id};";
            //                //    writer.WriteLine(updateSql);
            //                //}

            //                //if (!string.IsNullOrWhiteSpace(employments) && employments.Contains("businessNature"))
            //                //{
            //                //    var employmentsJson = JArray.Parse(employments);
            //                //    foreach (var employment in employmentsJson)
            //                //    {
            //                //        if (Convert.ToInt32(employment["businessNature"]) != 0)
            //                //        {
            //                //            employment["businessNature"] =
            //                //                10000 + Convert.ToInt32(employment["businessNature"]);
            //                //        }
            //                //    }

            //                //    employments = employmentsJson.ToString(Formatting.None);

            //                //    var updateSql =
            //                //            $"UPDATE person SET person.employments = '{employments}' WHERE person.id = {id};";
            //                //    writer.WriteLine(updateSql);
            //                //}

            //                //if (!string.IsNullOrWhiteSpace(employments) && employments.Contains("businessType"))
            //                //{
            //                //    var employmentsJson = JArray.Parse(employments);
            //                //    foreach (var employment in employmentsJson)
            //                //    {
            //                //        if (Convert.ToInt32(employment["businessType"]) != 0)
            //                //        {
            //                //            employment["businessType"] =
            //                //                10000 + Convert.ToInt32(employment["businessType"]);
            //                //        }
            //                //    }

            //                //    employments = employmentsJson.ToString(Formatting.None);

            //                //    var updateSql =
            //                //        $"UPDATE person SET person.employments = '{employments}' WHERE person.id = {id};";
            //                //    writer.WriteLine(updateSql);
            //                //}

            //                //if (!string.IsNullOrWhiteSpace(educations) && educations.Contains("countryOfStudy"))
            //                //{
            //                //    var educationsJson = JArray.Parse(educations);
            //                //    foreach (var education in educationsJson)
            //                //    {
            //                //        if (Convert.ToInt32(education["countryOfStudy"]) != 0)
            //                //        {
            //                //            education["countryOfStudy"] =
            //                //                10000 + Convert.ToInt32(education["countryOfStudy"]);
            //                //        }
            //                //    }

            //                //    educations = educationsJson.ToString(Formatting.None);

            //                //    var updateSql =
            //                //            $"UPDATE person SET person.educations = '{educations}' WHERE person.id = {id};";
            //                //    writer.WriteLine(updateSql);
            //                //}

            //                //if (!string.IsNullOrWhiteSpace(akdnTrainings) && akdnTrainings.Contains("countryOfTraining"))
            //                //{
            //                //    var akdnTrainingsJson = JArray.Parse(akdnTrainings);
            //                //    foreach (var akdnTraining in akdnTrainingsJson)
            //                //    {
            //                //        if (Convert.ToInt32(akdnTraining["countryOfTraining"]) != 0)
            //                //        {
            //                //            akdnTraining["countryOfTraining"] =
            //                //                10000 + Convert.ToInt32(akdnTraining["countryOfTraining"]);
            //                //        }
            //                //    }

            //                //    akdnTrainings = akdnTrainingsJson.ToString(Formatting.None);

            //                //    var updateSql =
            //                //        $"UPDATE person SET person.akdn_trainings = '{akdnTrainings}' WHERE person.id = {id};";
            //                //    writer.WriteLine(updateSql);
            //                //}

            //                //if (!string.IsNullOrWhiteSpace(professionalTrainings) && professionalTrainings.Contains("countryOfTraining"))
            //                //{
            //                //    var professionalTrainingsJson = JArray.Parse(professionalTrainings);
            //                //    foreach (var professionalTraining in professionalTrainingsJson)
            //                //    {
            //                //        if (Convert.ToInt32(professionalTraining["countryOfTraining"]) != 0)
            //                //        {
            //                //            professionalTraining["countryOfTraining"] =
            //                //                10000 + Convert.ToInt32(professionalTraining["countryOfTraining"]);
            //                //        }
            //                //    }

            //                //    professionalTrainings = professionalTrainingsJson.ToString(Formatting.None);

            //                //    var updateSql =
            //                //        $"UPDATE person SET person.professional_trainings = '{professionalTrainings}' WHERE person.id = {id};";
            //                //    writer.WriteLine(updateSql);
            //                //}

            //                //if (!string.IsNullOrWhiteSpace(educations) &&
            //                //    (educations.Contains("nameOfDegree") || educations.Contains("institution")))
            //                //{
            //                //    JArray educationsJson = JArray.Parse(educations);
            //                //    foreach (JToken education in educationsJson)
            //                //    {
            //                //        if (Convert.ToInt32(education["nameOfDegree"]) != 0)
            //                //        {
            //                //            education["nameOfDegree"] =
            //                //                10000 + Convert.ToInt32(education["nameOfDegree"]);
            //                //        }

            //                //        if (Convert.ToInt32(education["institution"]) != 0)
            //                //        {
            //                //            education["institution"] =
            //                //                10000 + Convert.ToInt32(education["institution"]);
            //                //        }
            //                //    }

            //                //    educations = educationsJson.ToString(Formatting.None);

            //                //    string updateSql =
            //                //        $"UPDATE person SET person.educations = '{educations}' WHERE person.id = {id};";
            //                //    writer.WriteLine(updateSql);
            //                //}
            //            }

            //            writer.Close();
            //        }
            //    }
            //}
        }

        #endregion Private Methods
    }

    public class Query
    {
        [JsonProperty("institution")]
        public int? Institution { get; set; }

        [JsonProperty("countryOfStudy")]
        public int? CountryOfStudy { get; set; }

        [JsonProperty("nameOfDegree")]
        public int? NameOfDegree { get; set; }

        [JsonProperty("majorAreaOfStudy")]
        public int? MajorAreaOfStudy { get; set; }

        [JsonProperty("fromYear")]
        public int? FromYear { get; set; }

        [JsonProperty("toYear")]
        public int? ToYear { get; set; }

        [JsonProperty("priority")]
        public int? Priority { get; set; }
    }

    public class VoluntaryCommunity
    {
        public string voluntaryCommunityId;
        public int institution;// Id
        public int position;// Id
        public int fromYear;
        public int toYear;
        public int priority;
        public bool isImamatAppointee;
        public int? cycleId;
        public int? seatNo;
    }
}
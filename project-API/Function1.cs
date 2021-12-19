using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using project_API.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace project_API
{
    public class BreweryFunctions
    {
        [FunctionName("GetBreweries")]
        public async Task<IActionResult> GetBreweries(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "v1/breweries")] HttpRequest req,
            ILogger log)
        {
            try
            {
                List<Brewery> breweries = new List<Brewery>();

                string connectionString = Environment.GetEnvironmentVariable("CONN_STRING");

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "SELECT * FROM Breweries;";

                        var reader = await sqlCommand.ExecuteReaderAsync();

                        while (await reader.ReadAsync())
                        {
                            Brewery brewery = new Brewery();

                            brewery.Id = reader["id"].ToString();
                            brewery.Name = reader["name"].ToString();
                            brewery.BreweryType = reader["brewery_type"].ToString();
                            brewery.Street = reader["street"].ToString();
                            brewery.Address2 = reader["address_2"].ToString();
                            brewery.Address3 = reader["address_3"].ToString();
                            brewery.City = reader["city"].ToString();
                            brewery.State = reader["state"].ToString();
                            brewery.CountyProvince = reader["county_province"].ToString();
                            brewery.PostalCode = reader["postal_code"].ToString();
                            brewery.Country = reader["country"].ToString();
                            brewery.Phone = reader["phone"].ToString();
                            brewery.WebsiteUrl = reader["website_url"].ToString();

                            breweries.Add(brewery);
                        }
                    }
                }
                return new OkObjectResult(breweries);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

        [FunctionName("AddBrewery")]
        public async Task<IActionResult> AddBrewery(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/breweries")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var json = await new StreamReader(req.Body).ReadToEndAsync();
                var brewery = JsonConvert.DeserializeObject<Brewery>(json);

                string connectionString = Environment.GetEnvironmentVariable("CONN_STRING");

                string guid = Guid.NewGuid().ToString();
                brewery.Id = guid;

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = "INSERT INTO Breweries VALUES(@id, @name, @brewery_type, @street, @address_2, @address_3, @city, @state, @county_province, @postal_code, @country, @phone, @website_url)";

                        sqlCommand.Parameters.AddWithValue("@id", brewery.Id);
                        sqlCommand.Parameters.AddWithValue("@name", brewery.Name);
                        sqlCommand.Parameters.AddWithValue("@brewery_type", brewery.BreweryType);
                        sqlCommand.Parameters.AddWithValue("@street", brewery.Street);
                        sqlCommand.Parameters.AddWithValue("@address_2", brewery.Address2);
                        sqlCommand.Parameters.AddWithValue("@address_3", brewery.Address3);
                        sqlCommand.Parameters.AddWithValue("@city", brewery.City);
                        sqlCommand.Parameters.AddWithValue("@state", brewery.State);
                        sqlCommand.Parameters.AddWithValue("@county_province", brewery.CountyProvince);
                        sqlCommand.Parameters.AddWithValue("@postal_code", brewery.PostalCode);
                        sqlCommand.Parameters.AddWithValue("@country", brewery.Country);
                        sqlCommand.Parameters.AddWithValue("@phone", brewery.Phone);
                        sqlCommand.Parameters.AddWithValue("@website_url", brewery.WebsiteUrl);

                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }
                return new OkObjectResult(brewery);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }

        [FunctionName("UpdateBrewery")]
        public async Task<IActionResult> UpdateBrewery(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "v1/breweries/{id}")] HttpRequest req,
            string id,
            ILogger log)
        {
            try
            {
                var json = await new StreamReader(req.Body).ReadToEndAsync();
                var brewery = JsonConvert.DeserializeObject<Brewery>(json);

                string connectionString = Environment.GetEnvironmentVariable("CONN_STRING");

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    await sqlConnection.OpenAsync();
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandText = $"UPDATE Breweries SET name = @name, brewery_type = @brewery_type, street = @street, address_2 = @address_2, address_3 = @address_3, city = @city, state = @state, county_province = @county_province, postal_code = @postal_code, country = @country, phone = @phone, website_url = @website_url WHERE id = '{id}'";
                        sqlCommand.Parameters.AddWithValue("@name", brewery.Name);
                        sqlCommand.Parameters.AddWithValue("@brewery_type", brewery.BreweryType);
                        sqlCommand.Parameters.AddWithValue("@street", brewery.Street);
                        sqlCommand.Parameters.AddWithValue("@address_2", brewery.Address2);
                        sqlCommand.Parameters.AddWithValue("@address_3", brewery.Address3);
                        sqlCommand.Parameters.AddWithValue("@city", brewery.City);
                        sqlCommand.Parameters.AddWithValue("@state", brewery.State);
                        sqlCommand.Parameters.AddWithValue("@county_province", brewery.CountyProvince);
                        sqlCommand.Parameters.AddWithValue("@postal_code", brewery.PostalCode);
                        sqlCommand.Parameters.AddWithValue("@country", brewery.Country);
                        sqlCommand.Parameters.AddWithValue("@phone", brewery.Phone);
                        sqlCommand.Parameters.AddWithValue("@website_url", brewery.WebsiteUrl);

                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }

                return new OkObjectResult(brewery);
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                return new StatusCodeResult(500);
            }
        }
    }
}

using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using StartupInvestorMatcher.Model.Entities;

namespace StartupInvestorMatcher.Model.Repositories
{
    public class CountryRepository : BaseRepository
    {
        public CountryRepository(string connectionString) : base(connectionString)
        {
        }

        public List<Country> GetCountries()
        {
            var countries = new List<Country>();
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM country";

            var data = GetData(dbConn, cmd);
            while (data.Read())
            {
                countries.Add(new Country
                {
                    CountryId = Convert.ToInt32(data["country_id"]),
                    CountryName = data["country_name"].ToString()
                });
            }

            return countries;
        }

        public Country GetCountryById(int id)
        {
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    dbConn.Open();
                    var cmd = dbConn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM country WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

                    var data = GetData(dbConn, cmd);
                    if (data != null && data.Read())
                    {
                        return new Country
                        {
                            CountryId = Convert.ToInt32(data["id"]),
                            CountryName = data["name"].ToString()
                        };
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching country by ID", ex);
                }
            }
        }
    }
}

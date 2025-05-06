using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using StartupInvestorMatcher.Model.Entities;
using System;
using System.Collections.Generic;

namespace StartupInvestorMatcher.Model.Repositories
{
    public class CountryRepository : BaseRepository
    {
        public CountryRepository(IConfiguration configuration) : base(configuration) { }

        public List<Country> GetCountries()
        {
            var countries = new List<Country>();
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM country";

            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read())
                {
                    countries.Add(new Country
                    {
                        CountryId = Convert.ToInt32(data["country_id"]),
                        CountryName = data["country_name"].ToString()
                    });
                }
            }
            return countries;
        }

        public Country GetCountryById(int id)
        {
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM country WHERE country_id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

            var data = GetData(dbConn, cmd);
            if (data != null && data.Read())
            {
                return new Country
                {
                    CountryId = Convert.ToInt32(data["country_id"]),
                    CountryName = data["country_name"].ToString()
                };
            }
            return null;
        }
    }
}

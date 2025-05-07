using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using StartupInvestorMatcher.Model.Entities;

namespace StartupInvestorMatcher.Model.Repositories
{
    public class StartupRepository : BaseRepository
    {
        public StartupRepository(string connectionString) : base(connectionString)
        {
        }

        // Get a specific Startup by MemberId
        public Startup GetStartupById(int memberId)
        {
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM startup WHERE member_id = @member_id";
            cmd.Parameters.Add("@member_id", NpgsqlDbType.Integer).Value = memberId;

            var data = GetData(dbConn, cmd);
            if (data != null && data.Read())
            {
                return new Startup(Convert.ToInt32(data["member_id"]))
                {
                    NameStartup = data["name_startup"].ToString(),
                    OverviewStartup = data["overview_startup"].ToString(),
                    CountryId = Convert.ToInt32(data["country_id"]),
                    IndustryId = Convert.ToInt32(data["industry_id"]),
                    InvestmentSizeId = Convert.ToInt32(data["investment_size_id"])
                };
            }

            return null;
        }

        // Get all Startups
        public List<Startup> GetStartups()
        {
            var startups = new List<Startup>();
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM startup";

            var data = GetData(dbConn, cmd);
            while (data.Read())
            {
                startups.Add(new Startup(Convert.ToInt32(data["member_id"]))
                {
                    NameStartup = data["name_startup"].ToString(),
                    OverviewStartup = data["overview_startup"].ToString(),
                    CountryId = Convert.ToInt32(data["country_id"]),
                    IndustryId = Convert.ToInt32(data["industry_id"]),
                    InvestmentSizeId = Convert.ToInt32(data["investment_size_id"])
                });
            }

            return startups;
        }

        // Insert a new Startup
        public bool InsertStartup(Startup s)
        {
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO startup (member_id, name_startup, overview_startup, country_id, industry_id, investment_size_id)
                VALUES (@member_id, @name_startup, @overview_startup, @country_id, @industry_id, @investment_size_id)
            ";

            cmd.Parameters.AddWithValue("@member_id", NpgsqlDbType.Integer, s.MemberId);
            cmd.Parameters.AddWithValue("@name_startup", NpgsqlDbType.Text, s.NameStartup);
            cmd.Parameters.AddWithValue("@overview_startup", NpgsqlDbType.Text, s.OverviewStartup);
            cmd.Parameters.AddWithValue("@country_id", NpgsqlDbType.Integer, s.CountryId);
            cmd.Parameters.AddWithValue("@industry_id", NpgsqlDbType.Integer, s.IndustryId);
            cmd.Parameters.AddWithValue("@investment_size_id", NpgsqlDbType.Integer, s.InvestmentSizeId);

            return InsertData(dbConn, cmd);
        }

        // Update a Startup
        public bool UpdateStartup(Startup s)
        {
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                UPDATE startup SET
                    name_startup = @name_startup,
                    overview_startup = @overview_startup,
                    country_id = @country_id,
                    industry_id = @industry_id,
                    investment_size_id = @investment_size_id
                WHERE member_id = @member_id
            ";

            cmd.Parameters.AddWithValue("@name_startup", NpgsqlDbType.Text, s.NameStartup);
            cmd.Parameters.AddWithValue("@overview_startup", NpgsqlDbType.Text, s.OverviewStartup);
            cmd.Parameters.AddWithValue("@country_id", NpgsqlDbType.Integer, s.CountryId);
            cmd.Parameters.AddWithValue("@industry_id", NpgsqlDbType.Integer, s.IndustryId);
            cmd.Parameters.AddWithValue("@investment_size_id", NpgsqlDbType.Integer, s.InvestmentSizeId);
            cmd.Parameters.AddWithValue("@member_id", NpgsqlDbType.Integer, s.MemberId);

            return UpdateData(dbConn, cmd);
        }

        // Delete a Startup by MemberId
        public bool DeleteStartup(int memberId)
        {
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "DELETE FROM startup WHERE member_id = @member_id";
            cmd.Parameters.AddWithValue("@member_id", NpgsqlDbType.Integer, memberId);

            return DeleteData(dbConn, cmd);
        }
    }
}

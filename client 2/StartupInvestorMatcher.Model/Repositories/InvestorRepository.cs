using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using StartupInvestorMatcher.Model.Entities;

namespace StartupInvestorMatcher.Model.Repositories
{
    public class InvestorRepository : BaseRepository
    {
        public InvestorRepository(IConfiguration configuration) : base(configuration)
        {
        }

        // Get Investor by Member ID
        public Investor GetInvestorById(int id)
        {
            NpgsqlConnection dbConn = null;
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM investor WHERE member_id = @id";
                cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

                var data = GetData(dbConn, cmd);
                if (data != null && data.Read())
                {
                    return new Investor(Convert.ToInt32(data["member_id"]))
                    {
                        Name_Investor = data["name_investor"].ToString(),
                        Overview_Investor = data["overview_investor"].ToString(),
                        Country_Id = Convert.ToInt32(data["country_id"]),
                        Industry_Id = Convert.ToInt32(data["industry_id"]),
                        Investment_Size_Id = Convert.ToInt32(data["investment_size_id"])
                    };
                }

                return null;
            }
            finally
            {
                dbConn?.Close();
            }
        }

        // Get all Investors
        public List<Investor> GetInvestors()
        {
            NpgsqlConnection dbConn = null;
            var investors = new List<Investor>();
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM investor";

                var data = GetData(dbConn, cmd);
                while (data != null && data.Read())
                {
                    Investor inv = new Investor(Convert.ToInt32(data["member_id"]))
                    {
                        Name_Investor = data["name_investor"].ToString(),
                        Overview_Investor = data["overview_investor"].ToString(),
                        Country_Id = Convert.ToInt32(data["country_id"]),
                        Industry_Id = Convert.ToInt32(data["industry_id"]),
                        Investment_Size_Id = Convert.ToInt32(data["investment_size_id"])
                    };
                    investors.Add(inv);
                }

                return investors;
            }
            finally
            {
                dbConn?.Close();
            }
        }

        // Insert new Investor
        public bool InsertInvestor(Investor inv)
        {
            NpgsqlConnection dbConn = null;
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO investor (member_id, name_investor, overview_investor, country_id, industry_id, investment_size_id)
                    VALUES (@member_id, @name_investor, @overview_investor, @country_id, @industry_id, @investment_size_id)
                ";

                cmd.Parameters.AddWithValue("@member_id", NpgsqlDbType.Integer, inv.MemberId);
                cmd.Parameters.AddWithValue("@name_investor", NpgsqlDbType.Text, inv.Name_Investor);
                cmd.Parameters.AddWithValue("@overview_investor", NpgsqlDbType.Text, inv.Overview_Investor);
                cmd.Parameters.AddWithValue("@country_id", NpgsqlDbType.Integer, inv.Country_Id);
                cmd.Parameters.AddWithValue("@industry_id", NpgsqlDbType.Integer, inv.Industry_Id);
                cmd.Parameters.AddWithValue("@investment_size_id", NpgsqlDbType.Integer, inv.Investment_Size_Id);

                return InsertData(dbConn, cmd);
            }
            finally
            {
                dbConn?.Close();
            }
        }

        // Update existing Investor
        public bool UpdateInvestor(Investor inv)
        {
            var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                UPDATE investor SET
                    name_investor = @name_investor,
                    overview_investor = @overview_investor,
                    country_id = @country_id,
                    industry_id = @industry_id,
                    investment_size_id = @investment_size_id
                WHERE member_id = @member_id
            ";

            cmd.Parameters.AddWithValue("@name_investor", NpgsqlDbType.Text, inv.Name_Investor);
            cmd.Parameters.AddWithValue("@overview_investor", NpgsqlDbType.Text, inv.Overview_Investor);
            cmd.Parameters.AddWithValue("@country_id", NpgsqlDbType.Integer, inv.Country_Id);
            cmd.Parameters.AddWithValue("@industry_id", NpgsqlDbType.Integer, inv.Industry_Id);
            cmd.Parameters.AddWithValue("@investment_size_id", NpgsqlDbType.Integer, inv.Investment_Size_Id);
            cmd.Parameters.AddWithValue("@member_id", NpgsqlDbType.Integer, inv.MemberId);

            return UpdateData(dbConn, cmd);
        }

        // Delete Investor by Member ID
        public bool DeleteInvestor(int id)
        {
            var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "DELETE FROM investor WHERE member_id = @member_id";
            cmd.Parameters.AddWithValue("@member_id", NpgsqlDbType.Integer, id);

            bool result = DeleteData(dbConn, cmd);

            return result;
        }
    }
}

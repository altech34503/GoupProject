using System;
using System.Collections.Generic;
using Npgsql;
using NpgsqlTypes;
using StartupInvestorMatcher.Model.Entities;

namespace StartupInvestorMatcher.Model.Repositories
{
    public class InvestorRepository : BaseRepository
    {
        public InvestorRepository(string connectionString) : base(connectionString)
        {
        }

        public Investor GetInvestorById(int id)
        {
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    var cmd = dbConn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM investor WHERE member_id = @id";
                    cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

                    var data = GetData(dbConn, cmd);
                    if (data != null && data.Read())
                    {
                        return new Investor
                        {
                            MemberId = Convert.ToInt32(data["member_id"]),
                            NameInvestor = data["name_investor"].ToString(),
                            OverviewInvestor = data["overview_investor"].ToString(),
                            CountryId = Convert.ToInt32(data["country_id"]),
                            IndustryId = Convert.ToInt32(data["industry_id"]),
                            InvestmentSizeId = Convert.ToInt32(data["investment_size_id"])
                        };
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching investor by ID", ex);
                }
            }
        }

        public List<Investor> GetInvestors()
        {
            var investors = new List<Investor>();
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    var cmd = dbConn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM investor";

                    var data = GetData(dbConn, cmd);
                    while (data != null && data.Read())
                    {
                        investors.Add(new Investor
                        {
                            MemberId = Convert.ToInt32(data["member_id"]),
                            NameInvestor = data["name_investor"].ToString(),
                            OverviewInvestor = data["overview_investor"].ToString(),
                            CountryId = Convert.ToInt32(data["country_id"]),
                            IndustryId = Convert.ToInt32(data["industry_id"]),
                            InvestmentSizeId = Convert.ToInt32(data["investment_size_id"])
                        });
                    }

                    return investors;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching all investors", ex);
                }
            }
        }

        public bool InsertInvestor(Investor investor)
        {
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    dbConn.Open();
                    var cmd = dbConn.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO investor (member_id, name_investor, overview_investor, country_id, industry_id, investment_size_id)
                        VALUES (@member_id, @name_investor, @overview_investor, @country_id, @industry_id, @investment_size_id)";
                    cmd.Parameters.AddWithValue("@member_id", NpgsqlDbType.Integer, investor.MemberId);
                    cmd.Parameters.AddWithValue("@name_investor", NpgsqlDbType.Text, investor.NameInvestor);
                    cmd.Parameters.AddWithValue("@overview_investor", NpgsqlDbType.Text, investor.OverviewInvestor);
                    cmd.Parameters.AddWithValue("@country_id", NpgsqlDbType.Integer, investor.CountryId);
                    cmd.Parameters.AddWithValue("@industry_id", NpgsqlDbType.Integer, investor.IndustryId);
                    cmd.Parameters.AddWithValue("@investment_size_id", NpgsqlDbType.Integer, investor.InvestmentSizeId);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inserting investor: {ex.Message}");
                    return false;
                }
            }
        }

        public bool UpdateInvestor(Investor investor)
        {
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    dbConn.Open();
                    var cmd = dbConn.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE investor
                        SET name_investor = @name_investor,
                            overview_investor = @overview_investor,
                            country_id = @country_id,
                            industry_id = @industry_id,
                            investment_size_id = @investment_size_id
                        WHERE member_id = @member_id";
                    cmd.Parameters.AddWithValue("@name_investor", NpgsqlDbType.Text, investor.NameInvestor);
                    cmd.Parameters.AddWithValue("@overview_investor", NpgsqlDbType.Text, investor.OverviewInvestor);
                    cmd.Parameters.AddWithValue("@country_id", NpgsqlDbType.Integer, investor.CountryId);
                    cmd.Parameters.AddWithValue("@industry_id", NpgsqlDbType.Integer, investor.IndustryId);
                    cmd.Parameters.AddWithValue("@investment_size_id", NpgsqlDbType.Integer, investor.InvestmentSizeId);
                    cmd.Parameters.AddWithValue("@member_id", NpgsqlDbType.Integer, investor.MemberId);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating investor: {ex.Message}");
                    return false;
                }
            }
        }

        public bool DeleteInvestor(int id)
        {
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    dbConn.Open();
                    var cmd = dbConn.CreateCommand();
                    cmd.CommandText = "DELETE FROM investor WHERE member_id = @id";
                    cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting investor: {ex.Message}");
                    return false;
                }
            }
        }
    }
}

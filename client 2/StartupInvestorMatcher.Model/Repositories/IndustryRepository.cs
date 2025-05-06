using Npgsql;
using NpgsqlTypes;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using StartupInvestorMatcher.Model.Entities;

namespace StartupInvestorMatcher.Model.Repositories
{
    public class IndustryRepository : BaseRepository
    {
        public IndustryRepository(IConfiguration configuration) : base(configuration) { }

        public List<Industry> GetIndustries()
        {
            var industries = new List<Industry>();
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM industry";

            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read())
                {
                    industries.Add(new Industry
                    {
                        IndustryId = (int)data["industry_id"],
                        IndustryName = data["industry_name"].ToString()
                    });
                }
            }
            return industries;
        }

        public Industry GetIndustryById(int id)
        {
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM industry WHERE industry_id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

            var data = GetData(dbConn, cmd);
            if (data != null && data.Read())
            {
                return new Industry
                {
                    IndustryId = (int)data["industry_id"],
                    IndustryName = data["industry_name"].ToString()
                };
            }
            return null;
        }
    }
}

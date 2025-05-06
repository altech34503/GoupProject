using Npgsql;
using NpgsqlTypes;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using StartupInvestorMatcher.Model.Entities;

namespace StartupInvestorMatcher.Model.Repositories
{
    public class InvestmentSizeRepository : BaseRepository
    {
        public InvestmentSizeRepository(IConfiguration configuration) : base(configuration) { }

        public List<InvestmentSize> GetInvestmentSizes()
        {
            var sizes = new List<InvestmentSize>();
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM investment_size";

            var data = GetData(dbConn, cmd);
            if (data != null)
            {
                while (data.Read())
                {
                    sizes.Add(new InvestmentSize
                    {
                        InvestmentSizeId = (int)data["investment_size_id"],
                        InvestmentSizeName = data["investment_size_name"].ToString()
                    });
                }
            }
            return sizes;
        }

        public InvestmentSize GetInvestmentSizeById(int id)
        {
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM investment_size WHERE investment_size_id = @id";
            cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

            var data = GetData(dbConn, cmd);
            if (data != null && data.Read())
            {
                return new InvestmentSize
                {
                    InvestmentSizeId = (int)data["investment_size_id"],
                    InvestmentSizeName = data["investment_size_name"].ToString()
                };
            }
            return null;
        }
    }
}

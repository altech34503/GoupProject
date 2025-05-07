using Npgsql;
using NpgsqlTypes;
using System.Collections.Generic;
using StartupInvestorMatcher.Model.Entities;

namespace StartupInvestorMatcher.Model.Repositories
{
    public class InvestmentSizeRepository : BaseRepository
    {
        public InvestmentSizeRepository(string connectionString) : base(connectionString)
        {
        }

        public List<InvestmentSize> GetInvestmentSizes()
        {
            var sizes = new List<InvestmentSize>();
            using var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = "SELECT * FROM investment_size";

            var data = GetData(dbConn, cmd);
            while (data.Read())
            {
                sizes.Add(new InvestmentSize
                {
                    InvestmentSizeId = Convert.ToInt32(data["investment_size_id"]),
                    InvestmentSizeName = data["investment_size_name"].ToString()
                });
            }

            return sizes;
        }

        public InvestmentSize GetInvestmentSizeById(int id)
        {
            using (var dbConn = new NpgsqlConnection(ConnectionString))
            {
                try
                {
                    dbConn.Open();
                    var cmd = dbConn.CreateCommand();
                    cmd.CommandText = "SELECT * FROM investment_size WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", NpgsqlDbType.Integer, id);

                    var data = GetData(dbConn, cmd);
                    if (data != null && data.Read())
                    {
                        return new InvestmentSize
                        {
                            InvestmentSizeId = Convert.ToInt32(data["id"]),
                            InvestmentSizeName = data["size"].ToString()
                        };
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching investment size by ID", ex);
                }
            }
        }
    }
}

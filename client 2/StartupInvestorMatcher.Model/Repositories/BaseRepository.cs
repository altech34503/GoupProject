using System;
using System.Data;
using Npgsql;

namespace StartupInvestorMatcher.Model.Repositories
{
    public class BaseRepository
    {
        protected string ConnectionString;

        public BaseRepository(string connectionString)
        {
            ConnectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        protected IDataReader GetData(NpgsqlConnection conn, NpgsqlCommand cmd)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            return cmd.ExecuteReader();
        }

        protected bool InsertData(NpgsqlConnection conn, NpgsqlCommand cmd)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            return cmd.ExecuteNonQuery() > 0;
        }

        protected bool UpdateData(NpgsqlConnection conn, NpgsqlCommand cmd)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            return cmd.ExecuteNonQuery() > 0;
        }

        protected bool DeleteData(NpgsqlConnection conn, NpgsqlCommand cmd)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd.Connection = conn;
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}

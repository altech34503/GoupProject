

namespace StartupInvestorMatcher.Model.Repositories;



using System;
using Microsoft.Extensions.Configuration;
using Npgsql;
using NpgsqlTypes;
using global::StartupInvestorMatcher.Model.Entities;



    public class MemberRepository : BaseRepository
    {
        public MemberRepository(IConfiguration configuration) : base(configuration)
        {
        }

        // Get Member by ID
        public Member GetMemberById(int id)
        {
            NpgsqlConnection dbConn = null;
            try
            {
                // Create a new connection for database
                dbConn = new NpgsqlConnection(ConnectionString);
                // Creating an SQL command
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM member WHERE member_id = @id";
                cmd.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;
                // Call the base method to get data
                var data = GetData(dbConn, cmd);
                if (data != null)
                {
                    if (data.Read()) // Every time loop runs, it reads next row
                    {
                        return new Member(Convert.ToInt32(data["member_id"]))
                        {
                            MemberEmail = data["member_email"].ToString(),
                            MemberType = data["member_type"].ToString(),
                            MemberAddress = data["member_address"].ToString(),
                            MemberPhone = data["member_phone"].ToString()
                        };
                    }
                }
                return null;
            }
            finally
            {
                dbConn?.Close();
            }
        }

        // Get all Members
        public List<Member> GetMembers()
        {
            NpgsqlConnection dbConn = null;
            var members = new List<Member>();
            try
            {
                // Create a new connection for database
                dbConn = new NpgsqlConnection(ConnectionString);
                // Creating an SQL command
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = "SELECT * FROM member";
                // Call the base method to get data
                var data = GetData(dbConn, cmd);
                if (data != null)
                {
                    while (data.Read()) // Every time loop runs, it reads the next row
                    {
                        Member m = new Member(Convert.ToInt32(data["member_id"]))
                        {
                            MemberEmail = data["member_email"].ToString(),
                            MemberType = data["member_type"].ToString(),
                            MemberAddress = data["member_address"].ToString(),
                            MemberPhone = data["member_phone"].ToString()
                        };
                        members.Add(m);
                    }
                }
                return members;
            }
            finally
            {
                dbConn?.Close();
            }
        }

        // Add a new Member
        public bool InsertMember(Member m)
        {
            NpgsqlConnection dbConn = null;
            try
            {
                dbConn = new NpgsqlConnection(ConnectionString);
                var cmd = dbConn.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO member (member_email, member_type, member_address, member_phone)
                    VALUES (@member_email, @member_type, @member_address, @member_phone)
                ";

                // Adding parameters in a better way
                cmd.Parameters.AddWithValue("@member_email", NpgsqlDbType.Text, m.MemberEmail);
                cmd.Parameters.AddWithValue("@member_type", NpgsqlDbType.Text, m.MemberType);
                cmd.Parameters.AddWithValue("@member_address", NpgsqlDbType.Text, m.MemberAddress);
                cmd.Parameters.AddWithValue("@member_phone", NpgsqlDbType.Text, m.MemberPhone);
                
                // Will return true if all goes well
                bool result = InsertData(dbConn, cmd);
                return result;
            }
            finally
            {
                dbConn?.Close();
            }
        }

        // Update a Member
        public bool UpdateMember(Member m)
        {
            var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                UPDATE member SET
                    member_email = @member_email,
                    member_type = @member_type,
                    member_address = @member_address,
                    member_phone = @member_phone
                WHERE member_id = @member_id
            ";

            // Adding parameters
            cmd.Parameters.AddWithValue("@member_email", NpgsqlDbType.Text, m.MemberEmail);
            cmd.Parameters.AddWithValue("@member_type", NpgsqlDbType.Text, m.MemberType);
            cmd.Parameters.AddWithValue("@member_address", NpgsqlDbType.Text, m.MemberAddress);
            cmd.Parameters.AddWithValue("@member_phone", NpgsqlDbType.Text, m.MemberPhone);
            cmd.Parameters.AddWithValue("@member_id", NpgsqlDbType.Integer, m.MemberId);
            
            // Will return true if all goes well
            bool result = UpdateData(dbConn, cmd);
            return result;
        }

        // Delete a Member by ID
        public bool DeleteMember(int id)
        {
            var dbConn = new NpgsqlConnection(ConnectionString);
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = @"
                DELETE FROM member
                WHERE member_id = @member_id
            ";

            // Adding parameters
            cmd.Parameters.AddWithValue("@member_id", NpgsqlDbType.Integer, id);
            
            // Will return true if all goes well
            bool result = DeleteData(dbConn, cmd);
            return result;
        }
    }

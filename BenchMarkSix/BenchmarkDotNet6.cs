using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace BenchMarkSix
{
    public class BenchmarkDotNet6
    {
        private readonly string _connectionString;

        public BenchmarkDotNet6(string connectionString)
        { 
            _connectionString = connectionString;
        }
        public void InsertUser(IEnumerable<UsersModel> users)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                foreach (var user in users)
                {
                    var command = new SqlCommand(
                        "INSERT INTO Users(FirstName, LastName, Age) VALUES (@FirstName, @LastName, @Age)",
                        connection, transaction);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Age", user.Age);
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        public List<UsersModel> GetAllUser()
        {
            var users = new List<UsersModel>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var command = new SqlCommand("SELECT PersonID, FirstName, LastName, Age FROM Users", connection);

            using var reader = command.ExecuteReader();
            while (reader.Read()) 
            {
                users.Add(new UsersModel
                {
                    PersonID = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Age = reader.GetInt32(3)
                });
            }
            return users;
        }
    }
}

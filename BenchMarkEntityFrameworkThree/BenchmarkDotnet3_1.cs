using BenchmarkDotNet.Attributes;
using System.Data.SqlClient;


namespace BenchMarkThree
{
    public class BenchmarkDotnet3_1
    {
        private readonly string _connectionString;

        public BenchmarkDotnet3_1(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertProduct(IEnumerable<UserModel> users)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction()) 
                {
                    try
                    {
                        foreach (var user in users)
                        {
                            var command = new SqlCommand(
                                "INSERT INTO Users (FirstName, LastName, Age) VALUES (@FirstName, @LastName, @Age)", connection, transaction);
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
            }
        }
        public IEnumerable<UserModel> GetUsers()
        {
            var users = new List<UserModel>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT PersonID, FirstName, LastName, Age FROM Users", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new UserModel
                        {
                            PersonID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Age = reader.GetInt32(3)
                        });
                    }
                }
            }
            return users;
        }
        public void TruncateUsersTable()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("TRUNCATE TABLE Users", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}

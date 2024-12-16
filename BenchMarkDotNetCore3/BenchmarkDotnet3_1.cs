using BenchmarkDotNet.Attributes;
using System.Data.SqlClient;


namespace BenchMarkDotNetCore3
{
    public class BenchmarkDotnet3_1
    {
        private const string ConnString = "Server=localhost,1433;Initial Catalog=benchmarkDB;User ID=sa;Password=pa55w0rd!;Encrypt=True;TrustServerCertificate=True;";
       
        [Benchmark]
        public void AdoNetQuery()
        {
            using (var conn = new SqlConnection(ConnString))
            {
                conn.Open();
                string query = "SELECT * FROM Persons";
                using (var command = new SqlCommand(query, conn))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var lastName = reader["LastName"];
                            var firstName = reader["FirstName"];
                            var address = reader["Address"];
                            var city = reader["City"];
                        }
                    }
                }
            }
        }
    }
}

using BenchmarkDotNet.Attributes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchMarkSix
{
    [MemoryDiagnoser]
    public class BenchmarkTests
    {
        private BenchmarkDotNet6 _adoNet;
        private BenchMarkEntityFrameworkSix _entityFrameworkSix;

        private readonly string _connectionString = "Server=localhost,1433;Initial Catalog=benchmarkDB;User ID=sa;Password=pa55w0rd!;Encrypt=True;TrustServerCertificate=True;";

        [GlobalSetup]
        public void Setup() 
        { 
            //EF CORE
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_connectionString)
                .Options;
            var context = new ApplicationDbContext(options);
            
            _entityFrameworkSix = new BenchMarkEntityFrameworkSix(context);

            //ADO.NET
            _adoNet = new BenchmarkDotNet6(_connectionString);


            /*using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var cleanupCommand = new SqlCommand("DELETE FROM Users", connection);
            cleanupCommand.ExecuteNonQuery();*/
        }

        [Benchmark]
        public void EFCoreInsert()
        {
            var users = new List<UsersModel>();
            for (var i = 0; i < 5000; i++)
            {
                users.Add(new UsersModel { FirstName = "EFCore", LastName = "Core", Age = i });
            }
            _entityFrameworkSix.InsertUser(users);
            _entityFrameworkSix.SaveChanges();

        }
        [Benchmark]
        public void ADOInsert()
        {
            var users = new List<UsersModel>();
            for (var i = 0; i < 5000; i++)
            {
                users.Add(new UsersModel { FirstName = "ADONET", LastName = "NET", Age = i });
            }
            _adoNet.InsertUser(users);
        }
        [Benchmark]
        public void EFCoreGetAllUsers()
        {
            _entityFrameworkSix.GetUsers();

        }
        [Benchmark]
        public void ADONetGetAllUsers()
        {
            _adoNet.GetAllUser();
        }
    }
}

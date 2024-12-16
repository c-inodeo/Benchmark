using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchMarkThree
{
    [MemoryDiagnoser]
    public class BenchMarkTests
    {
        private readonly string _connectionString = "Server=localhost,1433;Initial Catalog=benchmarkDB;User ID=sa;Password=pa55w0rd!;Encrypt=True;TrustServerCertificate=True;";
        private BenchmarkDotnet3_1 _adoNet;
        private BenchmarkEntity_Framework_3_1_4 _efCore;

        [GlobalSetup]
        public void Setup() 
        { 
            _adoNet = new BenchmarkDotnet3_1(_connectionString); 

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(_connectionString)
                .Options;
            var context = new ApplicationDbContext(options);

            _efCore = new BenchmarkEntity_Framework_3_1_4(context);
        }
        [Benchmark]
        public void EFCoreInsertProd()
        {
            var users = new List<UserModel>();
            for (int i = 0; i < 5000; i++)
            {
                users.Add(new UserModel { FirstName = "EFCore", LastName = "3.1.4", Age = i });
            }
            _efCore.InsertProduct(users);
            _efCore.SaveChanges();
        }
        [Benchmark]
        public void AdoNetInsertProducts()
        {
            var users = new List<UserModel>();
            for (int i = 0; i < 5000; i++)
            {
                users.Add(new UserModel { FirstName = "ADO", LastName = "3.1", Age = i });
            }
            _adoNet.InsertProduct(users);
        }
        [Benchmark]
        public void EFCoreGetAllUsers()
        {
            _efCore.GetAllUsers();

        }
        [Benchmark]
        public void ADONetGetAllUsers()
        {
            _adoNet.GetUsers();
        }
        /*
        [GlobalCleanup]
        public void Cleanup() 
        {
            _adoNet.TruncateUsersTable();
            _efCore.TruncateUsersTableEfCore();
        }*/
    }
}

using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchMarkThree
{ 
    public class BenchmarkEntity_Framework_3_1_4
    {
        private readonly ApplicationDbContext _context;

        public BenchmarkEntity_Framework_3_1_4(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertProduct(IEnumerable<UserModel> users)
        {
            _context.Users.AddRange(users);
        }
        public List<UserModel> GetAllUsers() 
        {
            return _context.Users.OrderBy(u => u.PersonID).ToList();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void TruncateUsersTableEfCore()
        {
            _context.Database.ExecuteSqlRaw("TRUNCATE TABLE Users");
        }
    }
}
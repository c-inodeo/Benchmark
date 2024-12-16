using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchMarkSix
{
    public class BenchMarkEntityFrameworkSix
    {
        private readonly ApplicationDbContext _context;

        public BenchMarkEntityFrameworkSix(ApplicationDbContext context)
        {
            _context = context;
        }

        public void InsertUser(IEnumerable<UsersModel> users)
        {
            _context.Users.AddRange(users);
        }

        public List<UsersModel> GetUsers() 
        { 
            return _context.Users.ToList();
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

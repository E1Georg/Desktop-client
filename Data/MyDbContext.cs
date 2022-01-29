using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Forms
{
    class MyDbContext : DbContext
    {
        public MyDbContext() : base("DbConnection")
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<UserException> UserExceptions { get; set; }
    }
}

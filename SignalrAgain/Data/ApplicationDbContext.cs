using Microsoft.EntityFrameworkCore;
using SignalrAgain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrAgain.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> UserDbs  { get; set; }

        public DbSet<Messages> MessagesDbs { get; set; }
    }
}

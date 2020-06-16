using NerdDinner.Models;
using Microsoft.EntityFrameworkCore;

namespace NerdDinner.Data
{
    public class DinnerContext : DbContext
    {
        public DinnerContext(DbContextOptions<DinnerContext> options)
            : base(options)
        {
        }
        public DbSet<Dinner> Dinner { get; set; }
        public DbSet<Rsvp> Rsvp { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Register> Register { get; set; }
    }
}
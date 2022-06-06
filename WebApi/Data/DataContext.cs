using WebApi;
using Microsoft.EntityFrameworkCore;
using WebApiJwtToken;

namespace WebApi.Data 
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<User> User {get; set;}
    }
}
using EFDatabaseLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EFDatabaseLibrary.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Contact> Contact { get; set; }
        public DbSet<Picture> Picture { get; set; }
    }
}

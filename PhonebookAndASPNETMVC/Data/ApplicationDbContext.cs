using Microsoft.EntityFrameworkCore;
using PhonebookAndASPNETMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookAndASPNETMVC.Data
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

using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Misc;
using Umlaut.Database.Models.PostgresModels;

namespace Umlaut.Database

{
    public class UmlautDBContext: DbContext
    {

        public UmlautDBContext(DbContextOptions<UmlautDBContext> options) : base(options) { }

        public DbSet<Graduate> Graduates { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Graduate>().HasIndex(u => u.ResumeLink).IsUnique();
            modelBuilder.Entity<Location>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<Specialization>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<Faculty>().HasIndex(u => u.Name).IsUnique();
        }
    }
}
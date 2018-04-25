using Isen.DotNet.Library.Models.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Isen.DotNet.Library.Data
{
    public class ApplicationDbContext : DbContext
    {
        // 1 - Préciser les DbSet
        public DbSet<Department> DepartmentCollection { get;set; }
        public DbSet<City> CityCollection { get;set; }
        public DbSet<Address> AddressCollection { get;set; }
        public DbSet<Category> CategoryCollection { get;set; }
        public DbSet<InterestPoint> InterestPointCollection { get;set; }
        public DbSet<Person> PersonCollection { get;set; }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(
            ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // 2 - Configurer les mappings tables / classes
            builder.Entity<Department>()
                .ToTable("Department")
                .HasMany(d => d.CityCollection)
                .WithOne(c => c.Department)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<City>()
                .ToTable("City")
                .HasMany(c => c.PersonCollection)
                .HasOne(d => d.Department)
                .WithOne(p => p.City)
                .HasForeignKey(c => c.DepartmentId);

            builder.Entity<Person>()
                .ToTable("Person")
                .HasOne(p => p.City)
                .WithMany(c => c.PersonCollection)
                .HasForeignKey(p => p.CityId);
                
        }
    }
}
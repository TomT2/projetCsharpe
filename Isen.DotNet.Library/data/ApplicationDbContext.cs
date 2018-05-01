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
            //Department
            builder.Entity<Department>()
                .ToTable("Department")
                .HasMany(d => d.CityCollection)
                .WithOne(c => c.Department)
                .OnDelete(DeleteBehavior.SetNull);

            //City
            builder.Entity<City>()
                .ToTable("City");

            builder.Entity<City>()
                .HasMany(c => c.PersonCollection)
                .WithOne(p => p.City)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<City>()
                .HasOne(c => c.Department)
                .WithMany(d => d.CityCollection) 
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<City>()
                .HasMany(c => c.AddressCollection)
                .WithOne(a => a.City)
                .OnDelete(DeleteBehavior.SetNull);

            //Address
            builder.Entity<Address>()
                .ToTable("Address");

            builder.Entity<Address>()
                .HasOne(a => a.City)
                .WithMany(c => c.AddressCollection) 
                .HasForeignKey(a => a.CityId);

            builder.Entity<Address>()
                .HasMany(a => a.InterestPointCollection)
                .WithOne(i => i.Address)
                .OnDelete(DeleteBehavior.SetNull);
                //.HasForeignKey<InterestPoint>(i => i.AddressId);


            //Category
            builder.Entity<Category>()
                .ToTable("Category")
                .HasMany(c => c.InterestPointCollection)
                .WithOne(i => i.Category)
                .OnDelete(DeleteBehavior.SetNull);

            //Point of interest
            builder.Entity<InterestPoint>()
                .ToTable("InterestPoint");

            builder.Entity<InterestPoint>()
                .HasOne(i => i.Category)
                .WithMany(c => c.InterestPointCollection) 
                .HasForeignKey(i => i.CategoryId);
            
            builder.Entity<InterestPoint>()
                .HasOne(i => i.Address)
                .WithMany(a => a.InterestPointCollection)
                .HasForeignKey(i => i.AddressId);

            //Person
            builder.Entity<Person>()
                .ToTable("Person")
                .HasOne(p => p.City)
                .WithMany(c => c.PersonCollection)
                .HasForeignKey(p => p.CityId);
                
        }
    }
}
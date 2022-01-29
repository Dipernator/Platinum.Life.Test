using Microsoft.AspNet.Identity.EntityFramework;
using Platinum.Life.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platinum.Life.Data
{
    public class DbContext : IdentityDbContext<User>
    {
        //public DbContext() : base("name=eCommerceConnectionString_OK")
        //{
        //    Database.SetInitializer<DbContext>(new eCommerceDBInitializer());
        //}

        public DbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
        }

        public DbSet<BankDetails> BankDetails { get; set; }
        public DbSet<Attachment> Attachement { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<PaymentRequisition> PaymentRequisition { get; set; }
        public DbSet<Signature> Signature { get; set; }
        //public DbSet<AuditLog> AuditLog { get; set; }        
        public static DbContext Create()
        {
            return new DbContext();
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).ModifiedDateTime = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreateDateTime = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }


    }
}


//Enable - Migrations: Enables the migration in your project by creating a Configuration class.
//Add - Migration: Creates a new migration class as per specified name with the Up() and Down() methods.
//Update - Database: Executes the last migration file created by the Add-Migration command and applies changes to the database schema
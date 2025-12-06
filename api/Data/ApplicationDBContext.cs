using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.model;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions)
        : base(dbContextOptions)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();
            builder.Entity<Book>()
                .Property(b => b.ISBN)
                .IsRequired();
            builder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired();
        }
    }
}
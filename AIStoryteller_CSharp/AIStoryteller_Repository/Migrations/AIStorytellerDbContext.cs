using AIStoryteller_Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Migrations
{
    public class AIStorytellerDbContext : DbContext
    {
        public AIStorytellerDbContext() { }
        public AIStorytellerDbContext(DbContextOptions options) : base(options){}
        public DbSet<Book> Books { get; set; }
        public DbSet<Page> Pages { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conn = "Data Source=(local);database=AIStorytellerDB;uid=sa;pwd=1234567890;TrustServerCertificate=True;MultipleActiveResultSets=True";
            optionsBuilder.UseSqlServer();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>()
                .HasMany(e => e.Pages)
                .WithOne(e => e.Book)
                .OnDelete(DeleteBehavior.Cascade);          
        }
    }
}

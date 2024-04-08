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

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBookPurchase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBookPurchase.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book_Writer>().HasKey(bw => new
            {
                bw.BookId,
                bw.WriterId
            });

            modelBuilder.Entity<Publications_Writers>().HasKey(pw => new
            {
                pw.PublicationsId,
                pw.WriterId
            });

            modelBuilder.Entity<Book_Writer>().HasOne(b => b.Book).WithMany(bw => bw.Book_Writer).HasForeignKey(b => b.BookId);
            modelBuilder.Entity<Book_Writer>().HasOne(w => w.Writer).WithMany(bw => bw.Book_Writer).HasForeignKey(w => w.WriterId);


            modelBuilder.Entity<Publications_Writers>().HasOne(p => p.Publications).WithMany(pw => pw.Publications_Writers).HasForeignKey(p => p.PublicationsId);
            modelBuilder.Entity<Publications_Writers>().HasOne(w => w.Writer).WithMany(pw => pw.Publications_Writers).HasForeignKey(w => w.WriterId);

            base.OnModelCreating(modelBuilder);
        }

     

        public DbSet<Book> Books { get; set; }
        public DbSet<Book_Writer> Book_Writers { get; set; }
        public DbSet<Publications> Publications { get; set; }
        public DbSet<Publications_Writers> Publications_Writers { get; set; }
        public DbSet<Writer> Writers { get; set; }

        // Orders related tables

        public DbSet<Order> Order { get; set; }
        public DbSet<OItem> OItem { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}

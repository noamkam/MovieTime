using Microsoft.EntityFrameworkCore;
using MovieTime.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection.Emit;

namespace MovieTime.Context
{
    public class MovieTimeDBContext : DbContext
    {
        public MovieTimeDBContext(DbContextOptions<MovieTimeDBContext> options)
           : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Language)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.LanguageId);
      
            modelBuilder.Entity<Screening>()
                .HasOne(s => s.Movie)
                .WithMany(m => m.Screenings)
                .HasForeignKey(s => s.MovieId);

            modelBuilder.Entity<Screening>()
                .HasOne(s => s.Hall)
                .WithMany(h => h.Screenings)
                .HasForeignKey(s => s.HallId);
           
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Screening)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => t.ScreeningId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany()
                .HasForeignKey(t => t.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Purchases)
                .HasForeignKey(p => p.CustomerId);

            modelBuilder.Entity<Purchase>()
                .HasOne(p => p.Discount)
                .WithMany(d => d.Purchases)
                .HasForeignKey(p => p.DiscountId)
                .IsRequired(false);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Purchase)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PurchaseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Hall)
                .WithMany(h => h.Seats)
                .HasForeignKey(s => s.HallId);
        }
    }
}
﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentingCars.Data.Models;

namespace RentingCars.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Dealer> Dealers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Car>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Cars)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Car>()
                .HasOne(c => c.Dealer)
                .WithMany(d => d.Cars)
                .HasForeignKey(c => c.DealerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder
                .Entity<Dealer>()
                .HasOne(d => d.User)                
                //.HasOne<IdentityUser>()
                .WithOne()
                .HasForeignKey<Dealer>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);                

            base.OnModelCreating(builder);
        }
    }
}
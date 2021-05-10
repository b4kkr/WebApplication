using System;
using Microsoft.EntityFrameworkCore;
using WebApplication.Dto;
using WebApplication.Entities;

namespace WebApplication.Repository
{
    public class CarMechanicContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Repair> Repairs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }

        public CarMechanicContext(DbContextOptions options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Type).WithMany(t => t.Cars)
                .HasForeignKey(c => c.CarTypeId);
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Repair).WithOne(r => r.Car)
                .HasForeignKey<Car>(c => c.RepairId);
            modelBuilder.Entity<Car>()
                .HasOne(c => c.User).WithOne(u => u.Car)
                .HasForeignKey<Car>(c => c.UserId);
            
            modelBuilder.Entity<User>()
                .HasOne(c => c.Car).WithOne(u => u.User)
                .HasForeignKey<User>(u => u.CarId);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Repair).WithOne(r => r.User)
                .HasForeignKey<User>(u => u.RepairId);
            
            modelBuilder.Entity<Repair>()
                .HasOne(r => r.Car).WithOne(c => c.Repair)
                .HasForeignKey<Repair>(r =>r.CarId);
            modelBuilder.Entity<Repair>()
                .HasOne(r => r.User).WithOne(u => u.Repair)
                .HasForeignKey<Repair>(r => r.UserId);
            modelBuilder.Entity<Repair>()
                .HasOne(r => r.Status).WithMany(s => s.Repairs)
                .HasForeignKey(r => r.StatusId);
        }
    }
}
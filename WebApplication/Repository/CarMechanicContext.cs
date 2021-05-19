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
        public DbSet<StatusEntity> StatusEntities { get; set; }

        public CarMechanicContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
            //InitDb();
            //Database.EnsureDeleted();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatusEntity>()
                .Property(r => r.Status).HasConversion(v => v.ToString(),
                    v => Enum.Parse<Status>(v));
            modelBuilder.Entity<StatusEntity>()
                .HasMany<Repair>().WithOne(r => r.StatusEntity);
            modelBuilder.Entity<Repair>()
                .Property(r => r.Guid).HasConversion(v => v.ToString(), v => Guid.Parse(v));
        }

        private void InitDb()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
            StatusEntities.Add(new StatusEntity() {Status = Status.Finished});
            StatusEntities.Add(new StatusEntity() {Status = Status.AddedForService});
            StatusEntities.Add(new StatusEntity() {Status = Status.WorkingOnCarNow});
            StatusEntities.Add(new StatusEntity() {Status = Status.WaitingForParts}); 
            SaveChanges();
            
        }
    }
}
using System;
using Microsoft.EntityFrameworkCore;
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
        
        public CarMechanicContext() {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().Property(x => x.Id).UseHiLo("DBSequenceHiLo");
            modelBuilder.Entity<StatusEntity>().Property(x => x.Id).UseHiLo("DBSequenceHiLo");
            modelBuilder.Entity<Repair>().Property(x => x.Id).UseHiLo("DBSequenceHiLo");
            modelBuilder.Entity<CarType>().Property(x => x.Id).UseHiLo("DBSequenceHiLo");
            modelBuilder.Entity<User>().Property(x => x.Id).UseHiLo("DBSequenceHiLo");
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
            Database.EnsureCreated();
            foreach (var statusEntity in StatusEntities)
            {
                StatusEntities.Remove(statusEntity);
            }
            foreach (var repair in Repairs)
            {
                Repairs.Remove(repair);
            }
            foreach (var carType in CarTypes)
            {
                CarTypes.Remove(carType);
            }
            foreach (var car in Cars)
            {
                Cars.Remove(car);
            }
            foreach (var user in Users)
            {
                Users.Remove(user);
            }
            
            SaveChanges();
            StatusEntities.Add(new StatusEntity() {Status = Status.Finished});
            StatusEntities.Add(new StatusEntity() {Status = Status.AddedForService});
            StatusEntities.Add(new StatusEntity() {Status = Status.WaitingForParts});
            StatusEntities.Add(new StatusEntity() {Status = Status.WorkingOnCarNow});
            SaveChanges();
            
        }
    }
}
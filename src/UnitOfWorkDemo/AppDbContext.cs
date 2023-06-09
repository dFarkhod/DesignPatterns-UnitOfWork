﻿using Microsoft.EntityFrameworkCore;
using UnitOfWorkDemo.Entities;
using UnitOfWorkDemo.Services;

namespace UnitOfWorkDemo
{
    public class AppDbContext : DbContext
    {
        private readonly IDateTimeService _dateTimeService;

        public AppDbContext(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "CompanyDb");
        }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<StaffActionJournal> StaffActionJournal { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasMany(dept => dept.StaffList)
                .WithOne(staff => staff.Department)
                .HasForeignKey(i => i.DepartmentId);

            modelBuilder.Entity<Department>().Navigation(e => e.StaffList).AutoInclude();
        }


        // audit trail design pattern
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = _dateTimeService.NowUtc;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = _dateTimeService.NowUtc;
                        break;
                }
            }
            return await base.SaveChangesAsync();

        }
    }
}

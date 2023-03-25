using Microsoft.EntityFrameworkCore;

namespace UnitOfWorkDemo.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var staff = new Staff
            {
                Id = 1,
                FirstName = "Farkhod",
                LastName = "Dadajanov",
                Email = "dfarkhod@gmail.com",
                Title = "CEO",
                CreatedBy = "Admin",
                LastModifiedBy = "Admin",
                Address = "Planet Earth",
                DateOfBirth = new DateTime(1928, 12, 03),
                CreatedAt = DateTime.UtcNow,
                LastModifiedAt = DateTime.UtcNow
            };

            modelBuilder.Entity<Staff>().HasData(staff);
        }
    }
}

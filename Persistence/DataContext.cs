
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }   

        public DbSet<ActivityAttendee> ActivityAttendees { get; set; } 

        public DbSet<Photo> Photos {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee >(x => 
            x.HasKey( z => new {z.AppUserId, z.ActivityId}));

            builder.Entity<ActivityAttendee>()
            .HasOne(u => u.AppUser)
            .WithMany(t => t.Activities)
            .HasForeignKey(m => m.AppUserId);

             builder.Entity<ActivityAttendee>()
            .HasOne(u => u.Activity)
            .WithMany(t => t.Attendees)
            .HasForeignKey(m => m.ActivityId);
        }

             
    }
}
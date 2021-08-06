
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

        public DbSet<Comment> Comments {get; set;}

        public DbSet<UserFollowing> UserFollowings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee>(x =>
            x.HasKey(z => new { z.AppUserId, z.ActivityId }));

            builder.Entity<ActivityAttendee>()
            .HasOne(u => u.AppUser)
            .WithMany(t => t.Activities)
            .HasForeignKey(m => m.AppUserId);

            builder.Entity<ActivityAttendee>()
           .HasOne(u => u.Activity)
           .WithMany(t => t.Attendees)
           .HasForeignKey(m => m.ActivityId);

            builder.Entity<Comment>()
            .HasOne(x => x.Activiy)
            .WithMany(u => u.Comments)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFollowing>(b => 
            {
                b.HasKey(k => new {k.ObserverId, k.TargetId});

                b.HasOne(u => u.Observer)
                .WithMany(f => f.Followings)
                .HasForeignKey(x => x.ObserverId)
                .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(u => u.Target)
                .WithMany(f => f.Followers)
                .HasForeignKey(x => x.TargetId)
                .OnDelete(DeleteBehavior.Cascade);
            });
            
        }

             
    }
}
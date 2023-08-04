using Microsoft.EntityFrameworkCore;
using Poc_net7.Entities;

namespace Poc_net7.Persistence
{
    public class DevEventsDbContext : DbContext
    {
        public DbSet<DevEvent> DevEvents { get; set; }
        public DbSet<DevEventSpeaker> DevEventSpeakers { get; set; }

        public DevEventsDbContext(DbContextOptions<DevEventsDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevEvent>(x =>
            { 
                x.HasKey(y => y.Id);
                
                x.Property(y => y.Title).IsRequired(false);
                
                x.Property(y =>y.Description)
                    .HasMaxLength(200)
                    .HasColumnType("varchar(200)");
                
                x.Property(y => y.StartDate)
                    .HasColumnName("Start_Date");
                
                x.Property(y => y.EndDate)
                    .HasColumnName("End_Date");

                x.HasMany(y => y.Speakers)
                    .WithOne()
                    .HasForeignKey(z => z.DevEventId);
            });

            modelBuilder.Entity<DevEventSpeaker>(x =>
            {
                x.HasKey(y => y.Id);
            });
        }

    }
}

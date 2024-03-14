using CRUD.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD.API.Persistence

{
    public class DevEventsDbContent : DbContext
    {
        public DevEventsDbContent(DbContextOptions<DevEventsDbContent> options) : base(options)
        {
            
        }

        public DbSet<DevEvent> DevEvents { get; set; }
        public DbSet<DevEventSpeaker> DevEventsSpeaker { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DevEvent>(e =>
            {
                e.HasKey(de => de.Id);

                e.Property(de => de.Title).IsRequired(false);

                e.Property(de => de.Description).HasMaxLength(200).HasColumnType("varchar(200)");

                // exemplo que é possível alterar o nome da coluna
                e.Property(de => de.StartDate).HasColumnName("Start_Date");

                e.Property(de => de.EndDate).HasColumnName("End_Date");

                e.HasMany(de => de.Speakers).WithOne().HasForeignKey(s => s.DevEventId);
            });

            builder.Entity<DevEventSpeaker>(e =>
            {
                e.HasKey(de => de.Id);
            });
        }



    }
}

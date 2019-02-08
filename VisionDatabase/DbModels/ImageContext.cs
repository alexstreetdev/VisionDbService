using Microsoft.EntityFrameworkCore;

namespace VisionDatabase.DbModels
{
    public class ImageContext : DbContext
    {

        public DbSet<Image> Images { get; set; }
        public DbSet<Content> Content { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(@"Server=168.63.105.225;Database=Vision;Uid=cato;Pwd=cato");
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Image>()
                .Property(b => b.CreatedOn)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            mb.Entity<Image>()
                .HasIndex(i => i.ImageId)
                .IsUnique(true);

            mb.Entity<Image>()
                .HasIndex(i => i.CorrelationId)
                .IsUnique(false);

            mb.Entity<Image>()
                .HasIndex(i => i.Filename)
                .IsUnique(true);


        }
    }
}
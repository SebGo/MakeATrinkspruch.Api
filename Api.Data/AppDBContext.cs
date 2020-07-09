using MakeATrinkspruch.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeATrinkspruch.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Toast> Toasts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ToastTag> ToastTag { get; set; }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Toast>()
                .HasIndex(u => u.ToastText)
                .IsUnique();

            modelBuilder.Entity<Tag>()
               .HasIndex(u => u.TagName)
               .IsUnique();

            modelBuilder.Entity<ToastTag>()
                .HasKey(t => new { t.ToastId, t.TagId });

            modelBuilder.Entity<ToastTag>()
                .HasOne(pt => pt.Toast)
                .WithMany(p => p.ToastTags)
                .HasForeignKey(pt => pt.ToastId);

            modelBuilder.Entity<ToastTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.ToastTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }
}
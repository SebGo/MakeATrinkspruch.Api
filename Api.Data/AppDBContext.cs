using MakeATrinkspruch.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MakeATrinkspruch.Data
{
    public class AppDBContext : IdentityDbContext
    {
        private const string defaultConnectionString = "Server=localhost;Database=MakeATrinkspruch;User=root;Password=BatteryHorseStaples;";

        public DbSet<Toast> Toasts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ToastTag> ToastTag { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(GetConnectionString());
            }
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

        private string GetConnectionString()
        {
            return defaultConnectionString;
        }
    }
}
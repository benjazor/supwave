using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using supwave.Models;

namespace supwave.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Playlist> Playlist { get; set; }
        public DbSet<Playlist> Song { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>(n => {
                // Primary Key
                n.HasKey(p => p.Id);

                // Foreing Key
                n.HasMany<Playlist>()
                .WithOne()
                .HasForeignKey(fk => fk.UserId)
                .IsRequired();
            });

            modelBuilder.Entity<Playlist>(n =>
            {
                // Primary Key
                n.HasKey(p => p.Id);

                // Properties
                n.Property(p => p.Name).HasMaxLength(64);

                // Foreing Key
                n.HasMany<Song>()
                .WithOne()
                .HasForeignKey(fk => fk.PlaylistId)
                .IsRequired();

            });

            modelBuilder.Entity<Song>(n =>
            {
                // Primary Key
                n.HasKey(p => p.Id);

                // Properties
                n.Property(p => p.Name).HasMaxLength(64);
            });
        }
    }
}

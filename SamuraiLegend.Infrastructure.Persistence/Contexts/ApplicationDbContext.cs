using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SamuraiLegend.Application.Interfaces;
using SamuraiLegend.Domain.Common;
using SamuraiLegend.Domain.Entities;
using SamuraiLegend.Infrastructure.Persistence.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SamuraiLegend.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        private readonly IAuthenticatedUserService _authenticatedUser;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) 
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Added:
                        entry.Entity.Id = Guid.NewGuid().ToString();
                        entry.Entity.Created = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Samurai>()
                .Property(t => t.Name)
                .IsRequired().HasMaxLength(50);

            modelBuilder.Entity<Quote>()
                .Property(p => p.Id)
                .HasIdentityOptions(startValue: 41)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Samurai>()
             .HasMany(s => s.Battles)
             .WithMany(b => b.Samurais)
             .UsingEntity<BattleSamurai>
              (bs => bs.HasOne<Battle>().WithMany(),
               bs => bs.HasOne<Samurai>().WithMany())
             .Property(bs => bs.DateJoined);

            modelBuilder.Entity<AppUser>()
                .Property(p => p.FirstName).IsRequired().HasMaxLength(25);

            modelBuilder.Entity<AppUser>()
                .Property(p => p.LastName).IsRequired().HasMaxLength(25);

        }
    }
}

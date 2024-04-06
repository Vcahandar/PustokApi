using Microsoft.EntityFrameworkCore;
using Pustok.Core.Entities.Common;
using Pustok.Core.Entities;
using Pustok.DataAccess.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Pustok.Core.Entities.Identity;

namespace Pustok.DataAccess.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<BookAuthor> BookAuthors { get; set; } = null!;
        public DbSet<BookImage> BookImages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthorConfiguration).Assembly);
            //  modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Author>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Book>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<BookAuthor>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<BookImage>().HasQueryFilter(a => !a.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseSectionEntity>();
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = "Admin";
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = "Admin";
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = "Admin";
                        break;

                    default:
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

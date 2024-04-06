using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pustok.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.DataAccess.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b=>b.Name).IsRequired(true).HasMaxLength(250);
            builder.Property(b => b.Description).IsRequired(true).HasMaxLength(500);
            builder.Property(b => b.Price).IsRequired(true).HasColumnType("decimal(5,2)");
            builder.Property(b => b.DiscountPercent).IsRequired(false);
            builder.Property(b=>b.StockCount).IsRequired(true);
            builder.Property(b => b.PageCount).IsRequired(true);
            builder.Property(b => b.Rating).IsRequired(true);

            builder.HasCheckConstraint("Price", "Price > 0");
            builder.HasCheckConstraint("DiscountPercent", "DiscountPercent BETWEEN 0 AND 100");
            builder.HasCheckConstraint("StockCount", "StockCount >= 0");
            builder.HasCheckConstraint("PageCount", "PageCount BETWEEN 2 AND 25000");
            builder.HasCheckConstraint("Rating", "Rating BETWEEN 1 AND 5");

            builder.HasMany(b => b.BookAuthors).WithOne(ba => ba.Book);
            builder.HasMany(b=>b.BookImages).WithOne(bi => bi.Book);



            
        }
    }
}

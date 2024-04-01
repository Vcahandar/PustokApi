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
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.Fullname).IsRequired(true).HasMaxLength(250);
            builder.Property(a => a.IsDeleted).HasDefaultValue(false);

            //builder.HasMany(a => a.BookAuthors).WithOne(ba => ba.Author);
        }
    }
}

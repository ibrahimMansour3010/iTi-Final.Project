using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Admin
{
    public class AdminEntityConfiguration : IEntityTypeConfiguration<AdminEntity> 
    {
        public void Configure(EntityTypeBuilder<AdminEntity> builder)
        {
            builder.ToTable("Admin");
            builder.HasKey(i => i.ID);
            builder.Property(i => i.ID).ValueGeneratedOnAdd();
            builder.Property(i => i.Firstname).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Lastname).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Email).IsRequired();
            builder.Property(i => i.Username).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Password).IsRequired().HasMaxLength(50);
        }
    }
}

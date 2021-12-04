﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Customer
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.ToTable("Customer");
            builder.Property(i => i.Firstname).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Lastname).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Address).HasMaxLength(50);
            builder.Property(i => i.Gender).HasColumnType("nvarchar").HasMaxLength(10);
            builder.Property(i => i.Image).HasMaxLength(1000);
        }
    }
}

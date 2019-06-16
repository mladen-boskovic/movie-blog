using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
	public class ImageConfiguration : IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> builder)
		{
			builder.Property(i => i.FileName).IsRequired();
			builder.HasIndex(i => i.FileName).IsUnique();
			builder.Property(i => i.CreatedAt).HasDefaultValueSql("GETDATE()");
		}
	}
}

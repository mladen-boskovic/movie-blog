using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
	public class GenreConfiguration : IEntityTypeConfiguration<Genre>
	{
		public void Configure(EntityTypeBuilder<Genre> builder)
		{
			builder.Property(g => g.Name).HasMaxLength(20).IsRequired();
			builder.HasIndex(g => g.Name).IsUnique();
			builder.Property(g => g.CreatedAt).HasDefaultValueSql("GETDATE()");

			builder.HasMany(g => g.MovieGenres).WithOne(mg => mg.Genre)
				.HasForeignKey(mg => mg.GenreId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}

using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
	public class MovieConfiguration : IEntityTypeConfiguration<Movie>
	{
		public void Configure(EntityTypeBuilder<Movie> builder)
		{
			builder.Property(m => m.Name).HasMaxLength(100).IsRequired();
			builder.HasIndex(m => m.Name).IsUnique();
			builder.Property(m => m.Description).HasMaxLength(300).IsRequired();
			builder.Property(m => m.Year).IsRequired();
			builder.Property(m => m.LengthMinutes).IsRequired();
			builder.Property(m => m.TrailerUrl).HasMaxLength(100).IsRequired();
			builder.Property(m => m.CreatedAt).HasDefaultValueSql("GETDATE()");

			builder.HasMany(m => m.MovieGenres).WithOne(mg => mg.Movie)
				.HasForeignKey(mg => mg.MovieId).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(m => m.Likes).WithOne(l => l.Movie)
				.HasForeignKey(l => l.MovieId).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(m => m.Comments).WithOne(c => c.Movie)
				.HasForeignKey(c => c.MovieId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}

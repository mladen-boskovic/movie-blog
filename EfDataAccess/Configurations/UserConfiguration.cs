using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfDataAccess.Configurations
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.Property(u => u.FirstName).HasMaxLength(20).IsRequired();
			builder.Property(u => u.LastName).HasMaxLength(20).IsRequired();
			builder.Property(u => u.Email).HasMaxLength(30).IsRequired();
			builder.HasIndex(u => u.Email).IsUnique();
			builder.Property(u => u.Username).HasMaxLength(20).IsRequired();
			builder.HasIndex(u => u.Username).IsUnique();
			builder.Property(u => u.Password).HasMaxLength(20).IsRequired();
			builder.Property(u => u.CreatedAt).HasDefaultValueSql("GETDATE()");

			builder.HasMany(u => u.Movies).WithOne(m => m.User)
				.HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);
			builder.HasMany(u => u.Likes).WithOne(l => l.User)
				.HasForeignKey(l => l.UserId).OnDelete(DeleteBehavior.Cascade);
			builder.HasMany(u => u.Comments).WithOne(c => c.User)
				.HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}

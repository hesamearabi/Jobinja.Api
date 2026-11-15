using Jobinja.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobinja.Api.Configurations;

public class KarjoConfiguration : IEntityTypeConfiguration<Karjo>
{
    public void Configure(EntityTypeBuilder<Karjo> builder)
    {
        builder.ToTable("Karjos");

        builder.Property(k => k.JobTitle)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(k => k.ResumePath)
            .HasMaxLength(500);

        builder.HasOne(k => k.User)
            .WithOne()
            .HasForeignKey<Karjo>(k => k.UserId);
    }
}
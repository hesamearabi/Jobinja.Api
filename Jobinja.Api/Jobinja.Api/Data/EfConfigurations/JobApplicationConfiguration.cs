using Jobinja.Api.Entities;
using Jobinja.Api.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jobinja.Api.Configurations;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.ToTable("JobApplications");

        builder.Property(ja => ja.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(ja => ja.PhoneNumber)
            .IsRequired()
            .HasMaxLength(15);

        builder.Property(ja => ja.ResumePath)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(ja => ja.Agahi)
            .WithMany(a => a.JobApplications)
            .HasForeignKey(ja => ja.AgahiId);

        builder.HasOne(ja => ja.Karjo)
            .WithMany(k => k.JobApplications)
            .HasForeignKey(ja => ja.KarjoId);

     
    }
}
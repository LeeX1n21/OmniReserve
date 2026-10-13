using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmniReserve.Domain.Entities;
using OmniReserve.Domain.ValueObjects;

namespace OmniReserve.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(user => user.Email)
               .HasConversion(
                   emailObj => emailObj.Value, 
                   emailStr => new EmailAddress(emailStr)
               )
               .HasColumnName("Email")
               .HasMaxLength(255)
               .IsRequired();

        builder.HasIndex(user => user.Email)
               .IsUnique();

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<string>();
    }
}
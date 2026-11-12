using Ebay.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebay.Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", Schema.User);
        builder.HasKey(usr => usr.Id);
        builder.Property(usr => usr.FirstName)
            .HasMaxLength(125);
        builder.Property(usr => usr.LastName)
            .HasMaxLength(125);
        builder.Property(usr => usr.Email)
            .HasMaxLength(125);
        builder.Property(usr => usr.PhoneNo)
            .HasMaxLength(20);
        builder.HasIndex(usr => usr.Email)
            .IsUnique();
        builder.HasIndex(usr => usr.Username)
            .IsUnique();
        builder.HasOne(usr => usr.Role)
            .WithMany()
            .HasForeignKey(usr => usr.RoleId);
        builder.OwnsOne(usr => usr.Address, addressBuilder =>
        {
            addressBuilder.Property(add => add.City).HasMaxLength(50);
            addressBuilder.Property(add => add.Country).HasMaxLength(50);
            addressBuilder.Property(add => add.PostCode).HasMaxLength(10);
            addressBuilder.Property(add => add.Street).HasMaxLength(100);
        });

    }
}

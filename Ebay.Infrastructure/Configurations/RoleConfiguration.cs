using Ebay.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ebay.Infrastructure.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles", Schema.User);
        builder.HasKey(role => role.Id);
        builder.Property(role => role.Name)
            .HasMaxLength(50);
        builder.Property(role => role.Code)
            .HasMaxLength(50);
    }
}

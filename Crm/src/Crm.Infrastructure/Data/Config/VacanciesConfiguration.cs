using Crm.Core.CrmAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Infrastructure.Data.Config
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .OwnsOne(c => c.Address);
        }
    }
}

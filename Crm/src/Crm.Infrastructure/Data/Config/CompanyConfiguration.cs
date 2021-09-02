using Crm.Core.CrmAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Crm.Infrastructure.Data.Config
{
    public class VacanciesConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.Property(v => v.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(v => v.Description)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}

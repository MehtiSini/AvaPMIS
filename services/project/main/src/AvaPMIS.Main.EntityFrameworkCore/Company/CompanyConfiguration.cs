using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AvaPMIS.Main.Company
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Main.Company.Company>
    {
        public void Configure(EntityTypeBuilder<Main.Company.Company> builder)
        {
            builder.ToTable("Company");

            builder.HasMany(c=>c.Departments)
                .WithOne(d => d.Company)
                .HasForeignKey(x => x.CompanyId)
                .IsRequired();
        }
    }
}

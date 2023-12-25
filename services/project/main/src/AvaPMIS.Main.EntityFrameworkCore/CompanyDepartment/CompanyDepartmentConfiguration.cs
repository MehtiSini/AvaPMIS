using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.CompanyDepartment
{
    public class CompanyDepartmentConfiguration : IEntityTypeConfiguration<CompanyDepartment>
    {
        public void Configure(EntityTypeBuilder<CompanyDepartment> builder)
        {
            builder.ToTable("CompanyDepartment");

            builder.HasOne(c => c.Company)
                .WithMany(d => d.CompanyDepartments)
             .HasForeignKey(x => x.CompanyId)
                .IsRequired();

            builder.HasMany(c => c.DepartmentDisciplines)
             .WithOne(d => d.CompanyDepartment)
             .HasForeignKey(x=>x.CompanyDepartmentId);
        }
    }
}


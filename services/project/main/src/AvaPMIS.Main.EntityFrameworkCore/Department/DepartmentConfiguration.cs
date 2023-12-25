using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.Department
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department");

            builder.HasOne(c => c.Company)
                .WithMany(d => d.Departments)
             .HasForeignKey(x => x.CompanyId)
                .IsRequired();

            builder.HasMany(c => c.DepartmentDisciplines)
             .WithOne(d => d.Department)
             .HasForeignKey(x=>x.DepartmentId);
        }
    }
}


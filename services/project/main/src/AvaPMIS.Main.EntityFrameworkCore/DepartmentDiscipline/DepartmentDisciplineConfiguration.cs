using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.DepartmentDiscipline
{
    public class DepartmentDisciplineConfiguration : IEntityTypeConfiguration<DepartmentDiscipline>
    {
        public void Configure(EntityTypeBuilder<DepartmentDiscipline> builder)
        {
            builder.ToTable("DepartmentDiscipline");

            builder.HasOne(d => d.CompanyDepartment)
                .WithMany(d => d.DepartmentDisciplines).
                HasForeignKey(x=>x.CompanyDepartmentId)
                .IsRequired();

        }
    }
}
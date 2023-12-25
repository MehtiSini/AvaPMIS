using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.Discipline
{
    public class DepartmentDisciplineConfiguration : IEntityTypeConfiguration<DepartmentDiscipline.DepartmentDiscipline>
    {
        public void Configure(EntityTypeBuilder<DepartmentDiscipline.DepartmentDiscipline> builder)
        {
            builder.ToTable("DepartmentDiscipline");

            builder.HasOne(d => d.Department)
                .WithMany(d => d.DepartmentDisciplines).
                HasForeignKey(x=>x.DepartmentId)
                .IsRequired();

        }
    }
}
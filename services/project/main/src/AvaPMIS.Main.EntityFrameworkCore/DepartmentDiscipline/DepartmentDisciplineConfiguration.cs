using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.DepartmentDiscipline
{
    public class DepartmentDisciplineConfiguration : IEntityTypeConfiguration<DepartmentDiscipline>
    {
        public void Configure(EntityTypeBuilder<DepartmentDiscipline> builder)
        {
            builder.ToTable("DepartmentDiscipline");

            builder.HasOne(c => c.DefDiscipline)
     .WithMany(d => d.DepartmentDisciplines)
     .HasForeignKey(x => x.DefDicsiplineId);

        }
    }
}
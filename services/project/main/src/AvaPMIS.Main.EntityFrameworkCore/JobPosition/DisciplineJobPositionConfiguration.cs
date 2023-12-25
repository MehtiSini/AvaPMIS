using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.JobPosition
{
    public class DisciplineJobPositionConfiguration : IEntityTypeConfiguration<DisciplineJobPosition.DisciplineJobPosition>
    {
        public void Configure(EntityTypeBuilder<DisciplineJobPosition.DisciplineJobPosition> builder)
        {
            builder.ToTable("DisciplineJobPosition");

            builder.HasOne(d => d.DepartmentDiscipline)
                .WithMany(d => d.DiciplineJobPositions).
                HasForeignKey(x => x.DepartmentDisciplineId)
                .IsRequired();

            builder.HasMany(d => d.People)
                .WithOne(d => d.DisciplineJobPosition).
                HasForeignKey(x => x.DisciplineJobPositionId)
                .IsRequired();

        }
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.DisciplineJobPosition
{
    public class DisciplineJobPositionConfiguration : IEntityTypeConfiguration<DisciplineJobPosition>
    {
        public void Configure(EntityTypeBuilder<DisciplineJobPosition> builder)
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
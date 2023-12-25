using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.JobPosition
{
    public class JobPositionConfiguration : IEntityTypeConfiguration<JobPosition>
    {
        public void Configure(EntityTypeBuilder<JobPosition> builder)
        {
            builder.ToTable("JobPosition");

            builder.HasOne(d => d.Discipline)
                .WithMany(d => d.JobPositions).
                HasForeignKey(x => x.DisciplineId)
                .IsRequired();

            builder.HasMany(d => d.People)
                .WithOne(d => d.JobPosition).
                HasForeignKey(x => x.JobPositionId)
                .IsRequired();

        }
    }
}
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.Discipline
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.ToTable("Discipline");

            builder.HasOne(d => d.Department)
                .WithMany(d => d.Disciples).
                HasForeignKey(x=>x.DepartmentId)
                .IsRequired();

        }
    }
}
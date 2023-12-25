using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.DefDiscipline
{
    public class DefDisciplineConfiguration : IEntityTypeConfiguration<DefDiscipline>
    {
        public void Configure(EntityTypeBuilder<DefDiscipline> builder)
        {
            builder.ToTable("DefDiscipline");
        }
    }
}
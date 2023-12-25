using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.DefJobPosition
{
    public class DefJobPositionConfiguration : IEntityTypeConfiguration<DefJobPosition>
    {
        public void Configure(EntityTypeBuilder<DefJobPosition> builder)
        {
            builder.ToTable("DefJobPosition");
        }
    }
}
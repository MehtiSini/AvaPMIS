using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.DefDepartment
{
    public class DefDepartmentConfiguration : IEntityTypeConfiguration<DefDepartment>
    {
        public void Configure(EntityTypeBuilder<DefDepartment> builder)
        {
            builder.ToTable("DefDepartment");
        }
    }
}

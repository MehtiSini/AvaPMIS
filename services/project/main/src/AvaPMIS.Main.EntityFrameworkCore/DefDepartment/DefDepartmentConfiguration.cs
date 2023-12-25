using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.DefDepartment
{
    public class DefDepartmentConfiguration : IEntityTypeConfiguration<DefDepartment>
    {
        public void Configure(EntityTypeBuilder<DefDepartment> builder)
        {
            builder.ToTable("DefDepartment");

            builder.HasMany(x=>x.CompanyDepartments)
                .WithOne(x=>x.DefDepartment)
                .HasForeignKey(x=>x.DefDepartmentId)
                .IsRequired();


        }
    }
}

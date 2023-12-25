using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AvaPMIS.Main.Person
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasOne(d => d.JobPosition)
                .WithMany(d => d.People).
                HasForeignKey(x => x.JobPositionId)
                .IsRequired();

        }
    }
}

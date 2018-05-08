using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkingTime.Entities.Models;

namespace WorkingTime.Data.EntityTypeConfig
{
    public class EmployeeTypeConfig: IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

            builder.ToTable("Employee");

            builder.HasKey(e => e.EmployeeId);

            builder.Property(e => e.EmployeeId)
                .HasColumnName("Employee_ID");

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasMany(e => e.EmployeeShifts);
        }
    }
}


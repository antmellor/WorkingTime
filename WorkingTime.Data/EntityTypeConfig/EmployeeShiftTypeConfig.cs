using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkingTime.Entities.Models;

namespace WorkingTime.Data.EntityTypeConfig
{
    public class EmployeeShiftTypeConfig : IEntityTypeConfiguration<EmployeeShift>
    {
        public void Configure(EntityTypeBuilder<EmployeeShift> builder)
        {

            builder.ToTable("Employee_Works_Shift");

            builder.Property(es => es.EmployeeId)
                .HasColumnName("Employee_ID");
            builder.Property(es => es.ShiftId)
                .HasColumnName("Shift_ID");

            builder.HasKey(es => new { es.EmployeeId, es.ShiftId });

            builder
                .HasOne(es => es.Shift)
                .WithMany(s => s.ShiftEmployees)
                .HasForeignKey(es => es.ShiftId);

            builder
                .HasOne(es => es.Employee)
                .WithMany(e => e.EmployeeShifts)
                .HasForeignKey(es => es.EmployeeId);

        }
    }
}

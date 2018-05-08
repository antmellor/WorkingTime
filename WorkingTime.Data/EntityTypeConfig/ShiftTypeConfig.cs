using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkingTime.Entities.Models;

namespace WorkingTime.Data.EntityTypeConfig
{
    public class ShiftTypeConfig : IEntityTypeConfiguration<Shift>
    {
        public void Configure(EntityTypeBuilder<Shift> builder)
        {
            builder.ToTable("Shifts");

            builder.HasKey(s => s.ShiftId);

            builder.Property(s => s.ShiftId).HasColumnName("Shift_ID");

            builder.Property(s => s.ShiftEnd)
                .HasColumnName("Shift_End")
                .HasColumnType("datetime");

            builder.Property(s => s.ShiftName)
                .IsRequired()
                .HasColumnName("Shift_Name")
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(s => s.ShiftStart)
                .HasColumnName("Shift_Start")
                .HasColumnType("datetime");

            builder.HasMany(s => s.ShiftEmployees);
        }
    }
}

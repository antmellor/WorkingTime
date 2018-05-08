using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WorkingTime.Data.EntityTypeConfig;
using WorkingTime.Entities.Models;

namespace WorkingTime.Data
{
    public partial class WorkingTimeContext : DbContext
    {
        public WorkingTimeContext(DbContextOptions<WorkingTimeContext> options):base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<EmployeeShift> EmployeeShifts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new EmployeeTypeConfig())
                .ApplyConfiguration(new ShiftTypeConfig())
                .ApplyConfiguration(new EmployeeShiftTypeConfig());
        }
    }
}

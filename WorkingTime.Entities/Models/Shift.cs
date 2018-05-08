using System;
using System.Collections.Generic;

namespace WorkingTime.Entities.Models
{
    public partial class Shift
    {
        public int ShiftId { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public string ShiftName { get; set; }

        public virtual IEnumerable<EmployeeShift> ShiftEmployees { get; set; }
    }
}

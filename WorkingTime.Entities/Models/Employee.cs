using System;
using System.Collections.Generic;
using System.Linq;

namespace WorkingTime.Entities.Models
{
    public partial class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public virtual IEnumerable<EmployeeShift> EmployeeShifts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTime.Entities.Models;
using WorkingTime.Entities.Models.DTOs;

namespace WorkingTime.Web.Models
{
    public class EmployeeShiftViewModel
    {
        public int EmployeeId;
        public List<ShiftDTO> ShiftsForMonth;
        public double TotalHours;
    }
}

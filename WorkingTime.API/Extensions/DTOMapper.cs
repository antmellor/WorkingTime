using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTime.Entities.Models;
using WorkingTime.Entities.Models.DTOs;

namespace WorkingTime.API.Extensions
{
    public static class DTOMapper
    {
        public static EmployeeDTO MapToEmployeeDTO(this Employee employee)
        {
            return new EmployeeDTO()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                Surname = employee.Surname
                //Months = employee.EmployeeShifts
                //            .OrderByDescending(es => es.Shift.ShiftStart)
                //            .GroupBy(es => new { es.Shift.ShiftStart.Month, es.Shift.ShiftStart.Year })
                //            .Select(g => new MonthYear()
                //            {
                //                Month = g.Key.Month,
                //                Year = g.Key.Year
                //            })
                //            .ToList()
            };
        }

        public static ShiftDTO MapToShiftDTO(this Shift shift)
        {
            return new ShiftDTO()
            {
                ShiftId = shift.ShiftId,
                ShiftStart = shift.ShiftStart,
                ShiftEnd = shift.ShiftEnd,
                ShiftName = shift.ShiftName
            };
        }
    }
}

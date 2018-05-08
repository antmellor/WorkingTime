using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingTime.Entities.Models.DTOs
{

    /* Added a DTO to restrict the data being published by the Web API, excluding
     * the EmployeeShifts IEnumerable from the data transmitted.
     * In a real-world application, I imagine the number of shifts worked by an employee could 
     * grow to be quite large. */
    public class EmployeeDTO
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }

        // public List<MonthYear> Months;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingTime.Entities.Models.DTOs
{
    public class ShiftDTO
    {
        public int ShiftId { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public string ShiftName { get; set; }
    }
}

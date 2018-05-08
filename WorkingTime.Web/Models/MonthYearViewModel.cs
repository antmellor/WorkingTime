using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTime.Entities.Models;

namespace WorkingTime.Web.Models
{
    public class MonthYearViewModel
    {
        public List<MonthYear> EmployeeMonths;
        public int EmployeeId { get; set; }

        [Display(Name = "Work Month")]
        public string SelectedMonthYear { get; set;  } 

        public List<SelectListItem> MonthSelectList
        {
            get
            {
                if (EmployeeMonths == null || EmployeeMonths.Count() == 0)
                    return new List<SelectListItem>();

                return EmployeeMonths.Select(m => new SelectListItem()
                {
                    Value = $"{m.Month}/{m.Year}",
                    Text = new System.Globalization.DateTimeFormatInfo().GetMonthName(m.Month).ToString()
                            + " " + m.Year.ToString()
                }).ToList();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingTime.Entities.Models.DTOs;

namespace WorkingTime.Web.Models
{
    public class EmployeeViewModel
    {
        public List<EmployeeDTO> Employees;

        [Display(Name = "Employee")]
        public int SelectedEmployeeId { get; set; }

        public IEnumerable<SelectListItem> EmployeeSelectList
        {
            get
            {
                return Employees.Select(e => new SelectListItem()
                {
                    Value = e.EmployeeId.ToString(),
                    Text = e.FirstName + " " + e.Surname
                });
            }
        }
    }
}

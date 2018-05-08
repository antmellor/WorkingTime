using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WorkingTime.Entities.Models;
using WorkingTime.Entities.Models.DTOs;
using WorkingTime.Web.Models;
using WorkingTime.Web.Services;

namespace WorkingTime.Web.Controllers
{
    public class WorkingTimeController : Controller
    {

        private IAPIClient client;

        public WorkingTimeController(IAPIClient apiClient)
        {
            client = apiClient;
        }

        public IActionResult Index()
        {
            try
            {
                var employees = client.GetEmployees().Result;
                var viewModel = new EmployeeViewModel()
                {
                    Employees = employees
                };
                return View(viewModel);
            } catch (Exception ex)
            {
                ViewBag.Error = "API Not available. Please contact support for assistance.";
                return View("APINotAvailable");
            }
        }
       
        public IActionResult EmployeeMonthsWorked(int employeeId)
        {
            var months = client.GetEmployeeWorkMonths(employeeId).Result
                ?? new List<MonthYear>();

            var viewModel = new MonthYearViewModel()
            {
                EmployeeMonths = months,
                EmployeeId = employeeId
            };

            return PartialView("_MonthsWorkedPartial", viewModel);
        }

        public IActionResult EmployeeShiftsInMonth(int employeeId, string monthYear)
        {
            if (monthYear == null || monthYear.Contains("/") == false)
                return PartialView("_ShiftsPartial");
            

            string[] monYear = monthYear.Split("/");

            var shifts = client.GetEmployeeShifts(employeeId,
                                    Convert.ToInt32(monYear[1]),
                                    Convert.ToInt32(monYear[0])).Result
                                ?? new List<ShiftDTO>();

            var employeeShifts = new EmployeeShiftViewModel()
            {
                EmployeeId = employeeId,
                ShiftsForMonth = shifts,
                TotalHours = shifts
                                .Select(s => (s.ShiftEnd - s.ShiftStart).TotalHours)
                                .Sum()
            };
            return PartialView("_ShiftsPartial", employeeShifts);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkingTime.Data;
using WorkingTime.Entities.Models;
using WorkingTime.Entities.Models.DTOs;
using WorkingTime.API.Extensions;

namespace WorkingTime.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Employee")]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EmployeeDTO>), 200)]
        public IActionResult GetEmployees()
        {
            return Ok(
                _repository.GetAllEmployees()
                    .Select(e => e.MapToEmployeeDTO())
                    .ToList()
                );
        }

        [HttpGet("{employeeId}")]
        [ProducesResponseType(typeof(EmployeeDTO), 200)]
        public IActionResult GetEmployee(int employeeId)
        {
            var e = _repository.GetEmployee(employeeId);
            if (e == null)
                return NoContent();

            return Ok(e.MapToEmployeeDTO());
        }

        [HttpGet("{employeeId}/months")]
        [ProducesResponseType(typeof(List<MonthYear>), 200)]
        public IActionResult GetEmployeeMonths(int employeeId)
        {
            var e = _repository.GetEmployee(employeeId);
            if (e == null)
                return NoContent();

            var monthsWorked = e.EmployeeShifts
                     .OrderByDescending(es => es.Shift.ShiftStart)
                     .GroupBy(es => new { es.Shift.ShiftStart.Month, es.Shift.ShiftStart.Year })
                     .Select(g => new MonthYear()
                     {
                         Month = g.Key.Month,
                         Year = g.Key.Year
                     })
                     .ToList();

            if (monthsWorked == null || monthsWorked.Count() == 0)
                return NoContent();

            return Ok(monthsWorked);
        }

        [HttpGet("{employeeId}/shifts")]
        [ProducesResponseType(typeof(List<ShiftDTO>), 200)]
        public IActionResult GetEmployeeShifts(int employeeId)
        {
            var e = _repository.GetEmployee(employeeId);
            if (e == null)
                return NoContent();

            var shifts = e.EmployeeShifts
                .Select(es => es.Shift.MapToShiftDTO())
                .ToList();

            if (shifts == null || shifts.Count() == 0)
                return NoContent();

            return Ok(e.EmployeeShifts
                .Select(es => es.Shift.MapToShiftDTO())
                .ToList()
             );
        }

        [HttpGet("{employeeId}/shifts/{year}/{month}")]
        [ProducesResponseType(typeof(List<ShiftDTO>), 200)]
        public IActionResult GetEmployeeShiftsForMonth(int employeeId, int year, int month)
        {
            var e = _repository.GetEmployee(employeeId);
            if (e == null)
                return NoContent();

            var shifts = e.EmployeeShifts
                .Where(es => es.Shift.ShiftStart.Month == month
                    && es.Shift.ShiftStart.Year == year)
                .Select(es => es.Shift.MapToShiftDTO())
                .ToList();

            if (shifts == null || shifts.Count() == 0)
                return NoContent();

            return Ok(shifts);
        }

    }
}
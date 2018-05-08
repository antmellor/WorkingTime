using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WorkingTime.Entities.Models;

namespace WorkingTime.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly WorkingTimeContext _context;

        public EmployeeRepository(WorkingTimeContext context)
        {
            _context = context;
        }

        public List<Employee> GetAllEmployees()
        {
            return _context.Employees
                .Include(e => e.EmployeeShifts)
                    .ThenInclude(es => es.Shift)
                .ToList();
        }

        public Employee GetEmployee(int employeeId)
        {
            return _context.Employees
                .Where(e => e.EmployeeId == employeeId)
                .Include(e => e.EmployeeShifts)
                    .ThenInclude(es => es.Shift)
                .FirstOrDefault();
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WorkingTime.API.Controllers;
using WorkingTime.Data;
using WorkingTime.Entities.Models;
using WorkingTime.Entities.Models.DTOs;
using Xunit;

namespace WorkingTime.API.Tests
{
    public class EmployeeControllerTest
    {
        private List<Employee> employees;
        private IEmployeeRepository empRepo;
        private EmployeeController empController;

        public EmployeeControllerTest()
        {
            using (StreamReader sr = new StreamReader(@"TestData\TestEmployees.json"))
            {
                var json = sr.ReadToEnd();
                employees = JsonConvert.DeserializeObject<IEnumerable<Employee>>(json).ToList();
                empRepo = new MockEmployeeRepository(employees);
                empController = new EmployeeController(empRepo);
            }
        }

        [Fact]
        public void GetEmployees_ReturnsAnIActionResultWithStatus200()
        {
            var result = empController.GetEmployees();
            var okResult = result as OkObjectResult;
            var employees = okResult.Value as IEnumerable<EmployeeDTO>;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(3, employees.Count());
        }

        [Fact]
        public void GetEmployee_CheckForEmployeeThatExists()
        {
            var result = empController.GetEmployee(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var employee = okResult.Value as EmployeeDTO;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Testy", employee.FirstName);
            /* Employee 1 has the following shifts:
                
                Morning 9-17, 26/11/16
                Morning 10-14, 12/11/16
                Morning 9-17, 13/11/16
                Morning 9-17, 15/11/16
                Morning 9-17, 14/12/16

                Data for 2 months - 11/16 and 12/16
             */
            //Assert.Equal(2, employee.Months.Count());
        }

        [Fact]
        public void GetEmployee_CheckForEmployeeThatDoesNotExist()
        {
            var result = empController.GetEmployee(59696);
            base_RecordNotExist(result);
        }

        [Fact]
        public void GetEmployeeMonths_CheckForEmployeeThatExists()
        {
            var result = empController.GetEmployeeMonths(1);
            var okResult = Assert.IsType<OkObjectResult>(result);

            var months = okResult.Value as List<MonthYear>;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(2, months.Count());
        }

        [Fact]
        public void GetEmployeeMonths_CheckForEmployeeThatDoesNotExist()
        {
            var result = empController.GetEmployeeMonths(3435561);
            base_RecordNotExist(result);
        }

        [Fact]
        public void GetEmployeeMonths_EmployeeWithNoShifts()
        {
            var result = empController.GetEmployeeMonths(3);
            base_RecordNotExist(result);
        }

        [Fact]
        public void GetEmployeeShifts_CheckForEmployeeThatExists()
        {
            // Employee with ID 1 has 5 shifts overall
            var result = empController.GetEmployeeShifts(1);
            var okResult = Assert.IsType<OkObjectResult>(result);

            var shifts = okResult.Value as List<ShiftDTO>;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(5, shifts.Count());
        }

        [Fact]
        public void GetEmployeeShifts_CheckForEmployeeThatDoesNotExist()
        {
            var result = empController.GetEmployeeShifts(59696);
            base_RecordNotExist(result);
        }

        [Fact]
        public void GetEmployeeShifts_EmployeeWithNoShifts()
        {
            var result = empController.GetEmployeeShifts(3);
            base_RecordNotExist(result);
        }

        [Fact]
        public void GetEmployeeShiftsForMonth_CheckForEmployeeThatExists()
        {
            // Employee with ID 1 has 4 shifts in 11/2016
            var result = empController.GetEmployeeShiftsForMonth(1, 2016, 11);
            var okResult = Assert.IsType<OkObjectResult>(result);

            var shifts = okResult.Value as List<ShiftDTO>;

            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(4, shifts.Count());
        }

        [Fact]
        public void GetEmployeeShiftsForMonth_CheckExistingEmployeeWithNoShiftsInMonth()
        {
            // Employee with ID 1 has 4 shifts in 11/2016
            var result = empController.GetEmployeeShiftsForMonth(1, 2019, 11);
            base_RecordNotExist(result);
        }

        internal static void base_RecordNotExist(IActionResult result)
        {
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.NotNull(noContentResult);
            Assert.Equal(204, noContentResult.StatusCode);
        }

    }
    
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employees;

        public MockEmployeeRepository(List<Employee> employees)
        {
            _employees = employees;
        }

        public List<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee GetEmployee(int employeeId)
        {
            return _employees.Where(e => e.EmployeeId == employeeId).FirstOrDefault();
        }
    }
}

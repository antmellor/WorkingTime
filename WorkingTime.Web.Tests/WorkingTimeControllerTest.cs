using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WorkingTime.Entities.Models;
using WorkingTime.Entities.Models.DTOs;
using WorkingTime.Web.Controllers;
using WorkingTime.Web.Models;
using WorkingTime.Web.Services;
using Xunit;

namespace WorkingTime.Web.Tests
{
    public class WorkingTimeControllerTest
    {
        private List<EmployeeDTO> employees;
        private List<ShiftDTO> shifts;
        private List<MonthYear> months;
        private IAPIClient client;
        private WorkingTimeController controller;

        public WorkingTimeControllerTest()
        {
            using (StreamReader sr = new StreamReader(@"TestData\Employees.json"))
            {
                var json = sr.ReadToEnd();
                employees = JsonConvert.DeserializeObject<IEnumerable<EmployeeDTO>>(json).ToList();
            }
            using (StreamReader sr = new StreamReader(@"TestData\EmployeeShiftsInMonth.json"))
            {
                var json = sr.ReadToEnd();
                shifts = JsonConvert.DeserializeObject<IEnumerable<ShiftDTO>>(json).ToList();
            }
            using (StreamReader sr = new StreamReader(@"TestData\EmployeeMonths.json"))
            {
                var json = sr.ReadToEnd();
                months = JsonConvert.DeserializeObject<IEnumerable<MonthYear>>(json).ToList();
            }
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithListOfEmployees()
        {
            client = new MockApiClient(shifts, employees, months);
            controller = new WorkingTimeController(client);

            var result = controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<EmployeeViewModel>(viewResult.ViewData.Model);
            Assert.Equal(6, model.Employees.Count());
        }

        [Fact]
        public void EmployeeMonthsWorked_EmployeeWithShifts_ReturnsListOfMonths()
        {
            client = new MockApiClient(shifts, employees, months);
            controller = new WorkingTimeController(client);

            var result = controller.EmployeeMonthsWorked(1);

            // dummy data contains 2 months
            var viewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsAssignableFrom<MonthYearViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, model.EmployeeMonths.Count());
        }

        [Fact]
        public void EmployeeMonthsWorked_EmployeeWithNoShifts_ReturnsEmptyList()
        {
            client = new MockApiClient(shifts, employees, null);
            controller = new WorkingTimeController(client);

            var result = controller.EmployeeMonthsWorked(3);
            var viewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsAssignableFrom<MonthYearViewModel>(viewResult.ViewData.Model);
            Assert.Empty(model.EmployeeMonths);
        }

        [Fact]
        public void EmployeeShiftsInMonth_EmployeeWithShifts_ReturnsViewModel_CalculatesTotalHours()
        {
            client = new MockApiClient(shifts, employees, months);
            controller = new WorkingTimeController(client);

            var result = controller.EmployeeShiftsInMonth(1, "11/2016");
            
            // test data includes 4 shifts (3x 9-17, 1x 10-14) - total hours = 28

            var viewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsAssignableFrom<EmployeeShiftViewModel>(viewResult.ViewData.Model);
            Assert.Equal(4, model.ShiftsForMonth.Count());
            Assert.Equal(28, model.TotalHours);
        }

        [Fact]
        public void EmployeeShiftsInMonth_EmployeeWithNoShifts_ReturnsViewModel_CalculatesTotalHours()
        {
            // pass no shift data to the mock client - test without shifts
            client = new MockApiClient(null, employees, null);
            controller = new WorkingTimeController(client);

            var result = controller.EmployeeShiftsInMonth(1, null);
            var viewResult = Assert.IsType<PartialViewResult>(result);
            Assert.Null(viewResult.ViewData.Model);
        }
    }

    public class MockApiClient : IAPIClient
    {
        public List<ShiftDTO> _shifts;
        public List<EmployeeDTO> _employees;
        public List<MonthYear> _months;

        public MockApiClient(List<ShiftDTO> shifts, List<EmployeeDTO> employees, List<MonthYear> months)
        {
            _shifts = shifts;
            _employees = employees;
            _months = months;
        }

        public Task<List<EmployeeDTO>> GetEmployees()
        {
            return Task.FromResult(_employees);
        }

        public Task<List<ShiftDTO>> GetEmployeeShifts(int employeeId, int year, int month)
        {
            return Task.FromResult(_shifts);
        }

        public Task<List<MonthYear>> GetEmployeeWorkMonths(int employeeId)
        {
            return Task.FromResult(_months);
        }
    }
}

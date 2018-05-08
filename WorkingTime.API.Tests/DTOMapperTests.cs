using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WorkingTime.API.Extensions;
using WorkingTime.Entities.Models;
using WorkingTime.Entities.Models.DTOs;
using Xunit;

namespace WorkingTime.API.Tests
{
    public class DTOMapperTests
    {
        private List<Employee> employees;

        public DTOMapperTests()
        {
            using (StreamReader sr = new StreamReader(@"TestData\TestEmployees.json"))
            {
                var json = sr.ReadToEnd();
                employees = JsonConvert.DeserializeObject<IEnumerable<Employee>>(json).ToList();
            }
        }

        [Fact]
        public void MapToEmployeeDTO_ReturnsEmployeeDTOObject()
        {
            var employee = employees.FirstOrDefault();
            var result = employee.MapToEmployeeDTO();

            Assert.IsType<EmployeeDTO>(result);
            Assert.Equal(employee.FirstName, result.FirstName);
        }

        [Fact]
        public void MapToShiftDTO_ReturnsShiftDTOObject()
        {
            var employee = employees.FirstOrDefault();
            var es = employee.EmployeeShifts.FirstOrDefault();
            var shift = es.Shift;

            var result = shift.MapToShiftDTO();

            Assert.IsType<ShiftDTO>(result);
            Assert.Equal(shift.ShiftName, result.ShiftName);
        }
    }
}

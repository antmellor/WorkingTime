using System.Collections.Generic;
using WorkingTime.Entities.Models;

namespace WorkingTime.Data
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployee(int employeeId);
    }
}
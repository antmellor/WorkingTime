using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WorkingTime.Entities.Models;
using WorkingTime.Entities.Models.DTOs;

namespace WorkingTime.Web.Services
{
    public interface IAPIClient
    {
        Task<List<EmployeeDTO>> GetEmployees();
        Task<List<ShiftDTO>> GetEmployeeShifts(int employeeId, int year, int month);
        Task<List<MonthYear>> GetEmployeeWorkMonths(int employeeId);
    }
}
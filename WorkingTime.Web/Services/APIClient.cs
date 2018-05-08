using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WorkingTime.Entities.Models;
using WorkingTime.Entities.Models.DTOs;

namespace WorkingTime.Web.Services
{
    public class APIClient : IAPIClient
    {
        private HttpClient client;

        public APIClient()
        {
            // Set up the base service URL
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9876/api/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<EmployeeDTO>> GetEmployees()
        {
            var employees = new List<EmployeeDTO>();
            var response = await client.GetAsync("employee");
            if (response.IsSuccessStatusCode)
            {
                var rawJson = response.Content.ReadAsStringAsync().Result;
                employees = JsonConvert.DeserializeObject<List<EmployeeDTO>>(rawJson);
            }
            return employees;
        }

        public async Task<List<MonthYear>> GetEmployeeWorkMonths(int employeeId)
        {
            var months = new List<MonthYear>();
            var response = await client.GetAsync($"employee/{employeeId}/months");
            if (response.IsSuccessStatusCode)
            {
                var rawJson = response.Content.ReadAsStringAsync().Result;
                months = JsonConvert.DeserializeObject<List<MonthYear>>(rawJson);
            }
            return months;
        }

        public async Task<List<ShiftDTO>> GetEmployeeShifts(int employeeId, int year, int month)
        {
            var shifts = new List<ShiftDTO>();
            var response = await client.GetAsync($"employee/{employeeId}/shifts/{year}/{month}");
            if (response.IsSuccessStatusCode)
            {
                var rawJson = response.Content.ReadAsStringAsync().Result;
                shifts = JsonConvert.DeserializeObject<List<ShiftDTO>>(rawJson);
            }
            return shifts;
        }
    }
}

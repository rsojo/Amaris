using Amaris.Helpers.APIResponse;
using Amaris.Models.DTO;

namespace Amaris.Helpers.Mappers
{
    public class EmployeeAPIMapper
    {
        public static Employee MapEmployeeAPI(EmployeeAPI employeeAPI)
        {
            return new Employee
            {
                Id = employeeAPI.id,
                Name = employeeAPI.employee_name,
                Salary = employeeAPI.employee_salary,
                Age = employeeAPI.employee_age,
                ProfileImage = employeeAPI.profile_image
            };
        }

    }
}

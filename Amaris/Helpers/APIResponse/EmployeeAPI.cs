namespace Amaris.Helpers.APIResponse
{
    /// <summary>
    /// Represents an employee in the API response.
    /// </summary>
    public class EmployeeAPI
    {
        /// <summary>
        /// Gets or sets the employee ID.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the employee name.
        /// </summary>
        public string? employee_name { get; set; }

        /// <summary>
        /// Gets or sets the employee salary.
        /// </summary>
        public float? employee_salary { get; set; }

        /// <summary>
        /// Gets or sets the employee age.
        /// </summary>
        public int employee_age { get; set; }

        /// <summary>
        /// Gets or sets the employee profile image.
        /// </summary>
        public string? profile_image { get; set; }
    }
}

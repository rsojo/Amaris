namespace Amaris.Models.DTO
{ /// <summary>
  /// Represents an employee.
  /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the employee ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the employee name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the employee salary.
        /// </summary>
        public float? Salary { get; set; }

        /// <summary>
        /// Gets or sets the employee age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the employee profile image.
        /// </summary>
        public string? ProfileImage { get; set; }


        /// <summary>
        /// Gets the employee's annual salary.
        /// </summary>
        public float AnnualSalary
        {
            get
            {
                if (Salary.HasValue)
                {
                    return Salary.Value * 12;
                }
                else
                {
                    return 0;
                }
            }
        }



    }
}

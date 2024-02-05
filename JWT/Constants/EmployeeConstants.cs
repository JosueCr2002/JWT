using JWT.Models;

namespace JWT.Constants
{
    public class EmployeeConstants
    {
        public static List<Employee> Employe = new List<Employee>()
        {
            new Employee{FirstName="Daniel",LastName="Cruz",EmailAddress="Dani@gmail.com"},
            new Employee{FirstName="Pedro",LastName="Lopez",EmailAddress="Pedro@gmail.com"},
        };
    }
}

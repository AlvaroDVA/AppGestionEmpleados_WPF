using GestionEmpleados.models;
using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEmpleados.Services;

internal interface IServiceEmployee
{
    public Employee? AddEmployee (Employee employee);

    public Department? AddDepartment (Department department);

    public Employee? DeleteEmployee (Employee employee);

    public Department? DeleteDepartment (Department department);

    public List<Employee> FindAllEmployees ();

    public List<Department> FindAllDepartments ();

    public List<Employee> FindEmployeesByDepartment(Department department);

    public Department FindDepartmentsByName(String name);
}

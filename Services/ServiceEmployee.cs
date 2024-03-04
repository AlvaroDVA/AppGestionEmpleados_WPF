using GestionEmpleados.models;
using GestionEmpleados.repositories;
using models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEmpleados.Services;

class ServiceEmployee : IServiceEmployee 
{

    private readonly IEmployeeRepository EmployeeRepository;
    private readonly IDepartmentRepository DepartmentRepository;

    public ServiceEmployee(
        IEmployeeRepository repositoryEmployee, IDepartmentRepository departmentRepository)
    {
        EmployeeRepository = repositoryEmployee;
        DepartmentRepository = departmentRepository;
    }

    public Department? AddDepartment(Department department) => DepartmentRepository.AddItem(department);

    public Employee? AddEmployee(Employee employee) => EmployeeRepository.AddItem(employee);
       
    public Department? DeleteDepartment(Department department) => DepartmentRepository.DeleteItem(department);
         
    public Employee? DeleteEmployee(Employee employee) =>EmployeeRepository.DeleteByDni(employee.Dni);

    public List<Department> FindAllDepartments() => DepartmentRepository.FindAll();

    public List<Employee> FindAllEmployees() => EmployeeRepository.FindAll();

    public Department FindDepartmentsByName(string name) => DepartmentRepository.FindByNombre(name);

    public List<Employee> FindEmployeesByDepartment(Department department) => EmployeeRepository.FilterByDepartamento(department);

}

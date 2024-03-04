using GestionEmpleados.models;
using models;

namespace GestionEmpleados.repositories;

public interface IEmployeeRepository : ICrudRepository<Employee>
{

    public Employee? FindByDni(string dni);
    public Employee? DeleteByDni(string dni);
    public List<Employee> FilterByDepartamento(Department departamento);
    public List<Employee> RemoveDepartamento(Department departamento);
}
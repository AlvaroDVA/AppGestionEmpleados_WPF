using CommunityToolkit.Mvvm.ComponentModel;
using GestionEmpleados.models;
using GestionEmpleados.models.factories;
using models;

namespace GestionEmpleados.repositories;

public class EmployeeRepository : IEmployeeRepository
{

    private List<Employee> listaEmpleados = new();

    public EmployeeRepository()
    {
        listaEmpleados.AddRange(EmployeeFactory.GenerarListaEmpleados());
    }

    public Employee? AddItem(Employee item)
    {
        if (FindByDni(item.Dni) != null)
        {
            return null;
        }
        listaEmpleados.Add(item);
        return item;
    }

    public Employee DeleteItem(Employee item)
    {
        Employee eliminado = FindByDni(item.Dni)!;
        listaEmpleados.Remove(item);
        return eliminado;
    }

    public void DeleteAll()
    {
        listaEmpleados = new List<Employee>();
    }

    public List<Employee> AddAll(List<Employee> listaItem)
    {
        listaItem.ForEach(empleado => listaEmpleados.Add(empleado));
        return listaEmpleados;
    }

    public List<Employee> FindAll() => listaEmpleados;

    public Employee? FindByDni(string dni) => listaEmpleados.Find(empleado => empleado.Dni == dni);
    

    public Employee? DeleteByDni(string dni)
    {
        var empladoBorrar = FindByDni(dni);

        if (empladoBorrar == null)
        {
            return null;
        }

        listaEmpleados.Remove(empladoBorrar);
        return empladoBorrar;
    }

    public List<Employee> FilterByDepartamento(Department departamento) =>
        listaEmpleados.FindAll(empleado => empleado.DepartamentoEmp?.NombreDepartamento == departamento.NombreDepartamento);

    public List<Employee> RemoveDepartamento(Department departamento)
    {
        var lista = listaEmpleados.FindAll(empleado => empleado.DepartamentoEmp?.NombreDepartamento == departamento.NombreDepartamento);

        foreach (var empleado in lista)
        {
            empleado.DepartamentoEmp = null;
        }

        return lista;
    }

}
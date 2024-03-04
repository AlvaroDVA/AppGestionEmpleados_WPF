using GestionEmpleados.models.factories;
using models;

namespace GestionEmpleados.repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private List<Department> lista = new();
    public Department? AddItem(Department item)
    {
        if (FindByNombre(item.NombreDepartamento) != null)
        {
            return null;
        }
        lista.Add(item);
        return item;
    }

    public DepartmentRepository()
    { 
        lista = DepartmentFactory.GenerarDepartamentos();
    }

    public Department DeleteItem(Department item)
    {
        Department eliminado = FindByNombre(item.NombreDepartamento);
        lista.Remove(item);
        return eliminado;
    }

    public void DeleteAll()
    {
        lista = new List<Department>();
    }

    public List<Department> AddAll(List<Department> listaItem)
    {
        foreach (var departamento in listaItem)
        {
            lista.Add(departamento);
        }

        return lista;
    }

    public List<Department> FindAll() => lista;

    public Department? FindByNombre(string nombre) =>
        lista.Find(departamento => departamento.NombreDepartamento == nombre);

    public Department DeleteByNombre(string nombre)
    {
        var deleteBorrar = FindByNombre(nombre);

        if (deleteBorrar == null)
        {
            return null;
        }

        lista.Remove(deleteBorrar);
        return deleteBorrar;
    }
}
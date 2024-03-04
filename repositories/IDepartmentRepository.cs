using models;

namespace GestionEmpleados.repositories;

public interface IDepartmentRepository : ICrudRepository<Department>
{
    public Department? FindByNombre(string nombre);
    public Department? DeleteByNombre(string nombre);
}
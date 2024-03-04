using models;

namespace GestionEmpleados.models.factories;

public static class DepartmentFactory
{
    public static List<Department> GenerarDepartamentos ()
    {
        var lista = new List<Department>();
        lista.Add(new Department("RR.HH"));
        lista.Add(new Department("Administración"));
        lista.Add(new Department("Contabilidad"));
        lista.Add(new Department("Almacen"));
        lista.Add(new Department("Transporte"));
        return lista;
    }
}
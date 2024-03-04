using models;

namespace GestionEmpleados.models.factories;

public static class EmployeeFactory
{

    private static int contador = 0;

    private static List<Department> departmentList = new List<Department>();

  
    public static Employee GenerarEmpleadoRandom()
    {
        contador += 1;
        return new Employee(
            dni: GenerarDNIRandom(),
            nombre: "Empleado Nº" + contador,
            email: "empleado" + contador + "@empleado.com",
            telefono: new Random().Next(999999999),
            puesto: GenerarPuestoRandom(),
            departamento: departmentList[new Random().Next(0, departmentList.Count)]
        );

    }

    private static string GenerarPuestoRandom()
    {
        var listaPuestos = new List<string>();
        listaPuestos.Add("Jefe");
        listaPuestos.Add("Empleado");
        listaPuestos.Add("Becario");
        return listaPuestos[new Random().Next(2)];
    }

    static string GenerarDNIRandom()
    {
        var random = new Random().Next(maxValue: 99999999);
        return (random.ToString() + CalcularLetra());
    }

    static char CalcularLetra()
    {
        var letras = "TRWAGMYFPDXBNJZSQVHLCKE";
        var letra = letras.ToCharArray();
        return letra[new Random().Next(maxValue: letras.Length - 1)];
    }

    public static List<Employee> GenerarListaEmpleados () {
        var list = new List<Employee>();
        departmentList.Add(new Department("RR.HH"));
        departmentList.Add(new Department("Administración"));
        departmentList.Add(new Department("Contabilidad"));
        departmentList.Add(new Department("Almacen"));
        departmentList.Add(new Department("Transporte")); ;
        for ( var i = 0;i < 50;i++) {
            list.Add(GenerarEmpleadoRandom());
        }
        return list;
    }
}
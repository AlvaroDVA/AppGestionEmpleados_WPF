
using models;

namespace GestionEmpleados.models;

public class Employee(string dni, string nombre, string email, int telefono, string puesto, Department? departamento, string imagenPath = null)
{
    public string Dni { get => dni; set => dni = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Email { get => email; set => email = value; }
    public int Telefono { get => telefono; set => telefono = value; }
    public string Puesto { get => puesto; set => puesto = value; }
    public Department? DepartamentoEmp { get => departamento; set => departamento = value; }

    public string ImagenPath { get => imagenPath; set => imagenPath = value; }

    public override string ToString() => ("Empleado -- Dni : " + Dni + " -- Nombre : " + Nombre + " -- Email " + Email + 
                                          " -- Telefono : " + Telefono + " -- Puesto : " + Puesto + " -- Departamento : "
                                          + PonerDepartamento());
    

    private string PonerDepartamento() => DepartamentoEmp != null ? DepartamentoEmp.NombreDepartamento : "------";
    
}
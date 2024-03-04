namespace models;

public class Department (string nombreDepartamento)
{
    public string NombreDepartamento { get => nombreDepartamento; set => nombreDepartamento = value; }

    public bool Equals(Department dep) => NombreDepartamento == dep.NombreDepartamento;

    public override string ToString()
    {
        return NombreDepartamento;
    }

    public int CountEmployees { get; set; } = 0;
}
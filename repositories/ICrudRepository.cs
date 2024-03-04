namespace GestionEmpleados.repositories;

public interface ICrudRepository<T>
{
    public T? AddItem(T item);
    public T DeleteItem(T item);
    public void DeleteAll();
    public List<T> AddAll(List<T> listaItem);
    public List<T> FindAll();
}
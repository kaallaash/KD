namespace KD.Core.Interfaces.CrudOperations;

public interface ICreateOperation<T>
{
    Task<T> Create(T entity, CancellationToken cancellationToken);
}
using KD.Core.Interfaces.CrudOperations;

namespace KD.Core.Repositories;

public interface IBaseCrudRepository<T> :
    IGetByIdOperation<T>,
    IGetAllOperation<T>,
    ICreateOperation<T>,
    IUpdateOperation<T>,
    IDeleteOperation<T>
{ }
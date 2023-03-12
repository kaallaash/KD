using KD.Core.Repositories;

namespace KD.DAL.Interfaces.Repositories;

public interface IUniversityRepository<T1> : IBaseCrudRepository<T1>
{
    Task<T1?> GetByName(string name, CancellationToken cancellationToken);
}
using KD.DAL.Context;
using KD.DAL.Entities;
using KD.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KD.DAL.Repositories;

public class UniversityRepository : IUniversityRepository<UniversityEntity>
{
    private readonly DatabaseContext _db;

    public UniversityRepository(DatabaseContext db)
    {
        _db = db;
    }
    public async Task<UniversityEntity> Create(UniversityEntity university, CancellationToken cancellationToken)
    {
        var drawingEntity = await _db.Universities.AddAsync(university, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return drawingEntity.Entity;
    }

    public async Task Delete(UniversityEntity university, CancellationToken cancellationToken)
    {
        _db.Universities.Remove(university);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<UniversityEntity?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _db.Universities
            .AsNoTracking()
            .Include(e=> e.Drawings)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<UniversityEntity?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _db.Universities
            .AsNoTracking()
            .Include(e => e.Drawings)
            .FirstOrDefaultAsync(e => e.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<UniversityEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _db.Universities
            .AsNoTracking()
            .Include(e => e.Drawings)
            .ToListAsync(cancellationToken);
    }

    public async Task<UniversityEntity> Update(UniversityEntity university, CancellationToken cancellationToken)
    {
        _db.Entry(university).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken);

        return university;
    }
}
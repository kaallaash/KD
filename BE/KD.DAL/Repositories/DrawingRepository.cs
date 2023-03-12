using KD.DAL.Context;
using KD.DAL.Entities;
using KD.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KD.DAL.Repositories;

public class DrawingRepository : IDrawingRepository<DrawingEntity>
{
    private readonly DatabaseContext _db;

    public DrawingRepository(DatabaseContext db)
    {
        _db = db;
    }
    public async Task<DrawingEntity> Create(DrawingEntity drawing, CancellationToken cancellationToken)
    {
        var drawingEntity = await _db.Drawings.AddAsync(drawing, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return drawingEntity.Entity;
    }

    public async Task Delete(DrawingEntity drawingEntity, CancellationToken cancellationToken)
    {
        _db.Drawings.Remove(drawingEntity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<DrawingEntity?> GetById(int id, CancellationToken cancellationToken)
    {
        return await _db.Drawings
            .AsNoTracking()
            .Include(e => e.University)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<DrawingEntity>> GetAll(CancellationToken cancellationToken)
    {
        return await _db.Drawings
            .AsNoTracking()
            .Include(e => e.University)
            .ToListAsync(cancellationToken);
    }

    public async Task<DrawingEntity> Update(DrawingEntity drawingEntity, CancellationToken cancellationToken)
    {
        _db.Entry(drawingEntity).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken);

        return drawingEntity;
    }
}
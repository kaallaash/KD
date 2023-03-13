using KD.DAL.Entities;
using KD.DAL.Tests.Helpers;

namespace KD.DAL.Tests.Entities;

public static class TestDrawingEntity
{
    internal static IEnumerable<DrawingEntity> GetValidDrawingEntitiesWithId() => new List<DrawingEntity>()
    {
        DrawingEntityHelper.CreateValidDrawingEntity(1),
        DrawingEntityHelper.CreateValidDrawingEntity(2),
        DrawingEntityHelper.CreateValidDrawingEntity(3),
        DrawingEntityHelper.CreateValidDrawingEntity(4),
        DrawingEntityHelper.CreateValidDrawingEntity(5)
    };

    internal static IEnumerable<DrawingEntity> GetValidCreatedDrawingEntities() => new List<DrawingEntity>()
    {
        DrawingEntityHelper.CreateValidDrawingEntityWithoutId(),
        DrawingEntityHelper.CreateValidDrawingEntityWithoutId(),
        DrawingEntityHelper.CreateValidDrawingEntityWithoutId(),
        DrawingEntityHelper.CreateValidDrawingEntityWithoutId(),
        DrawingEntityHelper.CreateValidDrawingEntityWithoutId()
    };

    public static IEnumerable<object[]> GetValidDrawingEntities()
    {
        foreach (var validCreatedDrawingEntity in GetValidCreatedDrawingEntities())
        {
            yield return new object[] { validCreatedDrawingEntity };
        }
    }
}
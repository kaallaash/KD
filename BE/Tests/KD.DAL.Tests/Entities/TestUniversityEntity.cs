using KD.DAL.Entities;
using KD.DAL.Tests.Helpers;

namespace KD.DAL.Tests.Entities;

internal static class TestUniversityEntity
{
    internal static IEnumerable<UniversityEntity> GetValidUniversityEntitiesWithId() => new List<UniversityEntity>()
    {
        UniversityEntityHelper.CreateValidUniversityEntity(1),
        UniversityEntityHelper.CreateValidUniversityEntity(2),
        UniversityEntityHelper.CreateValidUniversityEntity(3),
        UniversityEntityHelper.CreateValidUniversityEntity(4),
        UniversityEntityHelper.CreateValidUniversityEntity(5)
    };

    internal static IEnumerable<UniversityEntity> GetValidCreatedUniversityEntities() => new List<UniversityEntity>()
    {
        UniversityEntityHelper.CreateValidUniversityEntityWithoutId(),
        UniversityEntityHelper.CreateValidUniversityEntityWithoutId(),
        UniversityEntityHelper.CreateValidUniversityEntityWithoutId(),
        UniversityEntityHelper.CreateValidUniversityEntityWithoutId(),
        UniversityEntityHelper.CreateValidUniversityEntityWithoutId()
    };

    public static IEnumerable<object[]> GetValidUniversityEntities()
    {
        foreach (var validCreatedUniversityEntity in GetValidCreatedUniversityEntities())
        {
            yield return new object[] { validCreatedUniversityEntity };
        }
    }
}
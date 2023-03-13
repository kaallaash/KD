using KD.DAL.Entities;

namespace KD.DAL.Tests.Helpers;

public static class UniversityEntityHelper
{
    public static UniversityEntity CreateValidUniversityEntity(int id) => new UniversityEntity()
    {
        Id = id,
        Name = $"Name{id}",
        Drawings = new List<DrawingEntity>()
        {
            DrawingEntityHelper.CreateValidDrawingEntityWithoutUniversity(id)
        }
    };

    public static UniversityEntity CreateValidUniversityEntityWithoutId()
    {
        var random = new Random();
        var number = random.Next();

        return new UniversityEntity()
        {
            Name = $"Name{number}"
        };
    }

    public static UniversityEntity CreateValidUniversityEntityWithoutDrawing(int id) => new UniversityEntity()
    {
        Id = id,
        Name = $"Name{id}",
    };
}
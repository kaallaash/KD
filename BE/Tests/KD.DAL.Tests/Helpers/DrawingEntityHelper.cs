using KD.DAL.Entities;

namespace KD.DAL.Tests.Helpers;

public static class DrawingEntityHelper
{
    public static DrawingEntity CreateValidDrawingEntity(int id) => new DrawingEntity()
    {
        Id = id,
        Title = $"Title{id}",
        Price = id,
        ExamplePhotoLink = $"ExamplePhotoLink{id}",
        ManualLink = $"ManualLink{id}",
        Variants = new List<VariantEntity>()
        {
            VariantEntityHelper.CreateValidVariantEntityWithoutDrawing(id)
        },
        UniversityId = id,
        University = UniversityEntityHelper.CreateValidUniversityEntityWithoutDrawing(id)
    };

    public static DrawingEntity CreateValidDrawingEntityWithoutId()
    {
        var random = new Random();
        var number = random.Next();

        return new DrawingEntity()
        {
            Title = $"Title{number}",
            Price = number,
            ExamplePhotoLink = $"ExamplePhotoLink{number}",
            ManualLink = $"ManualLink{number}",
            Variants = new List<VariantEntity>()
            {
                VariantEntityHelper.CreateValidVariantEntityWithoutDrawing(number)
            },
            UniversityId = number,
            University = UniversityEntityHelper.CreateValidUniversityEntityWithoutDrawing(number)
        };
    }

    public static DrawingEntity CreateValidDrawingEntityWithoutUniversity(int id) =>
        new DrawingEntity()
        {
            Id = id,
            Title = $"Title{id}",
            Price = id,
            ExamplePhotoLink = $"ExamplePhotoLink{id}",
            ManualLink = $"ManualLink{id}",
            Variants = new List<VariantEntity>()
            {
                VariantEntityHelper.CreateValidVariantEntityWithoutDrawing(id)
            },
            UniversityId = id,
        };
}
using KD.DAL.Entities;

namespace KD.DAL.Tests.Helpers;

public static class VariantEntityHelper
{
    public static VariantEntity CreateValidVariantEntity(int id) => new VariantEntity()
    {
        Id = id,
        Number = id,
        IsAvailable = true,
        DrawingId = id,
        Drawing = new DrawingEntity()
    };

    public static VariantEntity CreateValidVariantEntityWithoutId()
    {
        var random = new Random();
        var number = random.Next();

        return new VariantEntity()
        {
            Number = number,
            IsAvailable = true,
            DrawingId = number,
            Drawing = new DrawingEntity()
        };
    }

    public static VariantEntity CreateValidVariantEntityWithoutDrawing(int id) =>
        new VariantEntity()
        {
            Id = id,
            Number = id,
            IsAvailable = true,
            DrawingId = id,
        };
};
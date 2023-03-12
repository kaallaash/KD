using KD.Core.Entities;

namespace KD.DAL.Entities;

public class DrawingEntity : BaseEntity
{
    public string Title { get; set; }
    public int Price { get; set; }
    public string ExamplePhotoLink { get; set; }
    public string? ManualLink { get; set; }
    public IEnumerable<VariantEntity> Variants { get; set; }
    public int UniversityId { get; set; }
    public UniversityEntity? University { get; set; }
}
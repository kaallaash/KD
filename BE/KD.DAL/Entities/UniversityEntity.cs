using KD.Core.Entities;

namespace KD.DAL.Entities;

public class UniversityEntity : BaseEntity
{
    public string Name { get; set; }
    public IEnumerable<DrawingEntity>? Drawings { get; set; }
}
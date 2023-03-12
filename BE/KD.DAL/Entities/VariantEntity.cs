using KD.Core.Entities;

namespace KD.DAL.Entities;

public class VariantEntity : BaseEntity
{
    public int Number { get; set; }
    public bool IsAvailable { get; set; }
    public int DrawingId{ get; set; }
    public DrawingEntity Drawing { get; set; }
}
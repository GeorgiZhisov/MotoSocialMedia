namespace MotoGrid.Data.Common;

/// <summary>
/// Base class for all entities with an integer primary key.
/// </summary>
public abstract class BaseEntity : IAuditInfo, IDeletableEntity
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}

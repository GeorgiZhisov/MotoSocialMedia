namespace MotoGrid.Data.Common;

/// <summary>
/// Entity that is never physically removed from the database (soft delete).
/// </summary>
public interface IDeletableEntity
{
    bool IsDeleted { get; set; }

    DateTime? DeletedOn { get; set; }
}

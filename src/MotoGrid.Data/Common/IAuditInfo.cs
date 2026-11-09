namespace MotoGrid.Data.Common;

/// <summary>
/// Entity that keeps track of when it was created and last modified.
/// The values are filled in automatically by ApplicationDbContext.
/// </summary>
public interface IAuditInfo
{
    DateTime CreatedOn { get; set; }

    DateTime? ModifiedOn { get; set; }
}

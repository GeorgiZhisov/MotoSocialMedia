using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;

using static MotoGrid.Common.Constants.ValidationConstants.Event;

namespace MotoGrid.Data.Models;

/// <summary>
/// A group ride, rally or meetup.
/// </summary>
public class Event : BaseEntity
{
    [Required]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;

    [MaxLength(DescriptionMaxLength)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(LocationMaxLength)]
    public string Location { get; set; } = null!;

    public DateTime StartsOn { get; set; }

    public DateTime? EndsOn { get; set; }

    /// <summary>Maximum number of participants. Null means unlimited.</summary>
    public int? Capacity { get; set; }

    [Required]
    public string OrganizerId { get; set; } = null!;

    public ApplicationUser Organizer { get; set; } = null!;

    public int? GroupId { get; set; }

    public Group? Group { get; set; }

    public int? RouteId { get; set; }

    public Route? Route { get; set; }

    public ICollection<EventParticipant> Participants { get; set; } = new HashSet<EventParticipant>();
}

namespace MotoGrid.Data.Models;

/// <summary>
/// A user who has signed up for an event. The primary key is (EventId, UserId).
/// </summary>
public class EventParticipant
{
    public int EventId { get; set; }

    public Event Event { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    public DateTime RegisteredOn { get; set; }
}

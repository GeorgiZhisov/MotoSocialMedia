using MotoGrid.Data.Models.Enums;

namespace MotoGrid.Data.Models;

/// <summary>
/// Membership of a user in a group. The primary key is (GroupId, UserId).
/// </summary>
public class GroupMember
{
    public int GroupId { get; set; }

    public Group Group { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    public GroupMemberRole Role { get; set; } = GroupMemberRole.Member;

    public DateTime JoinedOn { get; set; }
}

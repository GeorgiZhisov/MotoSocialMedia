namespace MotoGrid.Data.Models;

/// <summary>
/// A user (follower) following another user (followee).
/// The primary key is (FollowerId, FolloweeId).
/// </summary>
public class Follow
{
    public string FollowerId { get; set; } = null!;

    public ApplicationUser Follower { get; set; } = null!;

    public string FolloweeId { get; set; } = null!;

    public ApplicationUser Followee { get; set; } = null!;

    public DateTime CreatedOn { get; set; }
}

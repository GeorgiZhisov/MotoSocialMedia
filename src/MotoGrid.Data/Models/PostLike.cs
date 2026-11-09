namespace MotoGrid.Data.Models;

/// <summary>
/// "Like" of a post by a user. The primary key is (PostId, UserId),
/// so a user can like a post only once.
/// </summary>
public class PostLike
{
    public int PostId { get; set; }

    public Post Post { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public ApplicationUser User { get; set; } = null!;

    public DateTime CreatedOn { get; set; }
}

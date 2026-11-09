using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;

using static MotoGrid.Common.Constants.ValidationConstants.Post;

namespace MotoGrid.Data.Models;

/// <summary>
/// A post in the social feed - text, photos, videos, trip logs or build updates.
/// </summary>
public class Post : BaseEntity
{
    [Required]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;

    [MaxLength(LocationMaxLength)]
    public string? Location { get; set; }

    [Required]
    public string AuthorId { get; set; } = null!;

    public ApplicationUser Author { get; set; } = null!;

    /// <summary>Optional motorcycle from the author's garage the post is about.</summary>
    public int? MotorcycleId { get; set; }

    public Motorcycle? Motorcycle { get; set; }

    /// <summary>Set when the post is published inside a group.</summary>
    public int? GroupId { get; set; }

    public Group? Group { get; set; }

    public ICollection<Media> Media { get; set; } = new HashSet<Media>();

    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    public ICollection<PostLike> Likes { get; set; } = new HashSet<PostLike>();
}

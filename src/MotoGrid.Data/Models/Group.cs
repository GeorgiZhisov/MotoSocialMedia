using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;

using static MotoGrid.Common.Constants.ValidationConstants.Group;

namespace MotoGrid.Data.Models;

/// <summary>
/// A rider group or motorcycle club.
/// </summary>
public class Group : BaseEntity
{
    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [MaxLength(DescriptionMaxLength)]
    public string? Description { get; set; }

    [MaxLength(MotoGrid.Common.Constants.ValidationConstants.Media.UrlMaxLength)]
    public string? CoverImageUrl { get; set; }

    /// <summary>Private groups require approval to join.</summary>
    public bool IsPrivate { get; set; }

    [Required]
    public string CreatorId { get; set; } = null!;

    public ApplicationUser Creator { get; set; } = null!;

    public ICollection<GroupMember> Members { get; set; } = new HashSet<GroupMember>();

    public ICollection<Event> Events { get; set; } = new HashSet<Event>();

    public ICollection<Post> Posts { get; set; } = new HashSet<Post>();
}

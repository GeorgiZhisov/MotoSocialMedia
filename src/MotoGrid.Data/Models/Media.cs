using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;
using MotoGrid.Data.Models.Enums;

using static MotoGrid.Common.Constants.ValidationConstants.Media;

namespace MotoGrid.Data.Models;

/// <summary>
/// An uploaded image or video attached to a post.
/// </summary>
public class Media : BaseEntity
{
    [Required]
    [MaxLength(UrlMaxLength)]
    public string Url { get; set; } = null!;

    public MediaType Type { get; set; }

    /// <summary>Position of the file when a post has several.</summary>
    public int Order { get; set; }

    public int PostId { get; set; }

    public Post Post { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;
using MotoGrid.Data.Models.Enums;

namespace MotoGrid.Data.Models;

/// <summary>
/// Short-lived photo or video that disappears after <see cref="ExpiresOn"/> (24 hours).
/// </summary>
public class Story : BaseEntity
{
    public const int LifetimeHours = 24;
    public const int CaptionMaxLength = 200;

    [Required]
    [MaxLength(MotoGrid.Common.Constants.ValidationConstants.Media.UrlMaxLength)]
    public string MediaUrl { get; set; } = null!;

    public MediaType MediaType { get; set; }

    [MaxLength(CaptionMaxLength)]
    public string? Caption { get; set; }

    public DateTime ExpiresOn { get; set; }

    [Required]
    public string AuthorId { get; set; } = null!;

    public ApplicationUser Author { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;

namespace MotoGrid.Data.Models;

public class ListingImage : BaseEntity
{
    [Required]
    [MaxLength(MotoGrid.Common.Constants.ValidationConstants.Media.UrlMaxLength)]
    public string Url { get; set; } = null!;

    /// <summary>The cover image is shown in search results.</summary>
    public bool IsCover { get; set; }

    public int ListingId { get; set; }

    public Listing Listing { get; set; } = null!;
}

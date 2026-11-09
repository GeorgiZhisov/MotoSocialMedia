using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;

using static MotoGrid.Common.Constants.ValidationConstants.Review;

namespace MotoGrid.Data.Models;

/// <summary>
/// Rating of a seller left by a buyer. Builds the seller's reputation
/// and is one of the anti-scam safeguards of the marketplace.
/// </summary>
public class Review : BaseEntity
{
    [Range(MinRating, MaxRating)]
    public int Rating { get; set; }

    [MaxLength(CommentMaxLength)]
    public string? Comment { get; set; }

    [Required]
    public string ReviewerId { get; set; } = null!;

    public ApplicationUser Reviewer { get; set; } = null!;

    [Required]
    public string SellerId { get; set; } = null!;

    public ApplicationUser Seller { get; set; } = null!;

    public int? ListingId { get; set; }

    public Listing? Listing { get; set; }
}

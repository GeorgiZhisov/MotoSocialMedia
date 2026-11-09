using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;
using MotoGrid.Data.Models.Enums;

using static MotoGrid.Common.Constants.ValidationConstants.Listing;

namespace MotoGrid.Data.Models;

/// <summary>
/// A marketplace listing for a motorcycle, a part, gear or an accessory.
/// The vehicle specification fields are filled in only for motorcycles.
/// </summary>
public class Listing : BaseEntity
{
    [Required]
    [MaxLength(TitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(DescriptionMaxLength)]
    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public ListingCondition Condition { get; set; }

    public ListingStatus Status { get; set; } = ListingStatus.Active;

    [Required]
    [MaxLength(CityMaxLength)]
    public string City { get; set; } = null!;

    public int ViewsCount { get; set; }

    public int CategoryId { get; set; }

    public ListingCategory Category { get; set; } = null!;

    [Required]
    public string SellerId { get; set; } = null!;

    public ApplicationUser Seller { get; set; } = null!;

    // ---- Vehicle specifications (only for the "Motorcycles" category) ----

    [MaxLength(MotoGrid.Common.Constants.ValidationConstants.Motorcycle.MakeMaxLength)]
    public string? Make { get; set; }

    [MaxLength(MotoGrid.Common.Constants.ValidationConstants.Motorcycle.ModelMaxLength)]
    public string? Model { get; set; }

    public int? Year { get; set; }

    public int? MileageKm { get; set; }

    public int? EngineCc { get; set; }

    public MotorcycleCategory? MotorcycleCategory { get; set; }

    /// <summary>List of modifications made to the vehicle, free text.</summary>
    [MaxLength(MotoGrid.Common.Constants.ValidationConstants.Motorcycle.DescriptionMaxLength)]
    public string? Modifications { get; set; }

    public ICollection<ListingImage> Images { get; set; } = new HashSet<ListingImage>();
}

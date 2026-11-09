using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;

using static MotoGrid.Common.Constants.ValidationConstants.Listing;

namespace MotoGrid.Data.Models;

/// <summary>
/// Marketplace category - motorcycles, parts, gear, accessories.
/// </summary>
public class ListingCategory : BaseEntity
{
    [Required]
    [MaxLength(CategoryNameMaxLength)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// True for the "Motorcycles" category - its listings require vehicle
    /// specifications (make, model, year, mileage, engine size).
    /// </summary>
    public bool IsVehicle { get; set; }

    public ICollection<Listing> Listings { get; set; } = new HashSet<Listing>();
}

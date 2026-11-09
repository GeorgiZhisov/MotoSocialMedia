using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;
using MotoGrid.Data.Models.Enums;

using static MotoGrid.Common.Constants.ValidationConstants.Route;

namespace MotoGrid.Data.Models;

/// <summary>
/// A motorcycle route shared by a user.
/// </summary>
public class Route : BaseEntity
{
    [Required]
    [MaxLength(NameMaxLength)]
    public string Name { get; set; } = null!;

    [MaxLength(DescriptionMaxLength)]
    public string? Description { get; set; }

    [Required]
    [MaxLength(PlaceMaxLength)]
    public string StartPlace { get; set; } = null!;

    [Required]
    [MaxLength(PlaceMaxLength)]
    public string EndPlace { get; set; } = null!;

    public double DistanceKm { get; set; }

    public RouteDifficulty Difficulty { get; set; }

    /// <summary>Optional GPX file with the track.</summary>
    [MaxLength(MotoGrid.Common.Constants.ValidationConstants.Media.UrlMaxLength)]
    public string? GpxFileUrl { get; set; }

    [Required]
    public string AuthorId { get; set; } = null!;

    public ApplicationUser Author { get; set; } = null!;

    public ICollection<Event> Events { get; set; } = new HashSet<Event>();
}

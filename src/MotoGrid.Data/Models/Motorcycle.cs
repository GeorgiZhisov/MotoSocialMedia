using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;
using MotoGrid.Data.Models.Enums;

using static MotoGrid.Common.Constants.ValidationConstants.Motorcycle;

namespace MotoGrid.Data.Models;

/// <summary>
/// A motorcycle in a user's garage.
/// </summary>
public class Motorcycle : BaseEntity
{
    [Required]
    [MaxLength(MakeMaxLength)]
    public string Make { get; set; } = null!;

    [Required]
    [MaxLength(ModelMaxLength)]
    public string Model { get; set; } = null!;

    public int Year { get; set; }

    /// <summary>Engine displacement in cubic centimeters.</summary>
    public int EngineCc { get; set; }

    public MotorcycleCategory Category { get; set; }

    [MaxLength(NicknameMaxLength)]
    public string? Nickname { get; set; }

    [MaxLength(DescriptionMaxLength)]
    public string? Description { get; set; }

    [MaxLength(MotoGrid.Common.Constants.ValidationConstants.Media.UrlMaxLength)]
    public string? ImageUrl { get; set; }

    [Required]
    public string OwnerId { get; set; } = null!;

    public ApplicationUser Owner { get; set; } = null!;
}

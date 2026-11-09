using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using MotoGrid.Data.Common;

using static MotoGrid.Common.Constants.ValidationConstants.User;

namespace MotoGrid.Data.Models;

/// <summary>
/// A registered user of the platform (rider, buyer or seller).
/// Extends the default ASP.NET Core Identity user.
/// </summary>
public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
{
    [Required]
    [MaxLength(DisplayNameMaxLength)]
    public string DisplayName { get; set; } = null!;

    [MaxLength(BioMaxLength)]
    public string? Bio { get; set; }

    [MaxLength(CityMaxLength)]
    public string? City { get; set; }

    [MaxLength(MotoGrid.Common.Constants.ValidationConstants.Media.UrlMaxLength)]
    public string? AvatarUrl { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }

    /// <summary>The user's "garage" - the motorcycles they own.</summary>
    public ICollection<Motorcycle> Motorcycles { get; set; } = new HashSet<Motorcycle>();
}

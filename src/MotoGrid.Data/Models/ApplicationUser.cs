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

    public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

    public ICollection<Story> Stories { get; set; } = new HashSet<Story>();

    /// <summary>Users that follow this user.</summary>
    public ICollection<Follow> Followers { get; set; } = new HashSet<Follow>();

    /// <summary>Users that this user follows.</summary>
    public ICollection<Follow> Following { get; set; } = new HashSet<Follow>();

    public ICollection<GroupMember> GroupMemberships { get; set; } = new HashSet<GroupMember>();

    public ICollection<EventParticipant> EventParticipations { get; set; } = new HashSet<EventParticipant>();

    public ICollection<Route> Routes { get; set; } = new HashSet<Route>();

    public ICollection<Listing> Listings { get; set; } = new HashSet<Listing>();

    /// <summary>Reviews this user has received as a seller.</summary>
    public ICollection<Review> ReceivedReviews { get; set; } = new HashSet<Review>();

    /// <summary>Reviews this user has written as a buyer.</summary>
    public ICollection<Review> WrittenReviews { get; set; } = new HashSet<Review>();
}

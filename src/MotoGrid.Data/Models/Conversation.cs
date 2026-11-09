using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;

namespace MotoGrid.Data.Models;

/// <summary>
/// Private chat between a buyer and a seller, usually about a specific listing.
/// </summary>
public class Conversation : BaseEntity
{
    [Required]
    public string BuyerId { get; set; } = null!;

    public ApplicationUser Buyer { get; set; } = null!;

    [Required]
    public string SellerId { get; set; } = null!;

    public ApplicationUser Seller { get; set; } = null!;

    public int? ListingId { get; set; }

    public Listing? Listing { get; set; }

    public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
}

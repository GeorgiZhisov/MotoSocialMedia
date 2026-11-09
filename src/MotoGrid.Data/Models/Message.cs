using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;

using static MotoGrid.Common.Constants.ValidationConstants.Message;

namespace MotoGrid.Data.Models;

public class Message : BaseEntity
{
    [Required]
    [MaxLength(ContentMaxLength)]
    public string Content { get; set; } = null!;

    public bool IsRead { get; set; }

    public int ConversationId { get; set; }

    public Conversation Conversation { get; set; } = null!;

    [Required]
    public string SenderId { get; set; } = null!;

    public ApplicationUser Sender { get; set; } = null!;
}

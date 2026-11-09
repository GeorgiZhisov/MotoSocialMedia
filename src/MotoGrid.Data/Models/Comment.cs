using System.ComponentModel.DataAnnotations;
using MotoGrid.Data.Common;

using static MotoGrid.Common.Constants.ValidationConstants.Post;

namespace MotoGrid.Data.Models;

public class Comment : BaseEntity
{
    [Required]
    [MaxLength(CommentMaxLength)]
    public string Content { get; set; } = null!;

    public int PostId { get; set; }

    public Post Post { get; set; } = null!;

    [Required]
    public string AuthorId { get; set; } = null!;

    public ApplicationUser Author { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;

namespace clout_api.Data.Dtos.Comment;

public class RequestCommentDto
{
    [Required]
    public string Content { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int PostId { get; set; }
}

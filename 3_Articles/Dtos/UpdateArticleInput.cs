using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class UpdateArticleInput : CreateArticleInput
{
    [Required]
    public Guid Id { get; set; }
}

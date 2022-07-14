using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateArticleInput
{
    [Required]
    public string TitleAr { get; set; }
    [Required]
    public string TitleEn { get; set; }
    [Required]
    public string ContentAr { get; set; }
    [Required]
    public string ContentEn { get; set; }
    [Required]
    public string Url { get; set; }
}

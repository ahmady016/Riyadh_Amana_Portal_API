using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateArticleInput
{
    [Required]
    [StringLength(80)]
    public string TitleAr { get; set; }

    [Required]
    [StringLength(80)]
    public string TitleEn { get; set; }

    [Required]
    [StringLength(2000)]
    public string ContentAr { get; set; }

    [Required]
    [StringLength(2000)]
    public string ContentEn { get; set; }

    [Required]
    [StringLength(400)]
    public string Url { get; set; }
}

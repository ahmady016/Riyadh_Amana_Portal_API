using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateVideoInput
{
    [Required]
    [StringLength(80)]
    public string TitleAr { get; set; }
    [Required]
    [StringLength(80)]
    public string TitleEn { get; set; }
    [Required]
    [StringLength(400)]
    public string DescriptionAr { get; set; }
    [Required]
    [StringLength(400)]
    public string DescriptionEn { get; set; }
    public string ThumbUrl { get; set; }
    public string Url { get; set; }
}

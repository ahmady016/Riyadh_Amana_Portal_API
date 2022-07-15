using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateNewsInput
{
    [Required]
    [StringLength(100)]
    public string TitleAr { get; set; }

    [Required]
    [StringLength(100)]
    public string TitleEn { get; set; }

    [Required]
    [StringLength(100)]
    public string SourceAr { get; set; }

    [Required]
    [StringLength(100)]
    public string SourceEn { get; set; }

    [Required]
    [StringLength(200)]
    public string BriefAr { get; set; }

    [Required]
    [StringLength(200)]
    public string BriefEn { get; set; }

    [Required]
    [StringLength(2000)]
    public string ContentAr { get; set; }

    [Required]
    [StringLength(2000)]
    public string ContentEn { get; set; }

    [Required]
    [StringLength(400)]
    public string ImageUrl { get; set; }

    [Required]
    [StringLength(400)]
    public string ThumbUrl { get; set; }

    [StringLength(50)]
    public string HijriDate { get; set; }

    [StringLength(500)]
    public string Tags { get; set; }

    public bool? IsInHome { get; set; } = false;
}

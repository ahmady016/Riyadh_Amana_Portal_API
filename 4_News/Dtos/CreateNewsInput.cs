using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateNewsInput
{
    [Required(ErrorMessage = "TitleAr is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "TitleAr must between 5 and 200 characters")]
    public string TitleAr { get; set; }

    [Required(ErrorMessage = "TitleEn is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "TitleEn must between 5 and 200 characters")]
    public string TitleEn { get; set; }

    [Required(ErrorMessage = "SourceAr is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "SourceAr must between 5 and 100 characters")]
    public string SourceAr { get; set; }

    [Required(ErrorMessage = "SourceEn is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "SourceEn must between 5 and 100 characters")]
    public string SourceEn { get; set; }

    [Required(ErrorMessage = "BriefAr is required")]
    [StringLength(400, MinimumLength = 10, ErrorMessage = "BriefAr must between 10 and 400 characters")]
    public string BriefAr { get; set; }

    [Required(ErrorMessage = "BriefEn is required")]
    [StringLength(400, MinimumLength = 10, ErrorMessage = "BriefEn must between 10 and 400 characters")]
    public string BriefEn { get; set; }

    [Required(ErrorMessage = "ContentAr is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "ContentAr must between 10 and 2000 characters")]
    public string ContentAr { get; set; }

    [Required(ErrorMessage = "ContentEn is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "ContentEn must between 10 and 2000 characters")]
    public string ContentEn { get; set; }

    [DataType(DataType.ImageUrl)]
    [Required(ErrorMessage = "ImageUrl is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "ImageUrl must between 10 and 500 characters")]
    public string ImageUrl { get; set; }

    [DataType(DataType.ImageUrl)]
    [Required(ErrorMessage = "ThumbUrl is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "ThumbUrl must between 10 and 500 characters")]
    public string ThumbUrl { get; set; }

    [StringLength(50, MinimumLength = 10, ErrorMessage = "ImageUrl must between 10 and 50 characters")]
    public string HijriDate { get; set; }

    [StringLength(500, MinimumLength = 3, ErrorMessage = "ImageUrl must between 3 and 500 characters")]
    public string Tags { get; set; }

    public bool? IsInHome { get; set; } = false;
}

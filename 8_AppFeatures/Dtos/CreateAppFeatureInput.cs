using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateAppFeatureInput
{
    [Required(ErrorMessage = "TitleAr is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "TitleAr Must be between 5 and 100 characters")]
    public string TitleAr { get; set; }

    [Required(ErrorMessage = "TitleEn is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "TitleEn Must be between 5 and 100 characters")]
    public string TitleEn { get; set; }

    [Required(ErrorMessage = "DescriptionAr is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "DescriptionAr Must be between 10 and 500 characters")]
    public string DescriptionAr { get; set; }

    [Required(ErrorMessage = "DescriptionEn is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "DescriptionEn Must be between 10 and 500 characters")]
    public string DescriptionEn { get; set; }

    [DataType(DataType.ImageUrl)]
    [Required(ErrorMessage = "IconUrl is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "IconUrl Must be between 10 and 500 characters")]
    public string IconUrl { get; set; }

    [DataType(DataType.Url)]
    [Required(ErrorMessage = "Url is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Url Must be between 10 and 500 characters")]
    public string Url { get; set; }

    public byte? Order { get; set; }
}

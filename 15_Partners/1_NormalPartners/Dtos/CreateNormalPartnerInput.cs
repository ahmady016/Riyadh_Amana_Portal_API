using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateNormalPartnerInput
{
    [Required(ErrorMessage = "TitleAr is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "TitleAr Must be between 5 and 200 characters")]
    public string TitleAr { get; set; }

    [Required(ErrorMessage = "TitleAr is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "TitleAr Must be between 5 and 200 characters")]
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
}

using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateNavInput
{
    [Required(ErrorMessage = "TitleAr is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "TitleAr Must be between 5 and 100 characters")]
    public string TitleAr { get; set; }

    [Required(ErrorMessage = "TitleEn is required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "TitleEn Must be between 5 and 100 characters")]
    public string TitleEn { get; set; }

    [StringLength(200, MinimumLength = 10, ErrorMessage = "DescriptionAr Must be between 10 and 200 characters")]
    public string DescriptionAr { get; set; }

    [StringLength(200, MinimumLength = 10, ErrorMessage = "DescriptionEn Must be between 10 and 200 characters")]
    public string DescriptionEn { get; set; }

    [DataType(DataType.ImageUrl)]
    [Required(ErrorMessage = "IconUrl is required")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "IconUrl Must be between 5 and 500 characters")]
    public string IconUrl { get; set; }
}

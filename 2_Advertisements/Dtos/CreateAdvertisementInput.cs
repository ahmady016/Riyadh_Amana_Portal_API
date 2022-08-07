using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateAdvertisementInput
{
    [Required(ErrorMessage = "TitleAr is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "TitleAr must be between 5 and 200 characters")]
    public string TitleAr { get; set; }

    [Required(ErrorMessage = "TitleEn is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "TitleEn must be between 5 and 200 characters")]
    public string TitleEn { get; set; }

    [Required(ErrorMessage = "ContentAr is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "ContentAr must be between 10 and 2000 characters")]
    public string ContentAr { get; set; }

    [Required(ErrorMessage = "ContentEn is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "ContentEn must be between 10 and 2000 characters")]
    public string ContentEn { get; set; }

    [DataType(DataType.Url)]
    [Required(ErrorMessage = "Url is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Url must be between 10 and 500 characters")]
    public string Url { get; set; }

    [DataType(DataType.ImageUrl)]
    [Required(ErrorMessage = "ImageUrl is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "ImageUrl must be between 10 and 500 characters")]
    public string ImageUrl { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public int? Order { get; set; }
    public bool? IsHomeSlider { get; set; } = false;
    public bool? IsPopup { get; set; } = false;
}

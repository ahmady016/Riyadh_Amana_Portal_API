using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateAwardInput
{
    [Required(ErrorMessage = "TitleAr is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "TitleAr must between 5 and 200 characters")]
    public string TitleAr { get; set; }

    [Required(ErrorMessage = "TitleEn is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "TitleEn must between 5 and 200 characters")]
    public string TitleEn { get; set; }

    [Required(ErrorMessage = "ContentAr is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "ContentAr must between 10 and 2000 characters")]
    public string ContentAr { get; set; }

    [Required(ErrorMessage = "ContentEn is required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "ContentEn must between 10 and 2000 characters")]
    public string ContentEn { get; set; }

    [DataType(DataType.ImageUrl)]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "IconUrl must between 10 and 500 characters")]
    public string IconUrl { get; set; }

    [DataType(DataType.ImageUrl)]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "IconBase64Url must between 10 and 1000 characters")]
    public string IconBase64Url { get; set; }
    
    public int? Order { get; set; }
}

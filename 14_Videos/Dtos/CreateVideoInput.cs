using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateVideoInput
{
    [Required(ErrorMessage = "TitleAr is Required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "TitleAr must between 5 and 100 characters")]
    public string TitleAr { get; set; }

    [Required(ErrorMessage = "TitleEn is Required")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "TitleEn must between 5 and 100 characters")]
    public string TitleEn { get; set; }

    [StringLength(200, MinimumLength = 10, ErrorMessage = "DescriptionAr must between 10 and 200 characters")]
    public string DescriptionAr { get; set; }

    [StringLength(200, MinimumLength = 10, ErrorMessage = "DescriptionEn must between 10 and 200 characters")]
    public string DescriptionEn { get; set; }

    [DataType(DataType.ImageUrl)]
    [Required(ErrorMessage = "ThumbUrl is Required")]
    [StringLength(400, MinimumLength = 10, ErrorMessage = "ThumbUrl must between 10 and 400 characters")]
    public string ThumbUrl { get; set; }

    [DataType(DataType.Url)]
    [Required(ErrorMessage = "Url is Required")]
    [StringLength(400, MinimumLength = 10, ErrorMessage = "Url must between 10 and 400 characters")]
    public string Url { get; set; }

}

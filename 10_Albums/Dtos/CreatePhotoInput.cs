using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreatePhotoInput
{
    [Required(ErrorMessage = "TitleAr is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "TitleAr Must be between 5 and 200 characters")]
    public string TitleAr { get; set; }

    [Required(ErrorMessage = "TitleEn is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "TitleEn Must be between 5 and 200 characters")]
    public string TitleEn { get; set; }

    [Required(ErrorMessage = "DescriptionAr is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "DescriptionAr Must be between 10 and 500 characters")]
    public string DescriptionAr { get; set; }

    [Required(ErrorMessage = "DescriptionEn is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "DescriptionEn Must be between 10 and 500 characters")]
    public string DescriptionEn { get; set; }

    [Required(ErrorMessage = "TagsAr is required")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "TagsAr Must be between 5 and 500 characters")]
    public string TagsAr { get; set; }

    [Required(ErrorMessage = "TagsEn is required")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "TagsEn Must be between 5 and 500 characters")]
    public string TagsEn { get; set; }

    [DataType(DataType.ImageUrl)]
    [Required(ErrorMessage = "Url is required")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "Url Must be between 5 and 500 characters")]
    public string Url { get; set; }

    [DataType(DataType.ImageUrl)]
    [Required(ErrorMessage = "ThumbUrl is required")]
    [StringLength(500, MinimumLength = 5, ErrorMessage = "ThumbUrl Must be between 5 and 500 characters")]
    public string ThumbUrl { get; set; }

    [RegularExpression(
        @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
        ErrorMessage = "Not a valid AlbumId value"
    )]
    public Guid? AlbumId { get; set; }
}

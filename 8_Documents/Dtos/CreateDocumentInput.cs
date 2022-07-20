using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateDocumentInput
{
    [Required(ErrorMessage = "TitleAr is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "TitleAr Must be between 3 and 100 characters")]
    public string TitleAr { get; set; }

    [Required(ErrorMessage = "TitleEn is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "TitleEn Must be between 3 and 100 characters")]
    public string TitleEn { get; set; }

    [Required(ErrorMessage = "TitleEn is required")]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "DescriptionAr Must be between 10 and 200 characters")]
    public string DescriptionAr { get; set; }

    [Required(ErrorMessage = "TitleEn is required")]
    [StringLength(200, MinimumLength = 10, ErrorMessage = "DescriptionEn Must be between 10 and 200 characters")]
    public string DescriptionEn { get; set; }

    [StringLength(400, MinimumLength = 10, ErrorMessage = "Url Must be between 10 and 400 characters")]
    public string Url { get; set; }

    [StringLength(1000, MinimumLength = 10, ErrorMessage = "Base64Url Must be between 10 and 1000 characters")]
    public string Base64Url { get; set; }
}

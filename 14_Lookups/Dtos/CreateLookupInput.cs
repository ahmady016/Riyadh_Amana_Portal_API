using System.ComponentModel.DataAnnotations;
namespace Dtos;
public class CreateLookupInput
{
    [Required(ErrorMessage = "TitleAr is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "TitleAr Must be between 3 and 200 characters")]
    public string TitleAr { get; set; }

    [Required(ErrorMessage = "TitleEn is required")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "TitleEn Must be between 3 and 200 characters")]
    public string TitleEn { get; set; }
}

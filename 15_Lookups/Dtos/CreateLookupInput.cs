using System.ComponentModel.DataAnnotations;
namespace Dtos;
public class CreateLookupInput
{
    [Required(ErrorMessage = "TitleAr is required")]
    [StringLength(80, MinimumLength = 5, ErrorMessage = "TitleAr Must be between 5 and 80 characters")]
    public string TitleAr { get; set; }

    [Required(ErrorMessage = "TitleEn is required")]
    [StringLength(80, MinimumLength = 5, ErrorMessage = "TitleEn Must be between 5 and 80 characters")]
    public string TitleEn { get; set; }

    [Required(ErrorMessage = "Discriminator is required")]
    [StringLength(25, MinimumLength = 5, ErrorMessage = "Discriminator Must be city Or qualification Or nationality")]
    public string Discriminator { get; set; }
}



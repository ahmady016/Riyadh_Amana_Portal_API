using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreatePageKeyInput
{
    [Required(ErrorMessage = "Key is required")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "Key Must be between 5 and 50 characters")]
    public string Key { get; set; }

    [Required(ErrorMessage = "ValueAr is required")]
    [StringLength(400, MinimumLength = 5, ErrorMessage = "ValueAr Must be between 5 and 400 characters")]
    public string ValueAr { get; set; }

    [Required(ErrorMessage = "ValueEn is required")]
    [StringLength(400, MinimumLength = 5, ErrorMessage = "ValueEn Must be between 5 and 400 characters")]
    public string ValueEn { get; set; }

    [Required(ErrorMessage = "PageId is required")]
    [RegularExpression(
        @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
        ErrorMessage = "Not a valid PageId value"
    )]
    public Guid PageId { get; set; }

}

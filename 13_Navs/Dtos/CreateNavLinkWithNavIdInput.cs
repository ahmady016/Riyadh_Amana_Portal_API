using System.ComponentModel.DataAnnotations;

namespace Dtos;
public class CreateNavLinkWithNavIdInput : CreateNavLinkInput
{
    [Required(ErrorMessage = "NavId is required")]
    [RegularExpression(
        @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
        ErrorMessage = "Not a valid NavId value"
    )]
    public Guid NavId { get; set; }

}

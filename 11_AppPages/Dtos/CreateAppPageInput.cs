using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateAppPageInput
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Title Must be between 5 and 200 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Description Must be between 10 and 500 characters")]
    public string Description { get; set; }

    [DataType(DataType.Url)]
    [Required(ErrorMessage = "Url is required")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Url Must be between 10 and 500 characters")]
    public string Url { get; set; }
}

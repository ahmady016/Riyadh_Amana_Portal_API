using System.ComponentModel.DataAnnotations;

namespace Dtos;

public class CreateAdvertisementInput
{
    [Required]
    [StringLength(80)]
    public string TitleAr { get; set; }

    [Required]
    [StringLength(80)]
    public string TitleEn { get; set; }

    [Required]
    [StringLength(2000)]
    public string ContentAr { get; set; }

    [Required]
    [StringLength(2000)]
    public string ContentEn { get; set; }

    [Required]
    [StringLength(400)]
    public string Url { get; set; }

    [Required]
    [StringLength(400)]
    public string ImageUrl { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public int? Order { get; set; }
    public bool? IsHomeSlider { get; set; } = false;
    public bool? IsPopup { get; set; } = false;
}

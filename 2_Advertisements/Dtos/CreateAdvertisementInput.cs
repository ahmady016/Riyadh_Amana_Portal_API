namespace Dtos;

public class CreateAdvertisementInput
{
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string ContentAr { get; set; }
    public string ContentEn { get; set; }
    public string Url { get; set; }
    public string ImageUrl { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? Order { get; set; }
    public bool? IsHomeSlider { get; set; } = false;
    public bool? IsPopup { get; set; } = false;
}

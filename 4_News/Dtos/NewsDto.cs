namespace Dtos;

public class NewsDto
{
    public Guid Id { get; set; }
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string SourceAr { get; set; }
    public string SourceEn { get; set; }
    public string BriefAr { get; set; }
    public string BriefEn { get; set; }
    public string ContentAr { get; set; }
    public string ContentEn { get; set; }
    public string ImageUrl { get; set; }
    public string ThumbUrl { get; set; }
    public string HijriDate { get; set; }
    public string Tags { get; set; }
    public bool? IsInHome { get; set; } = false;
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
}

namespace Dtos;

public class VideoDto
{
    public Guid Id { get; set; }
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    public string ThumbUrl { get; set; }
    public string Url { get; set; }
}

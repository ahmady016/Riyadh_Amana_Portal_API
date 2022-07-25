namespace Dtos;

public class AppFeatureDto
{
    public Guid Id { get; set; }
    public string TitleAr { get; set; }
    public string TitleEn { get; set; }
    public string DescriptionAr { get; set; }
    public string DescriptionEn { get; set; }
    public string IconUrl { get; set; }
    public string Url { get; set; }
    public byte? Order { get; set; }
}
